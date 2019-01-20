using NUnit.Framework;
using System.Collections.Generic;

public class LessThanTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        LessThanToken result = new LessThanToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Lhs);
        Assert.AreEqual(rhs, result.Rhs);
    }
}
