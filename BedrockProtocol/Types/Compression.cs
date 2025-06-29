namespace BedrockProtocol.Types;

public class Compression {

    public enum Algorithm {
        Unset = -1,
        Zlib = 0,
        Snappy = 1,
        None = 255
    }
}