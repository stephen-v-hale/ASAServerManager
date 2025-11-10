using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace ASARcon;

/// <summary>
/// Minimal, async-only Source RCON client implementation (pure TcpClient, no external libs).
/// Use <see cref="ConnectAsync"/> then <see cref="AuthenticateAsync"/> before sending commands.
/// </summary>
public sealed class Rcon: IDisposable
{
    private TcpClient? _tcp;
    private NetworkStream? _stream;
    private int _nextRequestId;
    private bool _disposed;

    public static Rcon Instance;

    public event EventHandler<Exception>? OnError;

    /// <summary>
    /// Raised when starting to check server avaliability.
    /// </summary>
    public event EventHandler OnAvaliability;


    /// <summary>
    /// Raised when avaliability check has finished.
    /// </summary>
    public event EventHandler<bool> OnAvaliabilityCompleted;

    public Authentication Authentication { get; private set; }
    public bool IsConnected { get; private set; }

    /// <summary>
    /// Initialize a new instance of <see cref="Rcon"/>
    /// </summary>
    /// <param name="authentication"></param>
    /// <exception cref="ArgumentNullException"></exception>

    public Rcon(Authentication authentication)
    {
        if ( authentication == null ) throw new ArgumentNullException( nameof( authentication ) );

        this.Authentication = authentication;

        Instance = this;
    }


    public bool Disconnect()
    {
        if(_tcp != null)
        {
            try
            {
                _tcp.Close();
                IsConnected = false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a list of players.
    /// </summary>
    /// <returns></returns>
    public async Task<ASAPlayer[]> GetPlayerList()
    {
       try
        {
            var rcon = await SendCommandAsync("ListPlayers", default);

            var items = rcon.Split("\n");

            List<ASAPlayer> players = new List<ASAPlayer>();
            foreach ( var item in items )
            {
                if ( !string.IsNullOrWhiteSpace( item ) )
                {
                    players.Add( new ASAPlayer( item ) );
                }
            }
            return players.ToArray();
        }
        catch
        {
            OnError?.Invoke( this, new Exception( "Unable to list players" ));
            return new ASAPlayer[0];
        }
    }

    /// <summary>
    /// Connects to the RCON endpoint.
    /// </summary>
    /// <param name="host">Server hostname or IP.</param>
    /// <param name="port">RCON port.</param>
    /// <param name="cancellation">Cancellation token.</param>
    public async Task<bool> ConnectAsync(CancellationToken cancellation = default )
    {
        EnsureNotDisposed();
        try
        {
            OnAvaliability?.Invoke( this, EventArgs.Empty);
            bool avaliable = false;
            try
            {
                var ping = new Ping().Send(Authentication.Address);

                avaliable = ping.Status == IPStatus.Success;
            }
            catch
            {
                avaliable = false;
            }

            OnAvaliabilityCompleted?.Invoke( this, avaliable );

            if(avaliable)
            {
                _tcp = new TcpClient();
                await _tcp.ConnectAsync( Authentication.Address, Authentication.Port ).ConfigureAwait( false );
                _stream = _tcp.GetStream();
                _nextRequestId = 0;

                IsConnected = true;

                return true;
            }
            else
            {
                OnError?.Invoke( this, new Exception( "Server not reachable" ) );
                return false;
            }
        }
        catch
        {
            IsConnected = false;
            OnError?.Invoke( this, new Exception(  "Unable to connect to host " + Authentication.Address ) );

            return false;
        }
    }

    /// <summary>
    /// Sets the message of day.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> SetMessageOfTheDay(string message)
    {
        try
        {
            await SendCommandAsync( $"SetMessageOfTheDay {message}" );
            return true;
        }
        catch
        {
            OnError?.Invoke( this, new Exception( "Unable to set message of the day." ));
            return false;
        }
    }


    public async Task SendMessageToPlayer(string message, ASAPlayer player)
    {
        await SendCommandAsync($"ServerChatToPlayer {player.Name} {message}", default);
    }

    public async Task<string[]> GetServerLog()
    {
        var result = await SendCommandAsync("GetGameLog 0");
        return result.Split( "\n" );
    }


    /// <summary>
    /// Authenticates to the server using the server admin password (ServerAdminPassword).
    /// Returns true on success.
    /// </summary>
    /// <param name="password">RCON password (server admin password).</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if authentication succeeded.</returns>
    public async Task<bool> AuthenticateAsync(CancellationToken cancellation = default )
    {
        try
        {
            EnsureConnected();
            int req = GetNextRequestId();
            var packet = new Packet(req, PacketType.Auth, Authentication.Password);
            await SendPacketAsync( packet, cancellation ).ConfigureAwait( false );

            var resp = await ReadPacketAsync(cancellation).ConfigureAwait(false);
            // On auth failure some servers return -1 requestId. We validate by id match.

            IsConnected = resp.RequestId == req;

            return IsConnected;
        }
        catch
        {
            OnError?.Invoke( this, new Exception( "Unable to authenticate." ));
            return false;
        }
    }


    public async Task<bool> EnableCheats()
    {
        try
        {
            await SendCommandAsync( $"EnableCheats {Authentication.Password}" );
            return true;
        }
        catch
        {
            OnError?.Invoke( this, new Exception( "Unable to enable cheats." ) );
            return false;
        }
    }

    /// <summary>
    /// Sends a raw command string via RCON and returns the raw text response.
    /// </summary>
    /// <param name="command">Full command string, e.g. "ListPlayers" or "Broadcast Hello".</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Server's textual response.</returns>
    public async Task<string> SendCommandAsync( string command, CancellationToken cancellation = default )
    {
        try
        {
            EnsureConnected();
            int req = GetNextRequestId();
            var packet = new Packet(req, PacketType.ExecCommand, command);
            await SendPacketAsync( packet, cancellation ).ConfigureAwait( false );

            var resp = await ReadPacketAsync(cancellation).ConfigureAwait(false);
            // Note: Some servers may send multiple response packets for large outputs; this minimal client returns the first payload.

            return resp.Body;
        }
        catch
        {
            OnError?.Invoke( this, new Exception( $"Unable to send command {'"'}{command}{'"'}" ) );
            return string.Empty;
        }
    }

    /// <summary>
    /// Sends a typed command (from <see cref="RconCommand"/>) with arguments.
    /// </summary>
    /// <param name="cmd">Typed command enum.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <param name="args">Arguments required by the typed command.</param>
    /// <returns>Server textual response.</returns>
    public Task<string> SendCommandAsync( Command cmd, CancellationToken cancellation = default, params object[] args )
    {
        string text = cmd.ToCommandString(args);
        return SendCommandAsync( text, cancellation );
    }

    /// <summary>
    /// Shutdown the server.
    /// </summary>
    public async void ShutDown()
    {
        try
        {
            await SendCommandAsync( "cheat DoExit" );

            _tcp?.Close();

            Dispose();
        }
        catch
        {
            OnError?.Invoke( this, new Exception( "Could not shutdown server" ) );
        }
    }

    /// <summary>
    /// Disposes the client and closes the underlying TCP connection.
    /// </summary>
    public void Dispose()
    {
        if ( _disposed ) return;
        _disposed = true;
        _stream?.Dispose();
        _tcp?.Dispose();
        _stream = null;
        _tcp = null;
    }

    #region Private helpers

    private int GetNextRequestId()
    {
        return System.Threading.Interlocked.Increment( ref _nextRequestId );
    }

    private void EnsureConnected()
    {
        if ( _tcp == null || _stream == null || !_tcp.Connected )
            throw new InvalidOperationException( "RconClient is not connected. Call ConnectAsync first." );
    }

    private void EnsureNotDisposed()
    {
        if ( _disposed ) throw new ObjectDisposedException( nameof( Rcon ) );
    }

    private async Task SendPacketAsync( Packet packet, CancellationToken cancellation )
    {
        EnsureNotDisposed();
        EnsureConnected();

        byte[] bytes = packet.ToBytes();
        await _stream!.WriteAsync( bytes, 0, bytes.Length, cancellation ).ConfigureAwait( false );
    }

    private async Task<Packet> ReadPacketAsync( CancellationToken cancellation )
    {
        try
        {
            EnsureNotDisposed();
            EnsureConnected();

            // Read the 4-byte length (little-endian)
            byte[] lenBuf = new byte[4];
            await ReadExactlyAsync( lenBuf, 0, 4, cancellation ).ConfigureAwait( false );
            int length = BitConverter.ToInt32(lenBuf, 0);

            if ( length <= 0 )
            {
                // Some servers may send 0; handle gracefully
                return new Packet( -1, PacketType.ResponseValue, string.Empty );
            }

            // Read the payload of 'length' bytes
            byte[] payload = new byte[length];
            await ReadExactlyAsync( payload, 0, length, cancellation ).ConfigureAwait( false );

            // Parse and return
            return Packet.FromPayloadBytes( payload );
        }
        catch
        {
            OnError?.Invoke( this, new Exception( "Unable to read rcon package." ) );
            return new Packet( -1, PacketType.ResponseValue, string.Empty );
        }
    }

    private async Task ReadExactlyAsync( byte[] buffer, int offset, int count, CancellationToken cancellation )
    {
        int read = 0;
        while ( read < count )
        {
            int n = await _stream!.ReadAsync(buffer, offset + read, count - read, cancellation).ConfigureAwait(false);
            if ( n == 0 )
            {
                // remote closed connection
                throw new IOException( "Remote socket closed while attempting to read data." );
            }
            read += n;
        }
    }

    #endregion
}