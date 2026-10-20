using sat_ground_station.Models;
using System;

namespace sat_ground_station.Service;
public class PacketDecoder
{
	public PacketDecoder()
	{
	}

	public static Telemetry Decode(byte[] packet)
	{

		using (MemoryStream ms  = new MemoryStream(packet))
		{
			using var reader = new BinaryReader(ms);

            //Packet Identifier
            byte id1 = reader.ReadByte();
            byte id2 = reader.ReadByte();
            byte id3 = reader.ReadByte();
            if (id1 != (byte) 'T' || id2 != (byte)'E' || id3 != (byte)'L')
			{
				throw new InvalidDataException("Invalid Telemetry Packet");
			}

            //Packter Version
			byte version = reader.ReadByte();
            if (version != 1)
            {
                throw new InvalidDataException($"Unknwon Version {version}");
            }

            //Size of payload ( count * 44 bytes)
			byte size = reader.ReadByte();

            //Telemetry data
            int satelliteId = reader.ReadInt32();

            long ticks = reader.ReadInt64();
            DateTime timeStamp = new DateTime(ticks, DateTimeKind.Utc);

            double longitude = reader.ReadDouble();
            double latitude = reader.ReadDouble();
            int altitude = reader.ReadInt32();
            int velocity = reader.ReadInt32();
            int temperature = reader.ReadInt32();
            int batteryLevel = reader.ReadInt32();

            //Telemetry Object
            Telemetry telemetry = new Telemetry(satelliteId, timeStamp, longitude, latitude, altitude, velocity, temperature, batteryLevel);

            return telemetry;

        }

	}
}
