using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserException : System.Exception {
    public ParserException(string message) : base(message) {
    }

    public ParserException(string message, Exception innerException) : base(message, innerException) {
    }
}
