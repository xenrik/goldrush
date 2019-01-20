using System;

public class NoSuchPropertyException : ParserException {
    public NoSuchPropertyException(TokenImpl source, string message) : base(source, message) { }
    public NoSuchPropertyException(TokenImpl source, string message, Exception cause) : base(source, message, cause) { }
    public NoSuchPropertyException(int position, string message) : base(position, message) { }
    public NoSuchPropertyException(string contextName, int position, string message) : base(contextName, position, message) { }
    public NoSuchPropertyException(string contextName, int position, string message, Exception cause) : base(contextName, position, message, cause) { }
    public NoSuchPropertyException(string message) : base(message) { }
}
