using UnityEngine;
using UnityEditor;

public class SubtractionParser : BinaryTokenParser {
    public override string Name { get { return "subtraction"; } }

    public SubtractionParser() : 
        base(new SingleCharacterParser('-')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        return new SubtractionToken(symbolPos, lhs, rhs);
    }
}