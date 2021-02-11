//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HtmlTableFormatter.cs" company="PicklesDoc">
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
using System.Linq;
using System.Xml.Linq;

using RMPickles.Core.ObjectModel;

namespace RMPickles.Core.DocumentationBuilders.Html
{
    public class HtmlTableFormatter
    {
        private readonly XNamespace xmlns;
        private readonly HtmlImageResultFormatter htmlImageResultFormatter;

        public HtmlTableFormatter(HtmlImageResultFormatter htmlImageResultFormatter)
        {
            this.htmlImageResultFormatter = htmlImageResultFormatter;
            this.xmlns = HtmlNamespace.Xhtml;
        }

        public XElement Format(Table table)
        {
            return this.Format(table, null);
        }

        protected Dictionary<string, XElement> FormatComments(Table table, CommentType type)
        {
            var dict = new Dictionary<string, XElement>();
            foreach (var stepComment in table.Comments.Where(o => o.Type == type))
            {
                var guid = Guid.NewGuid().ToString();
                var baseElement = new XElement(this.xmlns + "details");
                var preElement = new XElement(this.xmlns + "pre");
                var summaryElement = new XElement(this.xmlns + "summary", stepComment.Summary);
                summaryElement.Add(new XElement(this.xmlns + "button", new XAttribute("style", "margin-left:10px"), new XAttribute("onclick", $"copyFileContent(\"{guid}\")")),"Copy");
                summaryElement.Add(new XElement(this.xmlns + "button", new XAttribute("style", "margin-left:10px"), new XAttribute("onclick", $"download(\"{stepComment.Summary}\",\"{guid}\")")), "Download");
                XElement comment = new XElement(this.xmlns + "span", new XAttribute("class", "comment"), new XAttribute("id",guid));

                comment.Add(stepComment.Text);
                comment.Add(new XElement(this.xmlns + "br"));
                foreach (var row in stepComment.Rows.Skip(1))
                {
                    comment.Add(row);
                    comment.Add(new XElement(this.xmlns + "br"));
                }

                comment.LastNode.Remove();

                baseElement.Add(summaryElement);
                preElement.Add(comment);
                baseElement.Add(preElement);
                
                if (!dict.ContainsKey(stepComment.Summary))
                {
                    dict.Add(stepComment.Summary, baseElement);
                }                
            }
            

            return dict;
        }

        public XElement Format(Table table, ScenarioOutline scenarioOutline)
        {
            if (table == null)
            {
                return null;
            }

            Dictionary<string, XElement> inlineComments = null;           
            if (table.Comments.Any(o => o.Type == CommentType.Inline))
            {
                inlineComments = this.FormatComments(table, CommentType.Inline);
            }
            if (inlineComments == null)
            {
                inlineComments = new Dictionary<string, XElement>();
            }
            var headerCells = table.HeaderRow.Cells.ToArray();

            headerCells = headerCells.Concat(new[] { " " }).ToArray();
            var filledList = new List<string>();
            var cells = table.DataRows.Select(row => this.FormatRow(row, scenarioOutline, inlineComments, filledList)).ToList();
            foreach(var item in filledList.Distinct())
            {
                inlineComments.Remove(item);
            }
            var rootElement = new XElement(this.xmlns + "div");
            foreach(var item in inlineComments)
            {
                rootElement.Add(item.Value);
            }
            return new XElement(
                this.xmlns + "div",
                new XAttribute("class", "table_container"),
                new XElement(
                    this.xmlns + "table",
                    new XAttribute("class", "datatable"),
                    new XElement(
                        this.xmlns + "thead",
                        new XElement(
                            this.xmlns + "tr",
                            headerCells.Select(
                                cell => new XElement(this.xmlns + "th", cell)))),
                    new XElement(
                        this.xmlns + "tbody",
                        cells)), rootElement);
        }

        private XElement FormatRow(TableRow row, ScenarioOutline scenarioOutline, Dictionary<string, XElement> inlineComments, List<string> filledList)
        {
            var formattedCells = row.Cells.Select(
                cell =>
                    {
                        var element = new XElement(this.xmlns + "td");
                        if (inlineComments.ContainsKey(cell))
                        {
                            element.Add(inlineComments[cell]);
                            filledList.Add(cell);
                        }
                        else
                        {
                            element.Add(cell);
                        }
                        return element;
                    }
                    ).ToList();

            if (scenarioOutline != null)
            {
                formattedCells.Add(
                    new XElement(this.xmlns + "td", this.htmlImageResultFormatter.Format(scenarioOutline, row.Cells.ToArray())));
            }

            var result = new XElement(this.xmlns + "tr");

            foreach (var cell in formattedCells)
            {
                result.Add(cell);
            }

            return result;
        }
    }
}
