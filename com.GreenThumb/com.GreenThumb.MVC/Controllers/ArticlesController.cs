using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult Articles()
        {
            var articles = new BlogManager().GetBlogs();
            List<ArticlesViewModel> blogs = new List<ArticlesViewModel>();
            foreach (Blog blog in articles)
            {
                ArticlesViewModel model = new ArticlesViewModel();
                int userId = blog.CreatedBy;
                var userInfo = new com.GreenThumb.BusinessLogic.UserManager().GetPersonalInfo(userId);
                model.FirstName = userInfo.FirstName;
                model.LastName = userInfo.LastName;
                model.blog = blog;
                blogs.Add(model);
            }
            return View(blogs);
        }
    }
}