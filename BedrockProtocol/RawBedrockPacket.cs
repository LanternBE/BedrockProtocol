using BedrockProtocol.Types;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol;

public class RawBedrockPacket : BedrockPacket {

    public Info.BedrockPackets PacketType { get; private set; }
    public override Info.BedrockPackets PacketId => PacketType;
    public override Compression.Algorithm CompressionAlgorithm { get; set; }

    public byte[] Payload { get; private set; } = [];

    protected override void WriteHeader(BinaryWriter writer) {
        writer.WriteVarUInt((uint)PacketType);
    }

    public override void ReadHeader(BinaryReader reader) {
        PacketType = (Info.BedrockPackets)reader.ReadVarUInt();
    }

    protected override void WritePayload(BinaryWriter writer) {
        writer.Write(Payload);
    }

    protected override void ReadPayload(BinaryReader reader) {
        Payload = reader.ReadRemainingBytes();
    }

    public static (RawBedrockPacket packet, byte[] buffer) Create(
        Info.BedrockPackets packetId,
        Compression.Algorithm compressionAlgorithm,
        byte[] payload
    ) {
        return BedrockPacket.Create<RawBedrockPacket>(packet => {
            packet.PacketType = packetId;
            packet.CompressionAlgorithm = compressionAlgorithm;
            packet.Payload = payload;
        });
    }
}
