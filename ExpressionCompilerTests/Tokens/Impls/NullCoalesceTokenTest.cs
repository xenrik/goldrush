using NUnit.Framework;
using System.Collections.Generic;

public class NullCoalesceTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        NullCoalesceToken result = new NullCoalesceToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Lhs);
        Assert.AreEqual(rhs, result.Rhs);
    }
}
