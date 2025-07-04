using BedrockProtocol.Types;
using RakSharp;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol;

public class GamePacket : BedrockPacket {
    
    public override Info.BedrockPackets PacketId => Info.BedrockPackets.GamePacket;
    public override Compression.Algorithm CompressionAlgorithm { get; set; }

    public const int PidMask = 255;
    public const int SubClientIdMask = 3;
    public const int SenderSubClientIdShift = 10;
    public const int RecipientSubClientIdShift = 12;
    
    public int SubClientId { get; set; }
    public int SubTargetId { get; set; }
    public byte[]? Payload { get; set; }
    
    protected override void WriteHeader(BinaryWriter writer) {
        
        var header = (int)PacketId | (SubClientId << SenderSubClientIdShift) | (SubTargetId << RecipientSubClientIdShift);
        writer.WriteByte((byte)header);
        writer.WriteByte((byte)(Payload?.Length ?? 0));
    }

    public override void ReadHeader(BinaryReader reader) {

        var header = reader.ReadVarUInt();
        var packetId = (header & PidMask) | 128;

        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, (int)packetId, nameof(GamePacket));
        }

        SubClientId = (int)((header >> SenderSubClientIdShift) & SubClientIdMask);
        SubTargetId = (int)((header >> RecipientSubClientIdShift) & SubClientIdMask);
    }

    protected override void WritePayload(BinaryWriter writer) {
        writer.Write(Payload);
    }

    protected override void ReadPayload(BinaryReader reader) {
        Payload = reader.ReadRemainingBytes();
    }
    
    public static (GamePacket packet, byte[] buffer) Create(int subClientId, int subTargetId, byte[] payload) {
        
        return BedrockPacket.Create<GamePacket>(packet => {
            packet.SubClientId = subClientId;
            packet.SubTargetId = subTargetId;
            packet.Payload = payload;
        });
    }
}