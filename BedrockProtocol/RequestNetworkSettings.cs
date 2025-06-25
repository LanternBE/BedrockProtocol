using BedrockProtocol.Types;
using RakSharp;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol;

public class RequestNetworkSettings : BedrockPacket {
    
    public override Info.BedrockPackets PacketId => Info.BedrockPackets.RequestNetworkSettings;
    
    public int ProtocolVersion { get; private set; }
    
    protected override void WriteHeader(BinaryWriter writer) {
        writer.WriteByte((byte)PacketId);
    }

    public override void ReadHeader(BinaryReader reader) {
        
        var packetId = reader.ReadByte();
        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, packetId, nameof(RequestNetworkSettings));
        }
        
        reader.ReadByte(); // This is the buffer after reading the PacketId: 01-00-00-03-32, idk whats 0x01, so im gonna skip it with this line:
    }

    protected override void WritePayload(BinaryWriter writer) {
        writer.WriteIntBigEndian(ProtocolVersion);
    }

    protected override void ReadPayload(BinaryReader reader) {
        ProtocolVersion = reader.ReadIntBigEndian();
    }
    
    public static (RequestNetworkSettings packet, byte[] buffer) Create(int protocolVersion) {
        
        return BedrockPacket.Create<RequestNetworkSettings>(packet => {
            packet.ProtocolVersion = protocolVersion;
        });
    }
}