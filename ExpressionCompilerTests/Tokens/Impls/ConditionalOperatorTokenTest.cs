using NUnit.Framework;
using System.Collections.Generic;

public class ConditionalOperatorTokenTest {
    [Test]
    public void TestTokenProperties() {
        BooleanToken test = new BooleanToken(true);
        IntegerToken resultIfTrue = new IntegerToken(1);
        IntegerToken resultIfFalse = new IntegerToken(2);
        ConditionalOperatorToken result = new ConditionalOperatorToken(0, test, resultIfTrue, resultIfFalse);

        Assert.AreEqual(test, result.Test);
        Assert.AreEqual(resultIfTrue, result.ResultIfTrue);
        Assert.AreEqual(resultIfFalse, result.ResultIfFalse);
    }
}
