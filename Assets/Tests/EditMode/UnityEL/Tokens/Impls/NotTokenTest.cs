using NUnit.Framework;
using System.Collections.Generic;

public class NotTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        NotToken result = new NotToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
