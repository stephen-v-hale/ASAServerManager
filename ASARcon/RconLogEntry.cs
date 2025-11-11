using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;

namespace ASARcon;

public enum LogEntryType
{
    /// <summary>
    /// The log value is just text.
    /// </summary>
    Text,

    /// <summary>
    /// The log value is describing a player leave.
    /// </summary>
    PlayerLeft,

    /// <summary>
    /// The log value is describing a player joined.
    /// </summary>
    PlayerJoined,

    /// <summary>
    /// The log value is describing a player sent a chat.
    /// </summary>
    PlayerMessage,

    /// <summary>
    /// The log value is describing the server sent a chat.
    /// </summary>
    Message,

    /// <summary>
    /// The log value is describing the server received an admin command.
    /// </summary>
    AdminCommand,

    /// <summary>
    /// The log value is describing that a player tamed a dino.
    /// </summary>
    Tame,

    /// <summary>
    /// The log value is describing that a tribe has changed.
    /// </summary>
    Tribe,

    /// <summary>
    /// The log value is describing that a player has died.
    /// </summary>
    PlayerDeath,

    /// <summary>
    /// The log value is describing that the world has saved or is saving.
    /// </summary>
    Worldsave,
}


public class RconLogEntry
{
    /// <summary>
    /// Gets the date time this entry was made.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Gets the value of the log.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the <see cref="LogEntryType"/>
    /// </summary>
    public LogEntryType Type { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="RconLogEntry"/>
    /// </summary>
    /// <param name="logEntryString"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public RconLogEntry( string logEntryString )
    {
        if ( string.IsNullOrEmpty( logEntryString ) )
            throw new ArgumentNullException( nameof( logEntryString ) );

        // If it doesn't contain a timestamp, just store it
        if ( !logEntryString.Contains( "_" ) || !logEntryString.Contains( ":" ) )
        {
            Time = DateTime.Now;
            Value = logEntryString;
            Type = DetectType( Value );
            return;
        }

        // Split only on the first colon (date/time separator)
        int firstColonIndex = logEntryString.IndexOf(':');
        string timestampPart = logEntryString.Substring(0, firstColonIndex).Trim();
        string valuePart = logEntryString.Substring(firstColonIndex + 1).Trim();

        // Separate date and time
        var parts = timestampPart.Split('_');
        var date = parts[0].Split('.');
        var time = parts[1].Split('.');

        int year = int.Parse(date[0]);
        int month = int.Parse(date[1]);
        int day = int.Parse(date[2]);

        int hour = int.Parse(time[0]);
        int minute = int.Parse(time[1]);
        int second = int.Parse(time[2]);

        Time = new DateTime( year, month, day, hour, minute, second );
        Value = valuePart;
        Type = DetectType( Value );
    }

    private static LogEntryType DetectType( string value )
    {
        if ( string.IsNullOrWhiteSpace( value ) )
            return LogEntryType.Text;

        // Player joined
        if ( Regex.IsMatch( value, @"^joined this ARK!", RegexOptions.IgnoreCase ) )
            return LogEntryType.PlayerJoined;

        // Player left
        if ( Regex.IsMatch( value, @"^left this ARK!", RegexOptions.IgnoreCase ) )
            return LogEntryType.PlayerLeft;

        // Player chat message
        if ( Regex.IsMatch( value, @"\(Human\):", RegexOptions.IgnoreCase ) )
            return LogEntryType.PlayerMessage;

        // Server message
        if ( Regex.IsMatch( value, @"^SERVER:", RegexOptions.IgnoreCase ) )
            return LogEntryType.Message;

        // Admin command
        if ( Regex.IsMatch( value, @"^AdminCmd:", RegexOptions.IgnoreCase ) )
            return LogEntryType.AdminCommand;

        if ( Regex.IsMatch( value, @"^Tamed a:", RegexOptions.IgnoreCase ) )
            return LogEntryType.Tame;

        if ( Regex.IsMatch( value, @"^Tribe", RegexOptions.IgnoreCase ) )
            return LogEntryType.Tribe;

        if ( Regex.IsMatch( value, @"^killed by", RegexOptions.IgnoreCase ) )
            return LogEntryType.PlayerDeath;


        if ( Regex.IsMatch( value, @"^World Save Complete", RegexOptions.IgnoreCase ) || Regex.IsMatch( value, @"^Saving", RegexOptions.IgnoreCase ) )
            return LogEntryType.Worldsave;

        return LogEntryType.Text;
    }
}