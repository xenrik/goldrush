using NUnit.Framework;
using System.Collections.Generic;

public class IsEmptyTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        IsEmptyToken result = new IsEmptyToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
