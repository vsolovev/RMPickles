﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NUnit2Results.cs" company="PicklesDoc">
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


namespace RMPickles.Core.TestFrameworks.NUnit.NUnit2
{
    public class NUnit2Results : MultipleTestRunsBase
    {
        public NUnit2Results(IConfiguration configuration, NUnit2SingleResultLoader singleResultLoader, NUnit2ScenarioOutlineExampleMatcher scenarioOutlineExampleMatcher)
            : base(configuration, singleResultLoader, scenarioOutlineExampleMatcher)
        {
        }
    }
}
