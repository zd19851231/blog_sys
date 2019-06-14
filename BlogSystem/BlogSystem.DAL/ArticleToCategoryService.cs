using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class ArticleToCategoryService:BaseService<Models.ArticleToCategory>,IArticleToCategory
    {
        public ArticleToCategoryService() : base(new BlogContext())
        {
        }
    }
}
