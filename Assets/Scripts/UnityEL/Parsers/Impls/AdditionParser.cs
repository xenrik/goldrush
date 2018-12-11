using UnityEngine;
using UnityEditor;

public class AdditionParser : BinaryTokenParser {
    public override string Name { get { return "addition"; } }

    public AdditionParser() : 
        base(new SingleCharacterParser('+')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        return new AdditionToken(symbolPos, lhs, rhs);
    }
}