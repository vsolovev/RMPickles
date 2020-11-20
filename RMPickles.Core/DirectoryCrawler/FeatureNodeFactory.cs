//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FeatureNodeFactory.cs" company="PicklesDoc">
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
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using NLog;
using RMPickles.Core.DocumentationBuilders.Html;
using RMPickles.Core.Extensions;
using RMPickles.Core.ObjectModel;

namespace RMPickles.Core.DirectoryCrawler
{
    public class FeatureNodeFactory
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private readonly FileSystemBasedFeatureParser featureParser;
        private readonly RelevantFileDetector relevantFileDetector;

        public FeatureNodeFactory(RelevantFileDetector relevantFileDetector, FileSystemBasedFeatureParser featureParser)
        {
            this.relevantFileDetector = relevantFileDetector;
            this.featureParser = featureParser;
        }

        public INode Create(FileSystemInfo root, FileSystemInfo location, ParsingReport report)
        {
            string relativePathFromRoot = root == null ? @".\" : PathExtensions.MakeRelativePath(root, location);

            var directory = location as DirectoryInfo;
            if (directory != null)
            {
                return new FolderNode(directory, relativePathFromRoot);
            }

            var file = location as FileInfo;

            if (file != null)
            {
                if (this.relevantFileDetector.IsFeatureFile(file))
                {
                    try
                    {
                        Feature feature = this.featureParser.Parse(file.FullName);
                        return feature != null ? new FeatureNode(file, relativePathFromRoot, feature) : null;
                    }
                    catch (FeatureParseException exception)
                    {
                        report.Add(exception.Message);
                        Log.Error(exception.Message);
                        return null;
                    }
                }                
                else if (this.relevantFileDetector.IsImageFile(file))
                {
                    return new ImageNode(file, relativePathFromRoot);
                }
            }

            var message = "Cannot create an IItemNode-derived object for " + location.FullName;
            report.Add(message);
            Log.Error(message);
            return null;
        }
    }
}
