using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Text;

namespace ASARcon;

public delegate void RconConnectionEventHandler( bool isConnected );
public delegate void RconAuthEventHandler( bool isAuthenticated );
public delegate void RconCommandEventHandler( string command );
public delegate void RconEventHandler();
public delegate void RconEventHandler<T>(T value);
public class RconNetworkAccess
{

    /// <summary>
    /// Gets whether there is an avaliable internet connection.
    /// </summary>
    public bool HasInternetAccess { get; }

    /// <summary>
    /// Gets a bool whether there is an avaliable network connection.
    /// </summary>
    public bool HasNetworkAccess { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="RconNetworkAccess"/>
    /// </summary>
    /// <param name="hIa"></param>
    /// <param name="hNa"></param>
    public RconNetworkAccess( bool hIa, bool hNa )
    {
        HasNetworkAccess = hNa;
        HasInternetAccess = hIa;
    }
}

/// <summary>
/// Minimal, async-only Source RCON client implementation (pure TcpClient, no external libs).
/// Use <see cref="ConnectAsync"/> then <see cref="AuthenticateAsync"/> before sending commands.
/// </summary>
public sealed class Rcon: IDisposable
{
    List<ASAPlayer> joinedPlayers = new List<ASAPlayer>();
    List<ASAPlayer> lastPlayers = new List<ASAPlayer>();
    List<string> lastEntries = new List<string>();

    System.Windows.Forms.Timer _timer;

    private TcpClient _tcp;
    private NetworkStream _stream;
    private int _nextRequestId;
    private bool _disposed;

    public static Rcon Instance;

    /// <summary>
    /// Occurs when an error is encountered in the RCON connection.
    /// </summary>
    public event RconEventHandler<Exception> OnError;

    /// <summary>
    /// Occurs when the RCON server's availability is being checked.
    /// </summary>
    public event RconEventHandler OnAvaliability;

    /// <summary>
    /// Occurs when the RCON server availability check is completed.
    /// The boolean parameter indicates whether the server is available.
    /// </summary>
    public event RconEventHandler<bool> OnAvaliabilityCompleted;

    /// <summary>
    /// Occurs when a connection attempt to the RCON server is initiated.
    /// </summary>
    public event RconEventHandler OnConnecting;

    /// <summary>
    /// Occurs when the RCON client successfully establishes a connection.
    /// </summary>
    public event RconConnectionEventHandler OnConnected;

    /// <summary>
    /// Occurs when authentication with the RCON server is in progress.
    /// </summary>
    public event RconEventHandler OnAuthenticating;

    /// <summary>
    /// Occurs when authentication with the RCON server is successful.
    /// </summary>
    public event RconAuthEventHandler OnAuthenticated;

    /// <summary>
    /// Occurs when a command is about to be sent to the RCON server.
    /// </summary>
    public event RconEventHandler OnSendingCommand;

    /// <summary>
    /// Occurs after a command has been sent to the RCON server.
    /// </summary>
    public event RconCommandEventHandler OnCommandSent;

    /// <summary>
    /// Occurs when the RCON client is disconnected from the server.
    /// </summary>
    public event RconEventHandler OnDisconnected;

    /// <summary>
    /// Occurs when a network access check is performed for the RCON client.
    /// </summary>
    public event RconEventHandler<RconNetworkAccess> OnNetworkAccessCheck;

    /// <summary>
    /// Occurs when a player joins the server.
    /// </summary>
    public event RconEventHandler<ASAPlayer> OnPlayerJoined;

    /// <summary>
    /// Occurs when a player leaves the server.
    /// </summary>
    public event RconEventHandler<ASAPlayer> OnPlayerLeave;

    /// <summary>
    /// Occurs when a log entry is received from the server.
    /// </summary>
    public event RconEventHandler<RconLogEntry> OnLogEntry;

    /// <summary>
    /// Gets the authentication information used for the RCON connection.
    /// </summary>
    public Authentication Authentication { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the client is currently connected to the server.
    /// </summary>
    public bool IsConnected { get; private set; } = false;

    /// <summary>
    /// Gets the list of players currently connected to the server.
    /// </summary>
    public List<ASAPlayer> ConnectedPlayers { get; private set; }

    /// <summary>
    /// Gets or sets whether to allow redirecting of the log to <see cref="Rcon.OnLogEntry"/>
    /// </summary>
    /// <remarks>Reqires ServerLog enabled on rcon server.</remarks>
    public bool AllowLogRedirect { get; set; } = false;

    /// <summary>
    /// Gets a bool indicating whether this <see cref="Rcon"/> is authenticated.
    /// </summary>
    public bool IsAuthenticated { get; private set; } = false;

    /// <summary>
    /// Gets or sets a bool value indicating whether to keep this <see cref="Rcon"/> alive.
    /// </summary>
    public bool KeepAlive
    {
        get
        {
            return ( bool )_tcp.Client.GetSocketOption( SocketOptionLevel.Socket, SocketOptionName.KeepAlive );
        }
        set
        {
            _tcp.Client.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.KeepAlive, value );
        }
    }


    /// <summary>
    /// Gets or sets a bool value indicating whether to have no delay <see cref="Rcon"/> alive.
    /// </summary>
    public bool NoDelay
    {
        get
        {
            return ( bool )_tcp.Client.GetSocketOption( SocketOptionLevel.Socket, SocketOptionName.NoDelay );
        }
        set
        {
            _tcp.Client.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.NoDelay, value );
        }
    }


    /// <summary>
    /// Gets or sets a bool value indicating whether to bypass hardware when possible this <see cref="Rcon"/> alive.
    /// </summary>
    public bool UseLoopBack
    {
        get
        {
            return ( bool )_tcp.Client.GetSocketOption( SocketOptionLevel.Socket, SocketOptionName.UseLoopback );
        }
        set
        {
            _tcp.Client.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.UseLoopback, value );
        }
    }

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

        _timer = new System.Windows.Forms.Timer();
        _timer.Interval = 5000;
        _timer.Tick += _timer_Tick;
        _timer.Enabled = true;
        ConnectedPlayers = new List<ASAPlayer>();
    }


    /// <summary>
    /// Disconnect this <see cref="Rcon"/>
    /// </summary>
    /// <returns></returns>

    public bool Disconnect()
    {
        if(_tcp != null)
        {
            try
            { 
                _timer.Enabled = false;
                _tcp.Close();
                _tcp = null;
                IsConnected = false;
                IsAuthenticated = false;
                OnDisconnected?.Invoke();

                return true;
            }
            catch(Exception ex)
            {
                OnError?.Invoke( ex );
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

            var items = rcon.Split("\n").Where(p => !p.Contains("No players connected"));

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
            OnError?.Invoke(new Exception( "Unable to list players" ) );
            return new ASAPlayer[0];
        }
    }

    /// <summary>
    /// Connects to the RCON endpoint using TcpClient.BeginConnect with timeout support.
    /// </summary>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if connected successfully.</returns>
    public async Task<bool> ConnectAsync( CancellationToken cancellation = default )
    {
        EnsureNotDisposed();

        try
        {
            // Check network access
            EnsureInternetAccess();
            OnAvaliability?.Invoke();
            bool available = false;

            // Quick ping check
            try
            {
                using var tempClient = new TcpClient();
                var connectTask = ConnectWithBeginConnectAsync(tempClient, Authentication.Address, Authentication.Port, 3000, cancellation);
                available = await connectTask.ConfigureAwait( false );
            }
            catch
            {
                available = false;
            }

            OnAvaliabilityCompleted?.Invoke( available );

            if ( !available )
            {
                OnError?.Invoke( new Exception( "Server not reachable." ) );
                return false;
            }

            OnConnecting?.Invoke();

            // Actual persistent connection
            _tcp = new TcpClient();
            try
            {
                await ConnectWithBeginConnectAsync( _tcp, Authentication.Address, Authentication.Port, 5000, cancellation ).ConfigureAwait( false );
            }
            catch ( Exception ex )
            {
                _tcp.Dispose();
                OnError?.Invoke( ex );
                return false;
            }

            _stream = _tcp.GetStream();
            _nextRequestId = 0;
            IsConnected = _tcp.Connected;

            OnConnected?.Invoke( IsConnected );
            return true;
        }
        catch ( Exception ex )
        {
            IsConnected = false;
            OnError?.Invoke( ex );
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
            await SendCommandAsync( $"SetMessageOfTheDay {message}" ).ConfigureAwait( false ); ;
            return true;
        }
        catch
        {
            OnError?.Invoke(new Exception( "Unable to set message of the day." ));
            return false;
        }
    }


    public async Task SendMessageToPlayer(string message, ASAPlayer player)
    {
        await SendCommandAsync( $"ServerChatToPlayer {player.Name} {message}", default );
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
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if authentication succeeded.</returns>
    public async Task<bool> AuthenticateAsync( CancellationToken cancellation = default )
    {
        try
        {
            OnAuthenticating?.Invoke();

            EnsureConnected();

            int reqId = GetNextRequestId();
            var authPacket = new Packet(reqId, PacketType.Auth, Authentication.Password);

            // send auth packet
            await SendPacketAsync( authPacket, cancellation ).ConfigureAwait( false );

            // read response
            var response = await ReadPacketAsync(cancellation).ConfigureAwait(false);

            // Some servers return -1 for failed auth; success is if IDs match
            bool success = response.RequestId == reqId;

            IsAuthenticated = success;

            OnAuthenticated?.Invoke( success );
            return success;
        }
        catch ( OperationCanceledException )
        {
            OnError?.Invoke( new TimeoutException( "Authentication was canceled." ) );
            IsAuthenticated = false;
            return false;
        }
        catch ( Exception ex )
        {
            OnError?.Invoke( new Exception( $"Authentication failed: {ex.Message}", ex ) );
            IsAuthenticated = false;
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
            OnError?.Invoke(  new Exception( "Unable to enable cheats." ) );
            return false;
        }
    }

    /// <summary>
    /// Sends a raw command string via RCON and returns the server's textual response.
    /// </summary>
    /// <param name="command">Full command string, e.g. "ListPlayers" or "Broadcast Hello".</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Server's textual response, or empty string on failure.</returns>
    public async Task<string> SendCommandAsync( string command, CancellationToken cancellation = default )
    {
        if ( string.IsNullOrWhiteSpace( command ) )
            throw new ArgumentException( "Command cannot be null or whitespace.", nameof( command ) );

        try
        {
            OnSendingCommand?.Invoke();

            EnsureConnected();

            int reqId = GetNextRequestId();
            var packet = new Packet(reqId, PacketType.ExecCommand, command);

            // send the command
            await SendPacketAsync( packet, cancellation );

            // read the response
            var response = await ReadPacketAsync(cancellation);

            OnCommandSent?.Invoke( command );

            return response?.Body ?? string.Empty;
        }
        catch ( OperationCanceledException )
        {
            OnError?.Invoke( new TimeoutException( $"Sending command '{command}' was canceled." ) );
            return string.Empty;
        }
        catch ( Exception ex )
        {
            OnError?.Invoke( new Exception( $"Unable to send command '{command}': {ex.Message}", ex ) );
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
            await SendCommandAsync( "cheat DoExit" ).ConfigureAwait( false ); ;

            _tcp?.Close();

            Disconnect();
            Dispose();
        }
        catch
        {
            OnError?.Invoke( new Exception( "Could not shutdown server" ) );
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

    private async void _timer_Tick( object sender, EventArgs e )
    {
        try
        {
            EnsureNotDisposed();
            EnsureConnected();

            #region players

            var playersTemp = await GetPlayerList();
            var players     = playersTemp.Where(p => p.ValidPlayer).ToList();

            var joined = players.Except(lastPlayers);
            var left   = lastPlayers.Except(players);

            foreach ( var p in joined )
                OnPlayerJoined?.Invoke( p );

            foreach ( var p in left )
                OnPlayerLeave?.Invoke( p );

            lastPlayers = players;

            ConnectedPlayers = joined.ToList();
            #endregion

            #region log
            if ( AllowLogRedirect )
            {
                var logs = await GetServerLog();

                var current = logs.Except(lastEntries).Where(p => !p.Contains("Server"));
                var previous = lastEntries.Except(logs);

                foreach ( var entry in current )
                {
                    if ( !string.IsNullOrWhiteSpace( entry ) ) 
                        OnLogEntry?.Invoke( new RconLogEntry(entry) );
                }

                lastEntries = current.ToList();
            }
            #endregion
        }
        catch(Exception ex)
        {
            OnError?.Invoke( ex );
            return;
        }
    }

    private int GetNextRequestId()
    {
        return System.Threading.Interlocked.Increment( ref _nextRequestId );
    }

    private void EnsureConnected()
    {
        if ( _tcp == null || _stream == null || !_tcp.Connected )
            throw new InvalidOperationException( "Not Connected" );
    }

    private void EnsureNotDisposed()
    {
        if ( _disposed ) throw new ObjectDisposedException( nameof( Rcon ) );
    }

    private async Task SendPacketAsync( Packet packet, CancellationToken cancellation )
    {
        EnsureNotDisposed();
        EnsureConnected();
        try
        {
            byte[] bytes = packet.ToBytes();
            await _stream!.WriteAsync( bytes, 0, bytes.Length, cancellation ).ConfigureAwait( false );
        }
        catch ( Exception ex )
        {
            OnError?.Invoke( new IOException( $"Failed to send RCON packet: {ex.Message}", ex ) );
        }
    }

    private async Task<Packet> ReadPacketAsync( CancellationToken cancellation )
    {
        try
        {
            EnsureNotDisposed();
            EnsureConnected();

            // Read first packet
            Packet first = await ReadSinglePacketAsync(cancellation).ConfigureAwait(false);
            if ( first == null )
                return new Packet( -1, PacketType.ResponseValue, string.Empty );

            // Buffer for multi-packet responses
            var responseBuilder = new StringBuilder(first.Body);
            int requestId = first.RequestId;

            // Some servers send multiple response packets with same ID
            // Keep reading until we see an empty body or different Request ID.
            while ( _tcp.Available > 0 )
            {
                var next = await ReadSinglePacketAsync(cancellation).ConfigureAwait(false);
                if ( next == null )
                    break;

                // If same request id, it's part of the same message
                if ( next.RequestId == requestId )
                {
                    // Stop if empty or duplicate "end of response" marker
                    if ( string.IsNullOrEmpty( next.Body ) )
                        break;

                    responseBuilder.Append( next.Body );
                }
                else
                {
                    // Different request or noise packet
                    break;
                }

                // Small delay to catch any packets still streaming
                await Task.Delay( 10, cancellation ).ConfigureAwait( false );
            }

            return new Packet( requestId, first.Type, responseBuilder.ToString() );
        }
        catch ( Exception ex )
        {
            OnError?.Invoke( new Exception( "Unable to read RCON packet.", ex ) );
            return new Packet( -1, PacketType.ResponseValue, string.Empty );
        }
    }

    /// <summary>
    /// Reads exactly one packet from the stream.
    /// </summary>
    private async Task<Packet> ReadSinglePacketAsync( CancellationToken cancellation )
    {
        // Read 4-byte length
        byte[] lenBuf = new byte[4];
        await ReadExactlyAsync( lenBuf, 0, 4, cancellation ).ConfigureAwait( false );
        int length = BitConverter.ToInt32(lenBuf, 0);

        if ( length <= 0 )
            return new Packet( -1, PacketType.ResponseValue, string.Empty );

        // Read payload of 'length' bytes
        byte[] payload = new byte[length];
        await ReadExactlyAsync( payload, 0, length, cancellation ).ConfigureAwait( false );

        return Packet.FromPayloadBytes( payload );
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
                OnError?.Invoke( new IOException( "Connection Dropped/Lossed." ) );
            }
            read += n;
        }
    }

    // Check if there is a network connection
    private bool HasNetworkConnection()
    {
        return NetworkInterface.GetIsNetworkAvailable();
    }

    // Check if there is an active internet connection
    private static bool HasInternetConnection()
    {
        try
        {
            using ( Ping ping = new Ping() )
            {
                PingReply reply = ping.Send("8.8.8.8", 3000); // Ping Google DNS
                return reply.Status == IPStatus.Success;
            }
        }
        catch
        {
            return false;
        }
    }

    void EnsureInternetAccess()
    {
        RconNetworkAccess access = new RconNetworkAccess(HasInternetConnection(), HasNetworkConnection());
        OnNetworkAccessCheck?.Invoke( access );

        if ( !access.HasInternetAccess )
        {
            throw new Exception( "No Internet connection avaliable" );
        }
    }

    /// <summary>
    /// Wraps TcpClient.BeginConnect into a Task for async/await usage with timeout and cancellation.
    /// </summary>
    private Task<bool> ConnectWithBeginConnectAsync( TcpClient tcp, string host, int port, int timeoutMs, CancellationToken cancellation )
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        // Start async connection
        var ar = tcp.BeginConnect(host, port, asyncResult =>
        {
            try
            {
                tcp.EndConnect(asyncResult);
                tcs.TrySetResult(true);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
        }, null);

        // Handle timeout
        var timeoutTask = Task.Delay(timeoutMs, cancellation).ContinueWith(t =>
        {
            if (!tcs.Task.IsCompleted)
            {
                try { tcp.Close(); } catch { }
                tcs.TrySetException(new TimeoutException("Connection timed out."));
            }
        }, TaskContinuationOptions.ExecuteSynchronously);

        return tcs.Task;
    }
    #endregion
}