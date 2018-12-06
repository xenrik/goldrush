using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface TokenParser {
    RawToken Consume(char[] chars, ref int pos);
}
