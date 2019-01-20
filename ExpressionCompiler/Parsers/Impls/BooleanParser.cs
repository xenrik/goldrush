/**
* Parser that accepts boolean sequences. 
*/
public class BooleanParser : TokenParser {
    private WordTokenParser trueParser = new WordTokenParser(true, "true");
    private WordTokenParser falseParser = new WordTokenParser(true, "false");

    public bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        if (trueParser.Parse(compiler)) {
            BooleanToken token = new BooleanToken(true, i);
            compiler.Parent.AddChild(token);
            return true;
        } else if (falseParser.Parse(compiler)) {
            BooleanToken token = new BooleanToken(false, i);
            compiler.Parent.AddChild(token);
            return true;
        } else {
            return false;
        }
    }
}
