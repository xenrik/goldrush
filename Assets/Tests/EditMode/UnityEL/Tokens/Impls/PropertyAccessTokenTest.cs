﻿using NUnit.Framework;
using System.Collections.Generic;

public class PropertyAccessTokenTest {
    [Test]
    public void TestTokenProperties() {
        IntegerToken lhs = new IntegerToken(1);
        IntegerToken rhs = new IntegerToken(2);
        PropertyAccessToken result = new PropertyAccessToken(0, lhs, rhs);

        Assert.AreEqual(lhs, result.Host);
        Assert.AreEqual(rhs, result.Property);
    }
}
