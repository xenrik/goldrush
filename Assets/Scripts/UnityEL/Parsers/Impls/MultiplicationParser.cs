using UnityEngine;
using UnityEditor;

public class MultiplicationParser : BinaryTokenParser {
    public override string Name { get { return "multiplication"; } }

    public MultiplicationParser() : 
        base(new SingleCharacterParser('*')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        return new MultiplicationToken(symbolPos, lhs, rhs);
    }
}