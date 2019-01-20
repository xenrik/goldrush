using NUnit.Framework;
using System.Collections.Generic;

public class NotExistsTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        NotExistsToken result = new NotExistsToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
