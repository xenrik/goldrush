using UnityEngine;
using UnityEditor;

public class DivisionParser : BinaryTokenParser {
    public override string Name { get { return "division"; } }

    public DivisionParser() : 
        base(new SingleCharacterParser('/')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        return new DivisionToken(symbolPos, lhs, rhs);
    }
}