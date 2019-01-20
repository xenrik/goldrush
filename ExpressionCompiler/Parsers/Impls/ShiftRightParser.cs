public class ShiftRightParser : BinaryTokenParser {
    public override string Name { get { return "shiftRight"; } }

    public ShiftRightParser() : 
        base(new DoubleCharacterParser('>')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ShiftRightToken(symbolPos, lhs, rhs));
        } else {
            return new ShiftRightToken(symbolPos, lhs, rhs);
        }
    }
}