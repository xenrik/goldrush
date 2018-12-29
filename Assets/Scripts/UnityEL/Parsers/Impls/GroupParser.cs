using UnityEngine;
using UnityEditor;

public class GroupParser : SingleCharacterParser {
    public GroupParser() : base('(') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // The group becomes the new parent token
        GroupToken group = new GroupToken(symbolPos);
        compiler.Parent.AddChild(group);
        compiler.ParentTokens.Push(group);

        // Keep parsing until we are closed
        while (!group.IsClosed) {
            if (!compiler.ParseNextToken()) {
                throw new ParserException(group, "Unclosed group");
            }
        }

        return true;
    }
}