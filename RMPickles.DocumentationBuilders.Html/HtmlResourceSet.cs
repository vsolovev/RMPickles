//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HtmlResourceSet.cs" company="PicklesDoc">
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

using RMPickles.Core.Extensions;

namespace RMPickles.Core.DocumentationBuilders.Html
{
    public class HtmlResourceSet
    {
        private readonly IConfiguration configuration;

        public HtmlResourceSet(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Uri MasterStylesheet
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("css/master.css"); }
        }

        public Uri PrintStylesheet
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("css/print.css"); }
        }

        public Uri JQueryScript
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("js/jquery.js"); }
        }

        public Uri AdditionalScripts
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("js/scripts.js"); }
        }

        public Uri SuccessImage
        {
            get { return new Uri(Path.Combine(this.ImagesFolder, "success.png")); }
        }

        public string ImagesFolder
        {
            get { return Path.Combine(this.configuration.OutputFolder.FullName, "img"); }
        }

        public string ScriptsFolder
        {
            get { return Path.Combine(this.configuration.OutputFolder.FullName, "js"); }
        }

        public Uri FailureImage
        {
            get { return new Uri(Path.Combine(this.ImagesFolder, "failure.png")); }
        }

        public IEnumerable<HtmlResource> All
        {
            get { return this.Stylesheets.Concat(this.Images); }
        }

        public IEnumerable<HtmlResource> Stylesheets
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (string resource in resources.Where(resource => resource.EndsWith(".css") && resource.Contains(".Html.")))
                {
                    string fileName = this.GetNameFromResourceName(resource);
                    yield return new HtmlResource
                    {
                        File = fileName,
                        Uri = this.configuration.OutputFolder.ToFileUriCombined(fileName)
                    };
                }
            }
        }

        public IEnumerable<HtmlResource> Images
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (string resource in resources.Where(resource => resource.EndsWith(".png") && resource.Contains(".Html.")))
                {
                    string fileName = this.GetNameFromResourceName(resource);
                    yield return new HtmlResource
                    {
                        File = fileName,
                        Uri = new Uri(Path.Combine(this.ImagesFolder, fileName))
                    };
                }
            }
        }

        public IEnumerable<HtmlResource> Scripts
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (string resource in resources.Where(resource => resource.EndsWith(".js") && resource.Contains(".Html.")))
                {
                    string fileName = this.GetNameFromResourceName(resource);
                    yield return new HtmlResource
                    {
                        File = fileName,
                        Uri = new Uri(Path.Combine(this.ScriptsFolder, fileName))
                    };
                }
            }
        }

        public Uri InconclusiveImage
        {
            get { return new Uri(Path.Combine(this.ImagesFolder, "inconclusive.png")); }
        }

        private string GetNameFromResourceName(string resourceName)
        {
            if (resourceName.StartsWith("RMPickles.Core.Resources.Html.img"))
            {
                return resourceName.Replace("RMPickles.Core.Resources.Html.img.", string.Empty);
            }
            else if (resourceName.StartsWith("RMPickles.Core.Resources.Html.js"))
            {
                return resourceName.Replace("RMPickles.Core.Resources.Html.js.", string.Empty);
            }
            else
            {
                return resourceName.Replace("RMPickles.Core.Resources.Html.css.", string.Empty);
            }
        }
    }
}
