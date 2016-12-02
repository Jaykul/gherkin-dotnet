﻿using System;
using Newtonsoft.Json;

namespace Gherkin.AstGenerator
{
    public static class AstGenerator
    {
        public static string GenerateAst(string featureFilePath)
        {
            var parser = new Parser();
            var parsingResult = parser.Parse(featureFilePath);

            if (parsingResult == null)
                throw new InvalidOperationException("parser returned null");

            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Formatting = Formatting.Indented;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializerSettings.ContractResolver = new FeatureAstJSonContractResolver();
            var astText = JsonConvert.SerializeObject(parsingResult, jsonSerializerSettings);

            return LineEndingHelper.NormalizeJSonLineEndings(astText);
        }

    }
}
