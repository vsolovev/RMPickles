using RMPickles.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMPickles.Console
{
    public class MarkdownProvider : IMarkdownProvider
    {
        public string Transform(string text)
        {
            return text;
        }
    }
}
