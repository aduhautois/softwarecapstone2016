using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class ExpertController : Controller
    {

        /// <summary>
        /// Gets Articles
        /// Date: 4/29/16
        /// Author: Chris Schwebach
        /// </summary>
        /// <param name=" UserID"></param>
        /// <returns>Article List</returns>
        public ActionResult Index()
        {
            
            var articles = new BlogManager().GetBlogs();
            List<ArticlesViewModel> blogs = new List<ArticlesViewModel>();
            foreach(Blog blog in articles)
            {
                ArticlesViewModel model = new ArticlesViewModel();
                int userId = blog.CreatedBy;
                var userInfo = new com.GreenThumb.BusinessLogic.UserManager().GetPersonalInfo(userId);
                model.FirstName = userInfo.FirstName;
                model.LastName = userInfo.LastName;
                model.blog = blog;
                blogs.Add(model);
            }

            ViewBag.IsExpert = new GroupManager().IsGroupMember(WebConfigurationManager.AppSettings["ExpertGroup"], RetrieveUserId());

            return View(blogs);
        }


        /// <summary>
        /// Submits a Request to become an expert
        /// Date: 4/24/16
        /// Author: Chris Schwebach
        /// </summary>
        /// <param name="groupID, UserID"></param>
        /// <returns></returns>
        public ActionResult RequestJoinExpert()
        {
            GroupManager grMangr = new GroupManager();
            Group grp = grMangr.RetrieveGroupByName(WebConfigurationManager.AppSettings["ExpertGroup"]);       
            int groupId = grp.GroupID;
            GroupRequest request = new GroupRequest();
            request.UserID = RetrieveUserId();
            request.RequestDate = DateTime.Now;
            request.GroupID = groupId;

            GroupManager manager = new GroupManager();
            try
            {
               if (manager.AddGroupMember(request) == 1)
                 {
                     return RedirectToAction("SuccessRequest", "Expert");
                 }                      

            }
            catch (Exception)
            {
                    //request failed
            }
            return RedirectToAction("AlreadyExpert", "Expert");
        }

        public ActionResult SuccessRequest()
        {
            return View();
        }

        public ActionResult AlreadyExpert()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Articles()
        {
            return View();
        }

        /// <summary>
        /// Submits a Blog from an expert
        /// Date: 4/28/16
        /// Author: Chris Schwebach
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Articles([Bind(Include = "BlogTitle, BlogData")]string blogTitle, string blogData)
        {
                BlogManager blogManager = new BlogManager();
                int UserID = RetrieveUserId();
 
                    Blog blog = new Blog()
                    {
                        BlogTitle = blogTitle,
                        BlogData = blogData,
                        CreatedBy = UserID,
                        DateCreated = DateTime.Now,
                        Active = true
                    };
                    blogManager.AddBlog(blog);
                    return RedirectToAction("Index", "Expert");
        }

        private int RetrieveUserId()
        {
            int userId = 0;

            var userName = User.Identity.GetUserName();

            if (null != userName)
            {
                userId = new UserManager().GetUserId(userName);
            }

            return userId;
        }

    }
}