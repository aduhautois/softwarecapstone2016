using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.MVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Controllers
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/14/16
    /// </summary>
    public class DonationController : Controller
    {
        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult view = View("Error");

            IEnumerable<GroupNeedSummary> summary = UserNeedsManager.RetrieveGroupsOfNeed();

            if (null != summary)
            {
                view = View(summary);
            }

            return view;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewGroupNeeds(int? id)
        {
            ActionResult view = View("Error");

            if (id.HasValue)
            {
                GroupNeedsDetailViewModel model = new GroupNeedsDetailViewModel();

                model.GardenSummaries = UserNeedsManager.RetrieveGroupGardensOfNeed(id.Value);

                if (null != model.GardenSummaries)
                {
                    model.ActiveNeeds = GardenNeedsManager.RetrieveGroupNeeds(id.Value);
                }

                if (null != model.ActiveNeeds || null != model.GardenSummaries)
                {
                    view = View(model);
                }
            }

            return view;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewGardenNeeds(int? id)
        {
            ActionResult view = View("Error");

            if (id.HasValue)
            {
                GardenNeedsDetailViewModel model = new GardenNeedsDetailViewModel();

                model.ActiveNeeds = GardenNeedsManager.RetrieveGardenNeeds(id.Value);

                if (null != model.ActiveNeeds)
                {
                    view = View(model);
                }
            }

            return view;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult SendContribution(int? id)
        {
            ActionResult view = View("Error");

            if (id.HasValue)
            {
                SendContributionViewModel model = new SendContributionViewModel();

                GardenNeed need = UserNeedsManager.RetrieveNeed(id.Value);

                model.NeedTitle 
                    = need.Title;
                model.NeedID 
                    = need.GardenNeedId;

                view = View(model);
            }

            return view;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult SendContribution(SendContributionViewModel model)
        {
            ActionResult view = View(model);

            if (ModelState.IsValid)
            {
                int userID = RetrieveUserId();

                NeedContribution contribution = new NeedContribution()
                {
                    Need = new GardenNeed()
                    {
                        GardenNeedId 
                            = model.NeedID
                    },
                    Description 
                        = model.Description,
                    SentBy = new User()
                    {
                        UserID 
                            = userID
                    }
                };

                if (new UserNeedsManager(userID).SendContribution(contribution))
                {
                    view = RedirectToAction("ViewContributions", "Donation");
                }
            }

            return view;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ViewContributions()
        {
            ActionResult view = View("Error");

            int userId = RetrieveUserId();

            IEnumerable<NeedContribution> collection = new UserNeedsManager(userId).RetrieveAllContributions();

            if (null != collection)
            {
                ContributionsDetailViewModel model = new ContributionsDetailViewModel();

                model.PendingContributions 
                    = collection.Where(n => !n.Contributed.HasValue);
                collection 
                    = collection.Where(n => n.Contributed.HasValue);
                model.ApprovedContributions 
                    = collection.Where(n => n.Contributed.Value);
                model.DeclinedContributions 
                    = collection.Where(n => !n.Contributed.Value);

                view = View(model);
            } 

            return view;
        }

        // Created By: Trent Cullinan 04/14/16
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