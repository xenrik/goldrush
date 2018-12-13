using NUnit.Framework;
using System.Collections.Generic;

public class FunctionTokenTest {
    [Test]
    public void TestTokenProperties() {
        IdentifierToken functionName = new IdentifierToken("function");
        FunctionToken result = new FunctionToken(0, functionName);

        Assert.AreEqual(functionName, result.FunctionName);
    }
}
