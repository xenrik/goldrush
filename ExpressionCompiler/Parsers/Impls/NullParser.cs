/**
* Parser that accepts null sequences. 
*/
public class NullParser : WordTokenParser {
    public NullParser() : base(true, "null") {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        if (base.Parse(compiler)) {
            NullToken token = new NullToken(i);
            compiler.Parent.AddChild(token);
            return true;
        } else {
            return false;
        }
    }
}
