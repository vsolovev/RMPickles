﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="XUnit1SingleResult.cs" company="PicklesDoc">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using RMPickles.Core.ObjectModel;

namespace RMPickles.Core.TestFrameworks.XUnit.XUnit1
{
    public class XUnit1SingleResult : SingleTestRunBase
    {
        private readonly XDocument resultsDocument;

        public XUnit1SingleResult(XDocument resultsDocument)
        {
            this.resultsDocument = resultsDocument;
        }

        public override TestResult GetFeatureResult(Feature feature)
        {
            XElement featureElement = this.GetFeatureElement(feature);

            if (featureElement == null)
            {
                return TestResult.Inconclusive;
            }

            int passedCount = int.Parse(featureElement.Attribute("passed").Value);
            int failedCount = int.Parse(featureElement.Attribute("failed").Value);
            int skippedCount = int.Parse(featureElement.Attribute("skipped").Value);

            return this.GetAggregateResult(passedCount, failedCount, skippedCount);
        }

        public override TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            IEnumerable<TestResult> exampleResults = this.GetScenarioOutlineElements(scenarioOutline).Select(this.GetResultFromElement);
            return this.DetermineAggregateResult(exampleResults);
        }

        public override TestResult GetScenarioResult(Scenario scenario)
        {
            XElement scenarioElement = this.GetScenarioElement(scenario);
            return scenarioElement != null
                ? this.GetResultFromElement(scenarioElement)
                : TestResult.Inconclusive;
        }

        public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] exampleValues)
        {
            IEnumerable<XElement> exampleElements = this.GetScenarioOutlineElements(scenarioOutline);

            foreach (XElement exampleElement in exampleElements)
            {
                if (this.ScenarioOutlineExampleMatcher.IsMatch(scenarioOutline, exampleValues, exampleElement))
                {
                    return this.GetResultFromElement(exampleElement);
                }
            }

            return TestResult.Inconclusive;
        }

        private bool ScenarioOutlineExampleIsMatch(Regex signature, XElement exampleElement)
        {
            return signature.IsMatch(exampleElement.Attribute("name").Value.ToLowerInvariant().Replace(@"\", string.Empty));
        }

        private XElement GetFeatureElement(Feature feature)
        {
            IEnumerable<XElement> featureQuery =
                from clazz in this.resultsDocument.Root.Descendants("class")
                from test in clazz.Descendants("test")
                from trait in clazz.Descendants("traits").Descendants("trait")
                where trait.Attribute("name").Value == "FeatureTitle" && trait.Attribute("value").Value == feature.Name
                select clazz;

            return featureQuery.FirstOrDefault();
        }

        private XElement GetScenarioElement(Scenario scenario)
        {
            XElement featureElement = this.GetFeatureElement(scenario.Feature);

            IEnumerable<XElement> scenarioQuery =
                from test in featureElement.Descendants("test")
                from trait in test.Descendants("traits").Descendants("trait")
                where trait.Attribute("name").Value == "Description" && trait.Attribute("value").Value == scenario.Name
                select test;

            return scenarioQuery.FirstOrDefault();
        }

        private IEnumerable<XElement> GetScenarioOutlineElements(ScenarioOutline scenario)
        {
            XElement featureElement = this.GetFeatureElement(scenario.Feature);

            IEnumerable<XElement> scenarioQuery =
                from test in featureElement.Descendants("test")
                from trait in test.Descendants("traits").Descendants("trait")
                where trait.Attribute("name").Value == "Description" && trait.Attribute("value").Value == scenario.Name
                select test;

            return scenarioQuery;
        }

        private TestResult GetResultFromElement(XElement element)
        {
            TestResult result;
            XAttribute resultAttribute = element.Attribute("result");
            switch (resultAttribute.Value.ToLowerInvariant())
            {
                case "pass":
                    result = TestResult.Passed;
                    break;
                case "fail":
                    result = TestResult.Failed;
                    break;
                case "skip":
                default:
                    result = TestResult.Inconclusive;
                    break;
            }

            return result;
        }
    }
}
