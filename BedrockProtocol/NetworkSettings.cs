using BedrockProtocol.Types;
using RakSharp;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol;

public class NetworkSettings : BedrockPacket {
    
    public override Info.BedrockPackets PacketId => Info.BedrockPackets.NetworkSettings;
    public override Compression.Algorithm CompressionAlgorithm { get; set; }

    public ushort CompressionThreshold { get; private set; }
    public ushort CompressionAlgorithmMethod { get; private set; }
    public bool ClientThrottleEnabled { get; private set; }
    public byte ClientThrottleThreshold { get; private set; }
    public float ClientThrottleScalar { get; private set; }
    
    protected override void WriteHeader(BinaryWriter writer) {
        writer.WriteVarInt((byte)PacketId);
    }

    public override void ReadHeader(BinaryReader reader) {
        
        var packetId = reader.ReadVarInt();
        if (packetId != (int)PacketId) {
            throw new RakSharpException.InvalidPacketIdException((uint)PacketId, packetId, nameof(NetworkSettings));
        }
    }

    protected override void WritePayload(BinaryWriter writer) {
        
        writer.WriteUnsignedShortLittleEndian(CompressionThreshold);
        writer.WriteUnsignedShortLittleEndian(CompressionAlgorithmMethod);
        writer.WriteBoolean(ClientThrottleEnabled);
        writer.WriteByte(ClientThrottleThreshold);
        writer.WriteFloatLittleEndian(ClientThrottleScalar);
    }

    protected override void ReadPayload(BinaryReader reader) {
        
        CompressionThreshold = reader.ReadUnsignedShortLittleEndian();
        CompressionAlgorithmMethod = reader.ReadUnsignedShortLittleEndian();
        ClientThrottleEnabled = reader.ReadBoolean();
        ClientThrottleThreshold = reader.ReadByte();
        ClientThrottleScalar = reader.ReadFloatLittleEndian();
    }
    
    public static (NetworkSettings packet, byte[] buffer) Create(Compression.Algorithm compressionAlgorithm, ushort compressionThreshold = 0, ushort compressionAlgorithmMethod = 0, bool clientThrottleEnabled = false, byte clientThrottleThreshold = 0, float clientThrottleScalar = 0.0f) {
        
        return BedrockPacket.Create<NetworkSettings>(packet => {
            packet.CompressionAlgorithm = compressionAlgorithm;
            packet.CompressionThreshold = compressionThreshold;
            packet.CompressionAlgorithmMethod = compressionAlgorithmMethod;
            packet.ClientThrottleEnabled = clientThrottleEnabled;
            packet.ClientThrottleThreshold = clientThrottleThreshold;
            packet.ClientThrottleScalar = clientThrottleScalar;
        });
    }
}