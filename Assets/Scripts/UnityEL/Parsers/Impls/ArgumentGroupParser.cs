using UnityEngine;
using UnityEditor;

public class ArgumentGroupParser : SingleCharacterParser {
    public ArgumentGroupParser() : base('{') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // The argument group becomes the new parent token
        ArgumentGroupToken group = new ArgumentGroupToken(symbolPos);
        compiler.Parent.AddChild(group);
        compiler.ParentTokens.Push(group);

        // Keep parsing until we are closed
        while (!group.IsClosed) {
            if (!compiler.ParseNextToken()) {
                throw new ParserException(group, "Unclosed argument group");
            }
        }

        return true;
    }
}