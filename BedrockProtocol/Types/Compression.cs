namespace BedrockProtocol.Types;

public class Compression {

    public enum Algorithm {
        Zlib = 0,
        Snappy = 1,
        None = 255
    }
}