using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.MVC.Models;
using System.Web.Configuration;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Admin can view request to be Expert.
        /// 
        /// Author: Chris Schwebach
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public ActionResult MngExpert()
        {
            int userId = RetrieveUserId();

            GroupManager grMangr = new GroupManager();
            Group grp = grMangr.RetrieveGroupByName(WebConfigurationManager.AppSettings["ExpertGroup"]);
            int groupId = grp.GroupID;

            var viewModel = new GroupDetailViewModel();
            viewModel.Requests = new List<GroupMemberRequestModel>();
            List<GroupRequest> requests = new GroupManager().GetGroupRequests(groupId);
            foreach (GroupRequest request in requests)
            {
               GroupMemberRequestModel requestModel = new GroupMemberRequestModel();
               requestModel.Request = request;
               requestModel.Requestor = new UserManager().GetUser(request.UserID);
               viewModel.Requests.Add(requestModel);
            }


             return View(viewModel);
       }

        /// <summary>
        /// Admin approves request.
        /// 
        /// Author: Chris Schwebach
        /// </summary>
        /// <param name="groupRequest"></param>
        /// <returns></returns>
        public ActionResult ApproveRequestToJoin(GroupRequest gRequest)
        {
            if (gRequest != null)
            {
                if (gRequest.UserID != 0 && gRequest.GroupID != 0)
                {
                    gRequest.ApprovedBy = RetrieveUserId();
                    gRequest.ApprovedDate = DateTime.Now;
                    if (new GroupManager().UpateAcceptGroupRequest(gRequest))
                    {
                        //accept completed
                    }
                    else
                    {
                        //accppet failed
                    }

                }
            }
            return RedirectToAction("MngExpert", "Admin");
        }

        public ActionResult Experts()
        {
            try
            {
                GroupManager grMangr = new GroupManager();
                Group grp = grMangr.RetrieveGroupByName(WebConfigurationManager.AppSettings["ExpertGroup"]);
                int groupId = grp.GroupID;
                var model = new GroupManager().GetGroupMembers(groupId);
                return View(model);
            }
            catch {
                return View();
            }

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