using NUnit.Framework;
using System.Collections.Generic;

public class IdentifierTokenTest {
    [Test]
    public void TestTokenProperties() {
        IdentifierToken result = new IdentifierToken("identifier");
        Assert.AreEqual("identifier", result.Value);
    }
}
