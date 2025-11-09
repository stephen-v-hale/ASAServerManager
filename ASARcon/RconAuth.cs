using System;
using System.Collections.Generic;
using System.Text;

namespace ASARcon;

/// <summary>
/// Represents a connection authority object for an RCON endpoint.
/// Contains the server address, RCON port, and RCON password.
/// </summary>
public sealed class Authentication
{
    /// <summary>
    /// Hostname or IPv4/IPv6 address of the RCON server.
    /// (Example: "127.0.0.1" or "myserver.ddns.net")
    /// </summary>
    public string Address { get; }

    /// <summary>
    /// RCON password (server admin password).
    /// This must match the ServerAdminPassword value in GameUserSettings.ini.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// RCON TCP port number.
    /// Usually 27020 for ARK ASA, but can vary if user configured different.
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// Creates a new <see cref="Authentication"/> container holding the connection information
    /// needed to authenticate to the server.
    /// </summary>
    /// <param name="address">Host IP or hostname.</param>
    /// <param name="password">RCON password used to authenticate.</param>
    /// <param name="port">The RCON TCP port.</param>
    public Authentication( string address, string password, int port )
    {
        Address = address;
        Password = password;
        Port = port;
    }
}