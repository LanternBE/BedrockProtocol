using BedrockProtocol.Types;
using RakSharp;
using RakSharp.Packet;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol;

public class PlayStatus : BedrockPacket {
    
    public override Info.BedrockPackets PacketId => Info.BedrockPackets.PlayStatus;
    public override Compression.Algorithm CompressionAlgorithm { get; set; }

    public Types.PlayStatus State { get; private set; }
    
    protected override void WriteHeader(BinaryWriter writer) {
        
        writer.WriteByte((byte)PacketId);
        writer.WriteByte((byte)CompressionAlgorithm);
    }

    public override void ReadHeader(BinaryReader reader) {
        
        var packetId = reader.ReadByte();
        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, packetId, nameof(PlayStatus));
        }
        
        CompressionAlgorithm = (Compression.Algorithm)reader.ReadByte();
    }

    protected override void WritePayload(BinaryWriter writer) {
        writer.WriteIntBigEndian((int)State);
    }

    protected override void ReadPayload(BinaryReader reader) {
        State = (Types.PlayStatus)reader.ReadIntBigEndian();
    }
    
    public static (PlayStatus packet, byte[] buffer) Create(Compression.Algorithm compressionAlgorithm, Types.PlayStatus playStatus) {
        
        return BedrockPacket.Create<PlayStatus>(packet => {
            packet.CompressionAlgorithm = compressionAlgorithm;
            packet.State = playStatus;
        });
    }
}