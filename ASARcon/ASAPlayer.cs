using System;
using System.Collections.Generic;
using System.Text;

namespace ASARcon;

/// <summary>
/// Represents a player in the ASA system with an index, name, and ID.
/// </summary>
public class ASAPlayer
{
    /// <summary>
    /// Gets the player's index number.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the player's full name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the player's unique ID.
    /// </summary>
    public string ID { get; }

    /// <summary>
    /// Gets a bool value indecating wheather this <see cref="ASAPlayer"/> is a player. and not "No Players Connected".
    /// </summary>
    public bool ValidPlayer { get; }

    /// <summary>
    /// Initializes a new instance of the ASAPlayer class from a formatted string.
    /// Expected format: "index. name, id"
    /// Example: "1. John Doe, 12345"
    /// </summary>
    /// <param name="asaPlayerString">The formatted string containing player info.</param>
    public  ASAPlayer( string asaPlayerString )
    {
        if ( asaPlayerString.Contains("No players connected" ))
        {
            Index = 0;
            Name = "No players connected";
            ID = "";
            ValidPlayer = false;
        }
        else
        { 
            try
            {
                // Split by ". " to separate index from the rest
                var indexSplit = asaPlayerString.Split(new[] { ". " }, 2, StringSplitOptions.None);
                Index = int.Parse( indexSplit[0] );

                // Split by ", " to separate name and ID
                var nameIdSplit = indexSplit[1].Split(new[] { ", " }, 2, StringSplitOptions.None);
                Name = nameIdSplit[0];
                ID = nameIdSplit[1];
                ValidPlayer = true;

            }
            catch
            {
                Index = 0;
                Name = "Unknown";
                ID = "Unknown";
                ValidPlayer = false;
            }
        }
    }

    public async void SendMessage( string message ) => await Rcon.Instance.SendMessageToPlayer( message, this ).ConfigureAwait( false );
    public async void Kick() => await Rcon.Instance.SendCommandAsync( Command.KickPlayer, default, this.ID ).ConfigureAwait( false );
    public async void Ban() => await Rcon.Instance.SendCommandAsync( Command.BanPlayer, default, this.ID ).ConfigureAwait( false );
    public async void WhiteList() => await Rcon.Instance.SendCommandAsync( "AllowPlayerToJoinNoCheck " + ID ).ConfigureAwait( false );
    public async void Blacklist() => await Rcon.Instance.SendCommandAsync( "DisallowPlayerToJoinNoCheck " + ID ).ConfigureAwait( false );

    public override bool Equals( object obj )
    {
        if ( obj is not ASAPlayer other )
            return false;

        return StringComparer.Ordinal.Equals( ID, other.ID );
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode( ID ?? "" );
    }
}
