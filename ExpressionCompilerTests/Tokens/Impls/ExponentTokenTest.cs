using NUnit.Framework;
using System.Collections.Generic;

public class ExponentTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        ExponentToken result = new ExponentToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Lhs);
        Assert.AreEqual(rhs, result.Rhs);
    }
}
