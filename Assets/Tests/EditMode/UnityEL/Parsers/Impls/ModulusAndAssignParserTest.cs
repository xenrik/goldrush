﻿using UnityEngine;
using UnityEditor;

public class ModulusAndAssignParserTest : BinaryParserTest<ModulusAndAssignParser, ModulusAndAssignToken> {
    public override string ParserSymbol { get { return "%="; } }
}