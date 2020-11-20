using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using G = Gherkin.Ast;
using System.Threading.Tasks;
using RMPickles.Core.Extensions;

namespace RMPickles.Core.ObjectModel
{
    public class ResourceFileMapper
    {
        private IConfiguration configuration;

        private readonly string[] extensions = new string[]
        {
            ".json",
            ".xml",
            ".txt",
            ".csv"
        };

        public ResourceFileMapper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Comment> MapFiles(string content, Location location)
        {

            var list = new List<Comment>();
            var substring = content.Split('\'');
            
            foreach(var ext in extensions)
            {
                if (substring.Any(x => x.Contains(ext)))
                {
                    var extSubstrings = substring.Where(x => x.Contains(ext));
                    foreach (var extFile in extSubstrings)
                    {
                        //Map file to real file address
                        //Resources dir should be placed actually
                        var guid = Guid.NewGuid().ToString();
                        var path = Path.Combine(configuration.ResourceDirectory, extFile.Replace(ext, guid).Replace('.', '/')).Replace(guid, ext);
                        if (File.Exists(path))
                        {
                            var lines = File.ReadAllLines(path);
                            var comment = new Comment();
                            comment.Summary = $"{extFile}";
                            comment.Location = location;
                            comment.Type = CommentType.AfterLastStepComment;
                            foreach (var line in lines)
                            {
                                comment.Rows.Add(line);
                            }
                            list.Add(comment);
                        }
                       
                    }
                }
            }
            
            return list;
        }

        public List<Comment> MapToComments(IEnumerable<G.TableRow> rows)
        {
            var list = new List<Comment>();
            foreach(var row in rows)
            {               
                foreach(var col in row.Cells)
                {                    
                    foreach (var ext in extensions)
                    {
                        if (col.Value.Contains(ext))
                        {                            
                            var guid = Guid.NewGuid().ToString();
                            var path = Path.Combine(configuration.ResourceDirectory, col.Value.Replace(ext, guid).Replace('.', '/')).Replace(guid, ext);
                            if (File.Exists(path))
                            {
                                var lines = File.ReadAllLines(path);
                                var comment = new Comment();
                                comment.Summary = $"{col.Value}";
                                comment.Type = CommentType.Inline;
                                foreach (var line in lines)
                                {
                                    comment.Rows.Add(line);
                                }
                                list.Add(comment);
                            }
                            
                        }
                    }                   
                }
            }
            return list;
        }

        

        

       
    }
}
