using NUnit.Framework;
using System.Collections.Generic;

public class ReturnAndIncrementTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(2);
        ReturnAndIncrementToken result = new ReturnAndIncrementToken(0, lhs);

        Assert.AreEqual(lhs, result.Lhs);
    }
}
