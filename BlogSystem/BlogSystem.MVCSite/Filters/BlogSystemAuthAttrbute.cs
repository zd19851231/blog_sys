using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSystem.MVCSite.Filters
{
    public class BlogSystemAuthAttribute:AuthorizeAttribute
    {
       
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当用户存储在cookie中且session数据为空时，把cookie的数据同步到session中
            if (filterContext.HttpContext.Request.Cookies["loginName"] != null &&
                filterContext.HttpContext.Session["loginName"] == null)
            {
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"].Value;
                filterContext.HttpContext.Session["userid"] = filterContext.HttpContext.Request.Cookies["userid"].Value;
            }


           // base.OnAuthorization(filterContext);
            if (!(filterContext.HttpContext.Session["loginName"] != null ||
                filterContext.HttpContext.Request.Cookies["loginName"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Home" },
                    {"action","Login" }
                });
            }
        }
    }
}