using System;
using System.Collections.Generic;
using System.Text;

namespace ASARcon;
/// <summary>
/// Represents a Source RCON packet (request or response).
/// Handles serializing to bytes and deserializing from bytes.
/// </summary>
public sealed class Packet
{
    /// <summary>
    /// The request id associated with this packet. Server echoes this back.
    /// </summary>
    public int RequestId { get; }

    /// <summary>
    /// The packet type (Auth, ExecCommand, etc).
    /// </summary>
    public PacketType Type { get; }

    /// <summary>
    /// The ASCII body/text contained in the packet.
    /// </summary>
    public string Body { get; }

    /// <summary>
    /// Creates a new RconPacket.
    /// </summary>
    /// <param name="requestId">Request id.</param>
    /// <param name="type">Packet type.</param>
    /// <param name="body">Packet body (ASCII text).</param>
    public Packet( int requestId, PacketType type, string body )
    {
        RequestId = requestId;
        Type = type;
        Body = body ?? string.Empty;
    }

    /// <summary>
    /// Serializes this packet into the wire format bytes.
    /// Format:
    /// [int32 length][int32 requestId][int32 type][body bytes][0x00][0x00]
    /// where length = 4(requestId)+4(type)+body.Length+2(nulls)
    /// </summary>
    /// <returns>Byte array ready to send over TCP.</returns>
    public byte[] ToBytes()
    {
        byte[] bodyBytes = Encoding.ASCII.GetBytes(Body);
        int length = 4 + 4 + bodyBytes.Length + 2;

        using MemoryStream ms = new MemoryStream(4 + length);
        using BinaryWriter bw = new BinaryWriter(ms, Encoding.ASCII, leaveOpen: true);

        bw.Write( length );                     // total payload length (int32)
        bw.Write( RequestId );                  // request id
        bw.Write( ( int )Type );                  // packet type (as int)
        bw.Write( bodyBytes );                  // body
        bw.Write( ( byte )0 );                    // null terminator
        bw.Write( ( byte )0 );                    // second null

        return ms.ToArray();
    }

    /// <summary>
    /// Builds a RconPacket from the received payload bytes (payload = bytes after length field).
    /// </summary>
    /// <param name="payload">Payload bytes (length bytes already consumed).</param>
    /// <returns>Parsed RconPacket.</returns>
    public static Packet FromPayloadBytes( byte[] payload )
    {
        if ( payload == null ) throw new ArgumentNullException( nameof( payload ) );
        if ( payload.Length < 10 ) throw new ArgumentException( "Payload too small to be a valid RCON packet.", nameof( payload ) );

        int id = BitConverter.ToInt32(payload, 0);
        int typeValue = BitConverter.ToInt32(payload, 4);

        // body length = payload.Length - 8 (id+type) - 2 (two nulls)
        int bodyLen = payload.Length - 10;
        if ( bodyLen < 0 ) bodyLen = 0;

        string body = Encoding.ASCII.GetString(payload, 8, bodyLen);
        return new Packet( id, ( PacketType )typeValue, body );
    }
}