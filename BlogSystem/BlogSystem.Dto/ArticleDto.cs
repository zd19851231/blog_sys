using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; } 
        public string Email { get; set; } 
        public int GoodCount { get; set; }
        public int BadCount { get; set; }
        public string ImagePath { get; set; } 
        public string[] CategoryNames { get; set; }
        public string[] CategoryIds { get; set; }
    }
}
