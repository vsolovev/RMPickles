﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PathExtensions.cs" company="PicklesDoc">
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

namespace RMPickles.Core.Extensions
{
    public static class PathExtensions
    {
        public static string MakeRelativePath(string from, string to)
        {
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException("from");
            }

            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("to");
            }

            string fromString = AddTrailingSlashToDirectoriesForUriMethods(from);
            string toString = AddTrailingSlashToDirectoriesForUriMethods(to);

            var fromUri = new Uri(fromString);
            var toUri = new Uri(toString);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        private static string AddTrailingSlashToDirectoriesForUriMethods(string path)
        {
            // Uri class treats paths that end in \ as directories, and without \ as files.
            // So if its a file then we need to append the \ to make the Uri class recognize it as a directory
            path = RemoveEndSlashSoWeDoNotHaveTwoIfThisIsADirectory(path);

            return Directory.Exists(path) ? path + @"\" : path;
        }

        private static string RemoveEndSlashSoWeDoNotHaveTwoIfThisIsADirectory(string path)
        {
            return path.TrimEnd('\\');
        }

        public static string MakeRelativePath(FileSystemInfo from, FileSystemInfo to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            return MakeRelativePath(from.FullName, to.FullName);
        }

        private static string[] GetAllFilesFromPathAndFileNameWithOptionalWildCards(string fileFullName)
        {
            var path = Path.GetDirectoryName(fileFullName);
            var wildcardFileName = Path.GetFileName(fileFullName);
            // GetFiles returns an array with 1 empty string when wildcard match is not found.
            return Directory.GetFiles(path, wildcardFileName).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        public static IEnumerable<FileInfo> GetAllFilesFromPathAndFileNameWithOptionalSemicolonsAndWildCards(string fileFullName)
        {
            var files = fileFullName.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            return files.SelectMany(f => GetAllFilesFromPathAndFileNameWithOptionalWildCards(f))
                    .Distinct()
                    .Select(f => new FileInfo(f));
        }
        
    }
}
