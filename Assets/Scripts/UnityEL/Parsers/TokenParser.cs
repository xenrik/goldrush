using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface TokenParser {
    /**
     * If possible, parse a token from the char array at the given position. The
     * token should be added to the given parent. 
     * 
     * The method may update the parent if subsequent children should be added
     * to a different one.
     * 
     * Return true if a token was parsed, false otherwise.
     */
    bool Parse(char[] chars, ref int pos, ref RawToken parent);
}
