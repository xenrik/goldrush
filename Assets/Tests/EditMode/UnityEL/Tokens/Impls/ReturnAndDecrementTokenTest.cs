using NUnit.Framework;
using System.Collections.Generic;

public class ReturnAndDecrementTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(2);
        ReturnAndDecrementToken result = new ReturnAndDecrementToken(0, lhs);

        Assert.AreEqual(lhs, result.Lhs);
    }
}
