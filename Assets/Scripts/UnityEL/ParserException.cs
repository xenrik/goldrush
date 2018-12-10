using System;

public class ParserException : Exception {
    public ParserException(TokenImpl source, string message) : 
        base($"{source.DebugName}{message}") {
    }

    public ParserException(TokenImpl source, string message, Exception cause) : 
        base($"{source.DebugName}{message}", cause) {
    }

    public ParserException(int position, string message) :
        base($"<@{position}>{message}") {
    }

    public ParserException(string contextName, int position, string message) :
        base($"<{contextName}@{position}>{message}") {
    }

    public ParserException(string contextName, int position, string message, Exception cause) :
        base($"<{contextName}@{position}>{message}", cause) {
    }
}
