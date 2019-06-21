using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlogSystem.BLL;
using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModels;
using Webdiyer.WebControls.Mvc;

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
        [ValidateInput(false)]
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
        public async Task<ActionResult> AritcleList(int pageIndex = 0, int pageSize = 1)
        {

            //需要给页面前端 总页码数，当前页码，可显示的总页码数量
            var articleMgr = new ArticleManager();
            var userid = Guid.Parse(Session["userid"].ToString());
            var articles = await articleMgr.GetAllArticlesByUserId(userid, pageIndex, pageSize);
            var dataCount = await articleMgr.GetDataCount(userid);
            ViewBag.PageCount = dataCount % pageSize == 0 ? dataCount / pageSize : dataCount / pageSize + 1;
            ViewBag.PageIndex = pageIndex;
            return View(articles);
        }
        [HttpGet]
        public async Task<ActionResult> AritcleList2(int pageIndex = 1, int pageSize =7)
        {

            //需要给页面前端 总页码数，当前页码，可显示的总页码数量
            var articleMgr = new ArticleManager();
            var userid = Guid.Parse(Session["userid"].ToString());
            //当前用户 第n页数据
            var articles = await articleMgr.GetAllArticlesByUserId(userid, pageIndex-1, pageSize);
            //获取当前用户文章总数
            var dataCount = await articleMgr.GetDataCount(userid);
            
            return View(new PagedList<Dto.ArticleDto>(articles, pageIndex, pageSize, dataCount));
        }

        public async Task<ActionResult> ArticleDetails(Guid? id)
        {
            var articleMgr = new ArticleManager();
            if (id == null || !await articleMgr.ExistsArticle(id.Value))
                return RedirectToAction(nameof(AritcleList));


            ViewBag.Comments = await articleMgr.GetCommentsByArticleId(id.Value);


            return View( await articleMgr.GetOneArticleById(id.Value));
        }

        [HttpGet] 
        public async Task<ActionResult> EditArticle(Guid id)
        {
            IBLL.IArticleManager articleManager = new ArticleManager();
            var data = await articleManager.GetOneArticleById(id);
            var userid = Guid.Parse(Session["userid"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
            return View(new  EditArticleViewModel()
            {
                Title = data.Title,
                Content = data.Content,
                CategoryIds = data.CategoryIds,
                Id = data.Id
            });
        }
        [HttpPost]
        public async Task<ActionResult> EditArticle(Models.ArticleViewModels.EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IArticleManager articleManager = new ArticleManager();
                await articleManager.EditArticle(model.Id, model.Title, model.Content, model.CategoryIds);
                return RedirectToAction("AritcleList2");
            }
            else
            {
                var userid = Guid.Parse(Session["userid"].ToString());
                ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
                return View(model);
            }
          
        }
        [HttpPost]
        public async Task<ActionResult> GoodCount(Guid id)
        {
            IBLL.IArticleManager articleManager = new ArticleManager();
            await articleManager.GoodCountAdd(id);
            return Json(new { result = "ok" });
        }
        [HttpPost]
        public async Task<ActionResult> BadCount(Guid id)
        {
            IBLL.IArticleManager articleManager = new ArticleManager();
            await articleManager.BadCountAdd(id);
            return Json(new { result = "ok" });
        }
        [HttpPost]
        public async Task<ActionResult> AddComment(Models.ArticleViewModels.CreateCommentViewModel model)
        {
            var userid = Guid.Parse(Session["userid"].ToString());
            IBLL.IArticleManager articleManager = new ArticleManager();
            await articleManager.CreateComment(userid, model.Id, model.Content);
            return Json(new {result = "ok"});
        }
    }
}