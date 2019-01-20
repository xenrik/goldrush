using NUnit.Framework;
using System.Collections.Generic;

public class NullTokenTest {
    [Test]
    public void TestNull() {
        ValueToken result = new NullToken();
        Assert.IsNull(result.Value);
    }
}
