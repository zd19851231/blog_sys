using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlogSystem.BLL;
using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModels;

namespace BlogSystem.MVCSite.Controllers
{
    [BlogSystemAuth]
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            { 
               IBLL.IArticleManager articleManager = new ArticleManager(); 
                articleManager.CreateCategory(model.CategoryName, Guid.Parse(Session["userid"].ToString()));
                return RedirectToAction("CategoryList");
            }
            ModelState.AddModelError("","您录入的信息有误");
            return View(model);
        }
        [HttpGet] 
        public async Task<ActionResult> CategoryList()
        {
            var userid = Guid.Parse(Session["userid"].ToString());
            return View( await new ArticleManager().GetAllCategories(userid));
        }
        [HttpGet] 
        public async Task<ActionResult> CreateArticle()
        {
            var userid = Guid.Parse(Session["userid"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
            return View();
        }
        [HttpPost] 
        public async Task<ActionResult> CreateArticle(Models.ArticleViewModels.CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userid = Guid.Parse(Session["userid"].ToString());
                await new ArticleManager().CreateArticle(model.Title, model.Content, model.CategoryIds, userid);
                return RedirectToAction("AritcleList");
            }
            ModelState.AddModelError("","添加失败");
            return View( );
        }
        [HttpGet]
        public async Task<ActionResult> AritcleList()
        {
            var userid = Guid.Parse(Session["userid"].ToString());
            var articles = await new ArticleManager().GetAllArticlesByUserId(userid);
            return View(articles);
        }
    }
}