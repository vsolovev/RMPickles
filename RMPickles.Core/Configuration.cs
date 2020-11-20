//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Configuration.cs" company="PicklesDoc">
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

using System.Collections.Generic;
using System.IO;
using System.Reflection;

using FeatureSwitcher.Configuration;

using NLog;

namespace RMPickles.Core
{
    public class Configuration : IConfiguration
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);
        private readonly List<FileInfo> testResultsFiles;

        public Configuration()
            : this(new LanguageServicesRegistry())
        {
        }

        public Configuration(ILanguageServicesRegistry languageServicesRegistry)
        {
            this.testResultsFiles = new List<FileInfo>();
            this.Language = languageServicesRegistry.DefaultLanguage;
        }

        public DirectoryInfo FeatureFolder { get; set; }

        public DirectoryInfo OutputFolder { get; set; }

        public DocumentationFormat DocumentationFormat { get; set; }

        public string Language { get; set; }

        public TestResultsFormat TestResultsFormat { get; set; }

        public bool HasTestResults
        {
            get { return this.TestResultsFiles != null && this.testResultsFiles.Count > 0; }
        }

        public FileInfo TestResultsFile
        {
            get { return this.testResultsFiles[0]; }
        }

        public IEnumerable<FileInfo> TestResultsFiles
        {
            get { return this.testResultsFiles; }
        }

        public string SystemUnderTestName { get; set; }

        public string SystemUnderTestVersion { get; set; }

        public void EnableExperimentalFeatures()
        {
            this.ShouldIncludeExperimentalFeatures = true;
            Features.Are.AlwaysEnabled();
        }

        public void DisableExperimentalFeatures()
        {
            this.ShouldIncludeExperimentalFeatures = false;
            Features.Are.AlwaysDisabled();
        }

        public bool ShouldEnableComments { get; private set; } = true;

        public void EnableComments()
        {
            this.ShouldEnableComments = true;
        }

        public void DisableComments()
        {
            this.ShouldEnableComments = false;
        }

        public bool ShouldIncludeExperimentalFeatures { get; private set; }

        public void AddTestResultFile(FileInfo FileInfo)
        {
            this.AddTestResultFileIfItExists(FileInfo);
        }

        public void AddTestResultFiles(IEnumerable<FileInfo> FileInfos)
        {
            foreach (var FileInfo in FileInfos ?? new FileInfo[0])
            {
                this.AddTestResultFileIfItExists(FileInfo);
            }
        }

        public string ExcludeTags { get; set; }

        public string HideTags { get; set; }
        public string ResourceDirectory { get; set; }

        private void AddTestResultFileIfItExists(FileInfo FileInfo)
        {
            if (FileInfo.Exists)
            {
                this.testResultsFiles.Add(FileInfo);
            }
            else
            {
                Log.Error("A test result file could not be found, it will be skipped: {0}", FileInfo.FullName);
            }
        }
    }
}
