using NUnit.Framework;
using System.Collections.Generic;

public class StringTokenTest {
    [Test]
    public void TestSimpleString() {
        ValueToken result = new StringToken("string");
        Assert.AreEqual("string", result.Value);
    }
}
