using System;
using System.Collections.Generic;
using System.Text;

namespace ASARcon;

/// <summary>
/// Represents the Source RCON packet type values used by ARK/Source RCON.
/// These integer values are the protocol numbers used on the wire.
/// </summary>
public enum PacketType : int
{
    /// <summary>Unused / value 0 (sometimes used as a simple response).</summary>
    ResponseValue = 0,

    /// <summary>Server data exec command and exec response (value 2).</summary>
    ExecCommand = 2,

    /// <summary>Authentication request (value 3).</summary>
    Auth = 3,

    /// <summary>Authentication response (server side uses value 2 for responses).</summary>
    AuthResponse = 2
}