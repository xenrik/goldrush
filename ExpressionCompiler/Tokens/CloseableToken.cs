/**
 * Simple interface that tokens can implement if they support closing
 */
public interface CloseableToken {
    /** Returns true if the token has been closed */
    bool IsClosed { get; }

    /** Close the token */
    void Close();
}