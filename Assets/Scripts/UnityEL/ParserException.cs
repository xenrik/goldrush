using System;

public class ParserException : Exception {
    public ParserException(RawToken source, string message) : 
        base($"{source.DebugName}{message}") {
    }

    public ParserException(RawToken source, string message, Exception cause) : 
        base($"{source.DebugName}{message}", cause) {
    }

    public ParserException(string contextName, int position, string message) :
        base($"<{contextName}@{position}>{message}") {
    }

    public ParserException(string contextName, int position, string message, Exception cause) :
        base($"<{contextName}@{position}>{message}", cause) {
    }
}
