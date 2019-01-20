using NUnit.Framework;
using System.Collections.Generic;

public class ExistsTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        ExistsToken result = new ExistsToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
