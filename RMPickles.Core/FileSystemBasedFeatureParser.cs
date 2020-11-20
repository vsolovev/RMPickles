//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FileSystemBasedFeatureParser.cs" company="PicklesDoc">
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
using System.Text;
using RMPickles.Core.ObjectModel;

namespace RMPickles.Core
{
    public class FileSystemBasedFeatureParser
    {
        private readonly FeatureParser parser;

        private readonly EncodingDetector encodingDetector;

        public FileSystemBasedFeatureParser(FeatureParser parser)
        {
            this.parser = parser;
            this.encodingDetector = new EncodingDetector();
        }

        public Feature Parse(string filename)
        {
            Feature feature = null;
            var encoding = this.encodingDetector.GetEncoding(filename);
            using (var fileStream = File.OpenRead(filename))
            {
                using (var specificEncoderReader = new StreamReader(fileStream, encoding))
                {
                    try
                    {
                        feature = this.parser.Parse(specificEncoderReader);
                    }
                    catch (FeatureParseException e)
                    {
                        string message =
                            $"There was an error parsing the feature file here: {Path.GetFullPath(filename)}" +
                            Environment.NewLine +
                            $"Errormessage was: '{e.Message}'";
                        throw new FeatureParseException(message, e);
                    }
                    specificEncoderReader.Close();

                }
                fileStream.Close();
            }

            return feature;
        }
    }
}