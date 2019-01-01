using NUnit.Framework;
using System.Collections.Generic;

public class DecrementAndReturnTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        DecrementAndReturnToken result = new DecrementAndReturnToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
