using System.Buffers;
using BinaryReader = RakSharp.Binary.BinaryReader;
using BinaryWriter = RakSharp.Binary.BinaryWriter;

namespace BedrockProtocol.Types;

public abstract class BedrockPacket {

    public abstract Info.BedrockPackets PacketId { get; }

    protected abstract void WriteHeader(BinaryWriter writer);
    public abstract void ReadHeader(BinaryReader reader);
    
    protected abstract void WritePayload(BinaryWriter writer);
    protected abstract void ReadPayload(BinaryReader reader);
    
    public void Write(BinaryWriter writer) {
        
        WriteHeader(writer);
        WritePayload(writer);
    }
    
    public void Read(BinaryReader reader) {
        
        ReadHeader(reader);
        ReadPayload(reader);
    }
    
    protected static (T packet, byte[] buffer) Create<T>(Action<T> initialize) where T : BedrockPacket, new() {
        
        var packet = new T();
        initialize(packet);
        
        var tempBuffer = ArrayPool<byte>.Shared.Rent(1492);
        try {
            var writer = new BinaryWriter(tempBuffer);
            packet.Write(writer);
            
            var buffer = new byte[writer.Position];
            Array.Copy(tempBuffer, buffer, writer.Position);
            
            return (packet, buffer);
        } finally {
            ArrayPool<byte>.Shared.Return(tempBuffer);
        }
    }
}