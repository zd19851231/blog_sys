using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.DAL;
using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.BLL
{
    public class ArticleManager:IArticleManager
    {
        public async Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId)
        {
            using (var articleSvc = new ArticleService())
            {
                var article = new Article()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };
                await articleSvc.CreateAsync(article);

                Guid articleId = article.Id;
                using (var articleToCategorySvc = new ArticleToCategoryService())
                {
                    foreach (var categoryId in categoryIds)
                    {
                        await  articleToCategorySvc.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            BlogCategoryId = categoryId
                        }, saved: false);
                    }

                    await articleToCategorySvc.Save();

                }
            }

        }

        public async Task CreateCategory(string name, Guid userId)
        {
            using (var categorySvc = new BlogCategoryService())
            {
               await  categorySvc.CreateAsync(new BlogCategory()
                {
                    CategoryName = name,
                    UserId = userId
                });
            }
        }

        public async Task<List<BlogCategoryDto>> GetAllCategories(Guid userId)
        {
            using (IDAL.IBlogCategory categoryService = new BlogCategoryService())
            {
                return await categoryService.GetAllAsync().Where(m=>m.UserId == userId).Select(m=>new Dto.BlogCategoryDto()
                {
                    Id = m.Id,
                    CategoryName = m.CategoryName
                }).ToListAsync();
            }
        }

        public async Task<List<ArticleDto>> GetAllArticlesByUserId(Guid userId)
        {
            using (var articleSvc = new ArticleService())
            {
                 var list = await  articleSvc.GetAllAsync().Include(m=>m.User) .Where(m => m.UserId == userId)
                  .Select(m=>new Dto.ArticleDto()
                  {
                      Title = m.Title,
                      BadCount = m.BadCount,
                      GoodCount = m.GoodCount, 
                      Email = m.User.Email,
                      Content = m.Content,
                      CreateTime = m.CreateTime,
                      Id = m.Id,
                      ImagePath = m.User.ImagePath
                  })
                  .ToListAsync();
                 using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService() )
                 {
                     foreach (var articleDto in list)
                     {
                         var cates = await articleToCategoryService.GetAllAsync().Include(m=>m.BlogCategory).Where(m => m.ArticleId == articleDto.Id).ToListAsync();
                         articleDto.CategoryIds = cates.Select(m => m.BlogCategoryId).ToArray();
                         articleDto.CategoryNames = cates.Select(m => m.BlogCategory.CategoryName).ToArray();
                     }

                     return list;
                 }

            }
        }

        public async Task<List<ArticleDto>> GetAllArticlesByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCategory(string name)
        {
            throw new NotImplementedException();
        }

        public async Task EditCategory(Guid categoryId, string newCategoryName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveArticle(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public async Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds)
        {
            throw new NotImplementedException();
        }
    }
}
