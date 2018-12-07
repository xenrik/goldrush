using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface TokenParser {
    /**
     * If possible, parse a token from the char array at the given position. The
     * token should be added to the given container. 
     * 
     * The method should return the container that will be used for subsequent tokens
     * (which can be the container supplied to the method)
     */
    RawToken Parse(RawToken container, char[] chars, ref int pos);
}
