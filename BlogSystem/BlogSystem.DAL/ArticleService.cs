using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class ArticleService:BaseService<Models.Article>,IArticleService
    {
        public ArticleService( ) : base(new BlogContext())
        {
        }
    }
}
