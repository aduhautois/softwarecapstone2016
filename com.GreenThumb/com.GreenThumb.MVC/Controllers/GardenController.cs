using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.GreenThumb.MVC.Models;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class GardenController : Controller
    {
        /// <summary>
        /// GET the list of gardens you belong to by group.
        /// Author: Chris Schwebach 4/18/2016
        /// </summary>
        /// <returns>GardenList by group</returns>
        public ActionResult Index()
        {
            int userId = RetrieveUserId();
            var model = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            return View(model);
       
        }

        /// <summary>
        /// Created by: Kristine Johnson
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult CreateGarden(int? id)
        {
            ActionResult view = RedirectToAction("Details", "Group", new { @id = id });

            if (id.HasValue)
            {
                GardenCreationViewModel model = new GardenCreationViewModel()
                {
                    GroupID = id.Value
                };

                view = View(model);
            }

            return view;
        }


        [HttpPost]
        public ActionResult CreateGarden(GardenCreationViewModel model)
        {
            ActionResult result = View(model);

            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();

                Garden garden = new Garden();

                ///using Trent's helper method to get a userID
                garden.UserID = RetrieveUserId();
                garden.GardenDescription = model.GardenDescription;
                garden.GardenName = model.GardenName;
                garden.GroupID = model.GroupID;
                garden.GardenRegion = model.RegionID.ToString();


                GardenManager gardenManager = new GardenManager();

                if (gardenManager.AddGarden(garden))
                {
                    ViewBag.StatusMessage = "Your garden was created!";
                }

                result = RedirectToAction("Details", "Group", new { id = garden.GroupID });
            }

            return result;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GardenDetail(int? id)
        {
            ActionResult viewResult = RedirectToAction("Index", "Garden");

            if (id.HasValue)
            {
                
                GardenDetailViewModel model = new GardenDetailViewModel()
                {
                    GardenID = id.Value
                };

                int userId = RetrieveUserId();

                GardenNeedsManager needsManager = new GardenNeedsManager(userId, id.Value);

                model.ActiveNeeds = needsManager.RetrieveActiveNeeds();

                ViewBag.GroupLeader = new GroupManager().
                    GetLeaderStatus(
                        userId, new GardenManager().RetrieveGardenGroupId(id.Value)
                    );

                if (ViewBag.GroupLeader)
                {
                    model.PendingContributions = needsManager.RetrievePendingContributions();
                }

                model.CompletedNeeds = needsManager.RetrieveCompletedNeeds();

                viewResult = View(model);
            }

            return viewResult;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddNeed(int? id)
        {
            ActionResult viewResult = RedirectToAction("Index", "Garden");

            if (id.HasValue)
            {
                CreateNeedViewModel model = new CreateNeedViewModel()
                {
                    GardenId = id.Value
                };

                viewResult = View(model);
            }

            return viewResult;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNeed(CreateNeedViewModel model)
        {
            ActionResult viewResult = View(model);

            if (ModelState.IsValid)
            {
                int userId = RetrieveUserId();

                GardenNeedsManager needsManager = new GardenNeedsManager(userId, model.GardenId);

                GardenNeed need = new GardenNeed()
                {
                    Title 
                        = model.Title,
                    Description 
                        = model.Description,
                };

                if (needsManager.AddNeed(need))
                {
                    viewResult = RedirectToAction("GardenDetail", new { id = model.GardenId });
                }
                else
                {
                    viewResult = View("Error"); // CHANGE
                }
            }

            return viewResult;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ApproveContribution(int? id, int? contributionId)
        {
            ActionResult viewResult = View("Error");

            int userId = RetrieveUserId();

            if (id.HasValue && contributionId.HasValue)
            {
                if (new GroupManager().
                    GetLeaderStatus(
                        userId, new GardenManager().RetrieveGardenGroupId(id.Value)
                    ))
                {
                    if (new GardenNeedsManager(userId, id.Value).ApproveContribution(contributionId.Value))
                    {
                        viewResult = RedirectToAction("Index", "Garden");
                    }
                }
            }

            return viewResult;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult DeclineContribution(int? id, int? contributionId)
        {
            ActionResult viewResult = View("Error");

            int userId = RetrieveUserId();

            if (id.HasValue && contributionId.HasValue)
            {
                if (new GroupManager().
                    GetLeaderStatus(
                        userId, new GardenManager().RetrieveGardenGroupId(id.Value)
                    ))
                {
                    if (new GardenNeedsManager(userId, id.Value).DeclineContribution(contributionId.Value))
                    {
                        viewResult = RedirectToAction("Index", "Garden");
                    }
                }
                
            }

            return viewResult;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CloseNeed(int? id, int? needId)
        {
            ActionResult viewResult = View("Error");

            if (id.HasValue && needId.HasValue)
            {
                if (new GardenNeedsManager(RetrieveUserId(), id.Value).RemoveNeed(needId.Value))
                {
                    viewResult = Redirect(Request.UrlReferrer.ToString());
                }
            }

            return viewResult;
        }


        #region Helper Methods

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

        #endregion

    }
}