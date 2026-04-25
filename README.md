# BedrockProtocol
Minecraft Bedrock Edition Protocol Fully Written In C#, using RakSharp as base.

Supported protocol versions include 944 (Bedrock 26.10).

## Protocol 944 coverage
- Complete packet ID registry for protocol 944 is available in `PacketRegistry` (`IdToName`/`NameToId`).
- `Info.BedrockPackets` contains the full packet enum mapping for protocol 944.
- `RawBedrockPacket` allows reading/writing any mapped packet as raw payload.
