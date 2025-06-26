namespace BedrockProtocol;

public static class Info {
    
    public static List<int> SupportedProtocols { get; } = [
        818 // 1.21.92
    ];

    public enum BedrockPackets {
        
        Login = 1,
        PlayStatus = 2,
        Disconnect = 5,
        ResourcePacksInfo = 6,
        NetworkSettings = 143,
        RequestNetworkSettings = 193,
        GamePacket = 254
    }
}