using NUnit.Framework;
using System;
using System.Collections.Generic;

public class AdditionParserTest : BinaryParserTest<AdditionParser,AdditionToken> {
    public override string ParserSymbol { get { return "+"; } }
}
