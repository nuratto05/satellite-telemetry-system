using sat_sim.Models;
using System;

namespace sat_sim.Services;

public class PacketEncoder
{
	public PacketEncoder()
    {
    }

    //TODO: encode telemetry from data to bytes, send packet to ground station

    // PACKET STRUCTURE
    // [ 'T' : 1 byte][ 'E' : 1 byte ][ 'L' : 1 byte ][ version : 1 byte][ count : 1 byte ] ( for each sat [ satellite 1 - 44 bytes ])

    // [ 'T' : 1 byte][ 'E' : 1 byte ][ 'L' : 1 byte ] ---> TEL : packet identifier let's reciever know it is our packet
    // [ version : 1 byte] ---> Version : let's reciver know which packet version we are
    // [ count : 1 byte ] ---> Count : number of satellites we are sending
    // [ satellite 1 - 44 bytes ] ---> Satellite : where we store the satellite objects

    public static byte[] EncodeTelemetry(Telemetry tel)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            using var writer = new BinaryWriter(ms);

            //Identifier
            writer.Write((byte)'T');
            writer.Write((byte)'E');
            writer.Write((byte)'L');

            //Version
            writer.Write((byte)1);

            //Count
            writer.Write((byte) 1);

            //Payload - Satellite
            writer.Write(tel.SatelliteId);
            writer.Write(tel.Timestamp.ToUniversalTime().Ticks);
            writer.Write(tel.Longitude);
            writer.Write(tel.Latitude);
            writer.Write(tel.Altitude);
            writer.Write(tel.Velocity);
            writer.Write(tel.Temperature);
            writer.Write(tel.BatteryLevel);

            return ms.ToArray();

        }
    }
}
