using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    public class CreateCommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}