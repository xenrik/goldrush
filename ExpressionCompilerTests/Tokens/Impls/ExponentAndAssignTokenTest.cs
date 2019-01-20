using NUnit.Framework;
using System.Collections.Generic;

public class ExponentAndAssignTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        ExponentAndAssignToken result = new ExponentAndAssignToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Lhs);
        Assert.AreEqual(rhs, result.Rhs);
    }
}
