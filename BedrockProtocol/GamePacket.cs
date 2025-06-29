using System.IO.Compression;
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
        // Alternativa: scrivi direttamente il byte dell'ID del pacchetto
        writer.WriteByte((byte)PacketId);
        // Poi scrivi l'algoritmo di compressione
        writer.WriteByte((byte)CompressionAlgorithm);
    }

    public override void ReadHeader(BinaryReader reader) {
        var packetId = reader.ReadByte();
        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, packetId, nameof(NetworkSettings));
        }
        
        CompressionAlgorithm = (Compression.Algorithm)reader.ReadByte();
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