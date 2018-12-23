using NUnit.Framework;
using System.Collections.Generic;

public class KeyedAccessTokenTest {
    [Test]
    public void TestTokenProperties() {
        IdentifierToken host = new IdentifierToken("KeyedAccess");
        KeyedAccessToken result = new KeyedAccessToken(0, host);

        Assert.AreEqual(host, result.Host);
    }
}
