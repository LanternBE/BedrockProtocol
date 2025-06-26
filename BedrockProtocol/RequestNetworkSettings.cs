using BedrockProtocol.Types;
using RakSharp;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol;

public class RequestNetworkSettings : BedrockPacket {
    
    public override Info.BedrockPackets PacketId => Info.BedrockPackets.RequestNetworkSettings;
    public override Compression.Algorithm CompressionAlgorithm { get; set; }

    public int ProtocolVersion { get; private set; }
    
    protected override void WriteHeader(BinaryWriter writer) {
        writer.WriteByte((byte)PacketId);
    }

    public override void ReadHeader(BinaryReader reader) {
        
        var packetId = reader.ReadByte();
        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, packetId, nameof(RequestNetworkSettings));
        }
        
        CompressionAlgorithm = (Compression.Algorithm)reader.ReadByte();
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