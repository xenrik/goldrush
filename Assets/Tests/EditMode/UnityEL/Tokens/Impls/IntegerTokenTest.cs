using NUnit.Framework;
using System.Collections.Generic;

public class IntegerTokenTest {
    [Test]
    public void TestSimpleInteger() {
        ValueToken result = new IntegerToken(123);
        Assert.AreEqual(123, result.Value);
    }
}