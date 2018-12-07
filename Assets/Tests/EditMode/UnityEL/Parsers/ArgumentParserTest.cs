using NUnit.Framework;
using System.Collections.Generic;

public class ArgumentParserTest {
    private ArgumentParser parser;
    
    [SetUp]
    public void Init() {
        parser = new ArgumentParser();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(arg1,arg2)";
        int pos = 15;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ArgumentToken>(result);
        Assert.AreEqual(16, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier(arg1+arg2)";
        int pos = 15;

        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(15, pos);
    }
}
