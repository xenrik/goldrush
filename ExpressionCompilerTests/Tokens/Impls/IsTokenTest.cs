using NUnit.Framework;
using System.Collections.Generic;

public class IsTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        IsToken result = new IsToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Lhs);
        Assert.AreEqual(rhs, result.Rhs);
    }
}
