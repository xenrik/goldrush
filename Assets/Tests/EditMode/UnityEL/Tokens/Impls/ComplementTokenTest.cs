using NUnit.Framework;
using System.Collections.Generic;

public class ComplementTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        ComplementToken result = new ComplementToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
