using NUnit.Framework;
using System.Collections.Generic;

public class UnaryMinusTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        UnaryMinusToken result = new UnaryMinusToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
