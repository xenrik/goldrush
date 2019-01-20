using NUnit.Framework;
using System.Collections.Generic;

public class BooleanTokenTest {
    [Test]
    public void TestTrue() {
        ValueToken result = new BooleanToken(true);
        Assert.AreEqual(true, result.Value);
    }

    [Test]
    public void TestFalse() {
        ValueToken result = new BooleanToken(false);
        Assert.AreEqual(false, result.Value);
    }
}
