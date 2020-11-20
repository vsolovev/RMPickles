﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DirectoryTreeCrawler.cs" company="PicklesDoc">
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
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using RMPickles.Core.DataStructures;

namespace RMPickles.Core.DirectoryCrawler
{
    public class DirectoryTreeCrawler
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);
        private readonly FeatureNodeFactory featureNodeFactory;

        private readonly RelevantFileDetector relevantFileDetector;

        public DirectoryTreeCrawler(RelevantFileDetector relevantFileDetector, FeatureNodeFactory featureNodeFactory)
        {
            this.relevantFileDetector = relevantFileDetector;
            this.featureNodeFactory = featureNodeFactory;
        }

        public Tree Crawl(DirectoryInfo directory, ParsingReport parsingReport)
        {
            return this.Crawl(directory, null, parsingReport);
        }

        private Tree Crawl(DirectoryInfo directory, INode rootNode, ParsingReport parsingReport)
        {
            INode currentNode =
                this.featureNodeFactory.Create(rootNode != null ? rootNode.OriginalLocation : null, directory, parsingReport);

            if (rootNode == null)
            {
                rootNode = currentNode;
            }

            var tree = new Tree(currentNode);

            var filesAreFound = this.CollectFiles(directory, rootNode, tree, parsingReport);

            var directoriesAreFound = this.CollectDirectories(directory, rootNode, tree, parsingReport);

            if (!filesAreFound && !directoriesAreFound)
            {
                return null;
            }

            return tree;
        }

        private bool CollectDirectories(DirectoryInfo directory, INode rootNode, Tree tree, ParsingReport parsingReport)
        {
            List<Tree> collectedNodes = new List<Tree>();

            foreach (DirectoryInfo subDirectory in directory.GetDirectories().OrderBy(di => di.Name))
            {
                Tree subTree = this.Crawl(subDirectory, rootNode, parsingReport);
                if (subTree != null)
                {
                    collectedNodes.Add(subTree);
                }
            }

            foreach (var node in collectedNodes)
            {
                tree.Add(node);
            }

            return collectedNodes.Count > 0;
        }

        private bool CollectFiles(DirectoryInfo directory, INode rootNode, Tree tree, ParsingReport parsingReport)
        {
            List<INode> collectedNodes = new List<INode>();

            foreach (FileInfo file in directory.GetFiles().Where(file => this.relevantFileDetector.IsRelevant(file)))
            {
                INode node = this.featureNodeFactory.Create(rootNode.OriginalLocation, file, parsingReport);
                if(node != null)
                    collectedNodes.Add(node);
            }

            foreach (var node in OrderFileNodes(collectedNodes))
            {
                tree.Add(node);
            }

            return collectedNodes.Count > 0;
        }

        private static IEnumerable<INode> OrderFileNodes(List<INode> collectedNodes)
        {
            var indexFiles =
                collectedNodes.Where(
                    node => node.OriginalLocation.Name.StartsWith("index", StringComparison.InvariantCultureIgnoreCase));
            var otherFiles = collectedNodes.Except(indexFiles);

            return indexFiles.OrderBy(node => node.Name).Concat(otherFiles.OrderBy(node => node.Name));
        }
    }
}
