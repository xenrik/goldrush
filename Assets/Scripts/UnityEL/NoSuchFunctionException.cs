using System;

public class NoSuchFunctionException : ParserException {
    public NoSuchFunctionException(TokenImpl source, string message) : base(source, message) { }
    public NoSuchFunctionException(TokenImpl source, string message, Exception cause) : base(source, message, cause) { }
    public NoSuchFunctionException(int position, string message) : base(position, message) { }
    public NoSuchFunctionException(string contextName, int position, string message) : base(contextName, position, message) { }
    public NoSuchFunctionException(string contextName, int position, string message, Exception cause) : base(contextName, position, message, cause) { }
    public NoSuchFunctionException(string message) : base(message) { }
}
