using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyAccessorParser : SingleCharacterParser<PropertyAccessorToken> {
    public PropertyAccessorParser() : base('.') {
    }
}
