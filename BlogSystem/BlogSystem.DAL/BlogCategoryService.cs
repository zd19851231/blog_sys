using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class BlogCategoryService:BaseService<Models.BlogCategory>,IBlogCategory
    {
        public BlogCategoryService() : base(new BlogContext())
        {
        }
    }
}
