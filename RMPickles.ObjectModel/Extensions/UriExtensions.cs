﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="UriExtensions.cs" company="PicklesDoc">
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

namespace RMPickles.Core.Extensions
{
    public static class UriExtensions
    {
        public static Uri ToUri(this DirectoryInfo instance)
        {
            string fullName = instance.FullName;

            if (!instance.FullName.EndsWith(@"\"))
            {
                fullName = fullName + @"\";
            }

            return fullName.ToFolderUri();
        }

        public static Uri ToFileUriCombined(this DirectoryInfo instance, string file)
        {
            string path = Path.Combine(instance.FullName, file);

            return path.ToFileUri();
        }

        public static Uri ToUri(this FileSystemInfo instance)
        {
            var di = instance as DirectoryInfo;

            if (di != null)
            {
                return ToUri(di);
            }

            return ToUri((FileInfo)instance);
        }

        public static Uri ToUri(this FileInfo instance)
        {
            return ToFileUri(instance.FullName);
        }

        public static Uri ToFileUri(this string instance)
        {
            return new Uri(instance);
        }

        public static Uri ToFolderUri(this string instance)
        {
            if (!instance.EndsWith(@"\"))
            {
                return new Uri(instance + @"\");
            }

            return new Uri(instance);
        }

        public static string GetUriForTargetRelativeToMe(this Uri me, FileSystemInfo target, string newExtension)
        {
            return target.FullName != me.LocalPath
                ? me.MakeRelativeUri(target.ToUri()).ToString().Replace(target.Extension, newExtension)
                : "#";
        }
    }
}
