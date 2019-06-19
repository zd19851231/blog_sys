using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    //如果该ViewModel和Dto中的类一样，可以省略直接使用Dto中的类，如果不同，必须创建
    public class ArticleDetailsViewModel
    { 
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public string Email { get; set; }
        public string ImagePath { get; set; }

        public string[] CategoryIds { get; set; }

        public string[] CategoryNames { get; set; }

        public int GoodCount { get; set; }

        public int BedCount { get; set; }
    }
}