using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
using Microsoft.AspNet.Identity;

namespace com.GreenThumb.MVC.Controllers
{
    public class HomeController : Controller
    {
        // created by Luke Frahm
        public ActionResult Index()
        {
            var am = new AnnouncementManager();
            var al = new List<Announcements>();
            int userID = RetrieveUserId();
            if (userID != 0)
            {
                al = am.GetAnnouncementsByGroupIDTop10(userID);
            }
            if (al.Count == 0)
            {
                al.Add(new Announcements(0, "", "", "", 0, DateTime.Now, ""));
            }
            if (userID != 0 && al.Count == 0)
            {
                al.Add(new Announcements(0, "", "", "", 0, DateTime.Now, "No new announcements!"));
            }
            ViewBag.Announcements = al;
			
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // Created by: Trent Cullinan 03/31/2016
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