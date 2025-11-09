using System;
using System.Collections.Generic;
using System.Text;

namespace ASARcon;

/// <summary>
/// Enum of well-known RCON commands used by ARK / Source-based servers.
/// This enum is used for typing; convert to the actual command string using <see cref="RconCommandExtensions.ToCommandString"/>.
/// </summary>
public enum Command
{
    Broadcast,
    GetChat,
    GetGameLog,
    ServerChat,
    ServerChatTo,
    ServerChatToPlayer,
    KickPlayer,
    BanPlayer,
    GetAllState,
    DoRestartLevel,
    Raw
}

/// <summary>
/// Helper extensions to convert <see cref="RconCommand"/> into actual RCON command strings.
/// </summary>
public static class CommandExtensions
{
    /// <summary>
    /// Convert a typed command + args into the actual command text to send via RCON.
    /// Use RconCommand.Raw to send arbitrary commands (arg0 = the raw string).
    /// </summary>
    /// <param name="cmd">Typed command.</param>
    /// <param name="args">Arguments for the command; formatting depends on command type.</param>
    /// <returns>Command string suitable for sending to server.</returns>
    public static string ToCommandString( this Command cmd, params object[] args )
    {
        args ??= Array.Empty<object>();
        return cmd switch
        {
            Command.Broadcast =>
                args.Length > 0 ? $"Broadcast {EscapeArg( args[0]?.ToString() ?? string.Empty )}" : throw new ArgumentException( "Broadcast requires a message arg." ),

            Command.GetChat =>
                args.Length > 0 && int.TryParse( args[0]?.ToString(), out var idx ) ? $"GetChat {idx}" : throw new ArgumentException( "GetChat requires an integer index arg." ),

            Command.GetGameLog =>
                args.Length > 0 && int.TryParse( args[0]?.ToString(), out var idx2 ) ? $"GetGameLog {idx2}" : throw new ArgumentException( "GetGameLog requires an integer index arg." ),

            Command.ServerChat => args.Length > 0 ? $"SendChat {EscapeArg( args[0]?.ToString() )}" : throw new ArgumentException( "ServerChat requires a message arg" ),
            Command.ServerChatTo => args.Length > 0 ? $"ServerChatTo {EscapeArg( args[0]?.ToString() )} {EscapeArg( args[1]?.ToString() )} " : throw new ArgumentException( "ServerChatTo requires a id and message arg." ),
            Command.ServerChatToPlayer => args.Length > 0 ? $"ServerChatToPlayer {EscapeArg( args[0]?.ToString() )} {EscapeArg( args[1]?.ToString() )} " : throw new ArgumentException( "ServerChatToPlayer requires a id and message arg" ),
            Command.GetAllState => $"GetAllState",
            Command.DoRestartLevel => $"DoRestartLevel",
            Command.Raw =>
                args.Length > 0 ? args[0]?.ToString() ?? string.Empty : throw new ArgumentException( "Raw requires the full command string as arg." ),

            _ => throw new NotSupportedException( $"RconCommand {cmd} not supported in conversion." )
        };
    }

    /// <summary>
    /// Simple arg escaping. If the argument contains spaces, wraps in quotes.
    /// </summary>
    private static string EscapeArg( string s )
    {
        if ( string.IsNullOrEmpty( s ) ) return "\"\"";
        if ( s.Contains( ' ' ) || s.Contains( '"' ) )
        {
            // Replace any " in the string with \" and wrap in quotes
            var escaped = s.Replace("\"", "\\\"");
            return $"\"{escaped}\"";
        }
        return s;
    }
}