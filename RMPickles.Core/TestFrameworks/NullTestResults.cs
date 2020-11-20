﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NullTestResults.cs" company="PicklesDoc">
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

using RMPickles.Core.ObjectModel;

namespace RMPickles.Core.TestFrameworks
{
    public class NullTestResults : ITestResults
    {
        #region ITestResults Members

        public TestResult GetFeatureResult(Feature feature)
        {
            return new TestResult();
        }

        public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            return new TestResult();
        }

        public TestResult GetScenarioResult(Scenario scenario)
        {
            return new TestResult();
        }

        #endregion

        public TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] exampleValues)
        {
            return new TestResult();
        }

        public bool SupportsExampleResults
        {
            get { return false; }
        }
    }
}
