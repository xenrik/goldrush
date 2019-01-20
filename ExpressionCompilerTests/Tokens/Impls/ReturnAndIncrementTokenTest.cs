using NUnit.Framework;

public class ReturnAndIncrementTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(2);
        ReturnAndIncrementToken result = new ReturnAndIncrementToken(0, lhs);

        Assert.AreEqual(lhs, result.Lhs);
    }
}
