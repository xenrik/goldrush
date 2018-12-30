using NUnit.Framework;
using System.Collections.Generic;

public class IsNotEmptyTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        IsNotEmptyToken result = new IsNotEmptyToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
