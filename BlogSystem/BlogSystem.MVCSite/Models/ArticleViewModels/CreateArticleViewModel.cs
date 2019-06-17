using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    public class CreateArticleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Display(Name = "用户文章分类")]
        public Guid[] CategoryIds { get; set; }
    }
}