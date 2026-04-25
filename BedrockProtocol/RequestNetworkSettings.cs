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
        writer.WriteVarUInt((uint)PacketId);
    }

    public override void ReadHeader(BinaryReader reader) {
        
        var packetId = (int)reader.ReadVarUInt();
        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, packetId, nameof(RequestNetworkSettings));
        }
    }

    protected override void WritePayload(BinaryWriter writer) {
        writer.WriteIntBigEndian(ProtocolVersion);
    }

    protected override void ReadPayload(BinaryReader reader) {
        ProtocolVersion = reader.ReadIntBigEndian();
    }
    
    public static (RequestNetworkSettings packet, byte[] buffer) Create(Compression.Algorithm compressionAlgorithm, int protocolVersion) {
        
        return BedrockPacket.Create<RequestNetworkSettings>(packet => {
            packet.CompressionAlgorithm = compressionAlgorithm;
            packet.ProtocolVersion = protocolVersion;
        });
    }
}
