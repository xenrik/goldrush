using NUnit.Framework;
using System.Collections.Generic;

public class DecimalTokenTest {
    private DecimalToken rawToken;

    [Test]
    public void TestSimpleDecimal() {
        ValueToken result = new DecimalToken(123.456f);
        Assert.AreEqual(123.456f, result.Value);
    }
}
