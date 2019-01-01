using NUnit.Framework;
using System.Collections.Generic;

public class IncrementAndReturnTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken rhs = new IntegerToken(2);
        IncrementAndReturnToken result = new IncrementAndReturnToken(0, rhs);

        Assert.AreEqual(rhs, result.Rhs);
    }
}
