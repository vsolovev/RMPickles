﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="XUnit2ScenarioOutlineExampleMatcher.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

using RMPickles.Core.ObjectModel;

namespace RMPickles.Core.TestFrameworks.XUnit.XUnit2
{
    public class XUnit2ScenarioOutlineExampleMatcher : IScenarioOutlineExampleMatcher
    {
        private readonly XUnitExampleSignatureBuilder signatureBuilder = new XUnitExampleSignatureBuilder();

        public bool IsMatch(ScenarioOutline scenarioOutline, string[] exampleValues, object scenarioElement)
        {
            var build = this.signatureBuilder.Build(scenarioOutline, exampleValues);
            return ScenarioOutlineExampleIsMatch((assembliesAssemblyCollectionTest)scenarioElement, build);
        }

        private bool ScenarioOutlineExampleIsMatch(assembliesAssemblyCollectionTest exampleElement, Regex signature)
        {
            // split scenario outline title to name + parameters
            var nameAndArgumentsSplitter = new Regex(@"^(?<name>(.*))(\(.*\))$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            var groups = nameAndArgumentsSplitter.Match(exampleElement.name).Groups;
            var scenarioName = groups["name"].Success ? groups["name"].Value : exampleElement.name;
            var scenariotNameWithNoSpacesAndSpecialCharacters = exampleElement.name.Replace(scenarioName, exampleElement.method);

            var esc = Regex.Escape("\"");
            var escapedScenariotNameWithNoSpacesAndSpecialCharacters = scenariotNameWithNoSpacesAndSpecialCharacters.Replace(@"\\""", "\"").Replace(@"\""", esc);
            var escapedExampleElementName = exampleElement.name.Replace(@"\\""", "\"").Replace(@"\""", esc);
            var escapedSignature = signature.ToString().Replace(@"\""", esc);

            return Regex.IsMatch(escapedExampleElementName, escapedSignature, RegexOptions.IgnoreCase) || Regex.IsMatch(escapedScenariotNameWithNoSpacesAndSpecialCharacters, escapedSignature, RegexOptions.IgnoreCase);
        }
    }
}