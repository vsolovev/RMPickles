﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ImageFileDetector.cs" company="PicklesDoc">
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

namespace RMPickles.Core.DirectoryCrawler
{
    public class ImageFileDetector
    {
        private readonly List<string> validExtensions;

        public ImageFileDetector()
        {
            this.validExtensions = new List<string> { ".PNG", ".JPG", ".BMP", ".GIF" };
        }

        public bool IsRelevant(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                return false;
            }

            string extension = fileInfo.Extension.ToUpperInvariant();

            if (this.validExtensions.Contains(extension))
            {
                return true;
            }

            return false;
        }
    }
}
