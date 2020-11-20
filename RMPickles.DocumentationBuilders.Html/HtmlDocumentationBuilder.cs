﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HtmlDocumentationBuilder.cs" company="PicklesDoc">
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
using System.Text;
using System.Xml.Linq;

using NLog;

using RMPickles.Core.DataStructures;
using RMPickles.Core.DirectoryCrawler;
using RMPickles.Core.Extensions;

namespace RMPickles.Core.DocumentationBuilders.Html
{
    public class HtmlDocumentationBuilder : IDocumentationBuilder
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private readonly IConfiguration configuration;
        private readonly HtmlDocumentFormatter htmlDocumentFormatter;
        private readonly HtmlResourceWriter htmlResourceWriter;

        public HtmlDocumentationBuilder(
            IConfiguration configuration,
            HtmlDocumentFormatter htmlDocumentFormatter,
            HtmlResourceWriter htmlResourceWriter)
        {
            this.configuration = configuration;
            this.htmlDocumentFormatter = htmlDocumentFormatter;
            this.htmlResourceWriter = htmlResourceWriter;
        }

        #region IDocumentationBuilder Members

        public void Build(Tree features)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info("Writing HTML to {0}", this.configuration.OutputFolder.FullName);
            }

            this.htmlResourceWriter.WriteTo(this.configuration.OutputFolder.FullName);

            if (features != null)
            {
                foreach (var node in features)
                {
                    this.VisitNodes(features, node);
                }
            }
        }

        private void VisitNodes(Tree features, INode node)
        {
            if (node.IsIndexMarkDownNode())
            {
                return;
            }

            string nodePath = Path.Combine(this.configuration.OutputFolder.FullName, node.RelativePathFromRoot);
            string htmlFilePath;

            if (node.NodeType == NodeType.Content)
            {
                htmlFilePath = nodePath.Replace(Path.GetExtension(nodePath), ".html");
                this.WriteContentNode(features, node, htmlFilePath);
            }
            else if (node.NodeType == NodeType.Structure)
            {
                Directory.CreateDirectory(nodePath);

                htmlFilePath = Path.Combine(nodePath, "index.html");
                this.WriteContentNode(features, node, htmlFilePath);
            }
            else
            {
                // copy file from source to output
                File.Copy(node.OriginalLocation.FullName, nodePath, overwrite: true);
            }
        }

        private void WriteContentNode(Tree features, INode node, string htmlFilePath)
        {
            using (var writer = new System.IO.StreamWriter(htmlFilePath, false, Encoding.UTF8))
            {
                XDocument document = this.htmlDocumentFormatter.Format(node, features, this.configuration.FeatureFolder);
                document.Save(writer);
                writer.Close();
            }
        }

        #endregion
    }
}
