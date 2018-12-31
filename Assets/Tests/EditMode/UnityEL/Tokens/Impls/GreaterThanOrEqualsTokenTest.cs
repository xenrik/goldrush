using NUnit.Framework;
using System.Collections.Generic;

public class GreaterThanOrEqualsTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        GreaterThanOrEqualsToken result = new GreaterThanOrEqualsToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Lhs);
        Assert.AreEqual(rhs, result.Rhs);
    }
}
