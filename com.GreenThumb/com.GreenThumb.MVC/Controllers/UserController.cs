using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.MVC.Models;

namespace com.GreenThumb.MVC.Controllers
{
    /// <summary>
    /// Controller related to the currently logged in user.
    /// 
    /// Created by: Trent Cullinan 03/31/2016
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// View groups for logged in user.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewUserGroups()
        {
            int userId = RetrieveUserId();

            if (0 != userId)
            {
                var groups = new GroupManager().RetrieveUserGroups(userId);
                var groupsView = new List<UserGroupViewModel>(groups.Count());

                foreach (Group group in groups)
                {
                    groupsView.Add(new UserGroupViewModel()
                    {
                        GroupId
                            = group.GroupID,
                        Name
                            = group.Name,
                        LeaderUserName
                            = group.GroupLeader.User.UserName,
                        LeaderEmail
                            = group.GroupLeader.User.EmailAddress,
                        CreatedDate
                            = group.CreatedDate
                    });
                }

                return View(groupsView);
            }

            return View("Error");
        }

        /// <summary>
        /// Logged in user will leave group.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="group">Group Id that is being left.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LeaveGroup(int? group)
        {
            if (group.HasValue)
            {
                int userId = RetrieveUserId();

                if (0 != userId) 
                {
                    if (new GroupManager().LeaveGroup(userId, group.Value))
                    {
                        return RedirectToAction("ViewUserGroups", "User");
                    }
                }
            }

            return View("Error");
        }

        // Created by: Trent Cullinan 03/31/2016
        private int RetrieveUserId()
        {
            int userId = 0;

            var userName = User.Identity.GetUserName();

            if (null != userName)
            {
                userId = new UserManager().RetrieveUserId(userName);
            }

            return userId;
        }
    }
}