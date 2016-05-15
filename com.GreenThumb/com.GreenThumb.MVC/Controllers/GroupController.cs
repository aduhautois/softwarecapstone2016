using com.GreenThumb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
using System.Web.Configuration;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        // GET: Group
        /// <summary>
        /// Created by: Trent Cullinan 03/31/2016
        /// Displays list of groups a User belongs to 
        /// 
        /// Modified by: Nicholas King 04/03/2016
        /// Merged grouplist display and create group
        /// added join group list table
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            GroupIndexViewModel model = new GroupIndexViewModel();
            int userId = RetrieveUserId();

            if (0 != userId)
            {
                var groups = new GroupManager().GetUserGroups(userId);
                model.UserGroupList = new List<GroupIndexViewModel.UserGroupViewModel>(groups.Count());

                foreach (Group group in groups)
                {
                    model.UserGroupList.Add(new GroupIndexViewModel.UserGroupViewModel()
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

                var joinableGroups = new GroupManager().GetGroupsToJoin(userId);
                model.NonUserGroupList = new List<GroupIndexViewModel.UserGroupViewModel>(joinableGroups.Count());

                foreach (Group group in joinableGroups)
                {
                    if (WebConfigurationManager.AppSettings["ExpertGroup"] != group.Name)
                    {
                        model.NonUserGroupList.Add(new GroupIndexViewModel.UserGroupViewModel()
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
                }


                return View(model);
            }

            return View("Error");
        }

        /// <summary>
        /// Logged in user will leave group.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="id">Group Id that is being left.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LeaveGroup(int? id)
        {
            if (id.HasValue)
            {
                int userId = RetrieveUserId();

                if (0 != userId)
                {
                    if (new GroupManager().EditLeaveGroup(userId, id.Value))
                    {
                        return RedirectToAction("Index", "Group");
                    }
                }
            }

            return View("Error");
        }
        [HttpGet]
        public ActionResult CreateAnnouncement(int? id)
        {
            AnnouncementViewModel model = new AnnouncementViewModel();
            model.GroupID = id.Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAnnouncement([ModelBinder(typeof(AnnouncementViewModel))]AnnouncementViewModel AVM)
        {
            int userID = RetrieveUserId();
            int groupID = AVM.GroupID;

            try
            {
                new AnnouncementManager().CreateAnnouncement((int)AVM.GroupID, AVM.Content, User.Identity.GetUserName());
            }
            catch (Exception)
            {
                return View("Error");
            }
         
            

            return RedirectToAction("Details", "Group");
        }

        /// <summary>
        /// Logged in user will view group details
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// Modified by: Nicholas King
        /// Last word by: Trent Cullinan 05/05/2016
        /// </summary>
        /// <param name="id">Group Id</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            ActionResult view = RedirectToAction("Index", "Group");

            if (id.HasValue)
            {
                GroupManager groupManager = new GroupManager();

                var group
                    = groupManager.GetGroup(id.Value);
                var gardens
                    = new GardenManager().GetGroupGardens(id.Value);

                var viewModel = new GroupDetailViewModel()
                {
                    GroupID
                        = group.GroupID,
                    GroupName
                        = group.Name,
                    GroupLeader
                        = ConvertGroupMember(group.GroupLeader),
                    GroupMembers
                        = ConvertGroupMemberCollection(group.UserList),
                    Gardens
                        = ConvertGardenCollection(gardens)
                };

                //Added by Nicholas King
                if (ViewBag.GroupLeader = groupManager.GetLeaderStatus(RetrieveUserId(), id.Value))//do check for if user is group leader
                {
                    viewModel.Requests = new List<GroupMemberRequestModel>();
                    List<GroupRequest> requests = groupManager.GetGroupRequests(id.Value);
                    foreach (GroupRequest request in requests)
                    {
                        GroupMemberRequestModel requestModel = new GroupMemberRequestModel();
                        requestModel.Request = request;
                        requestModel.Requestor = new UserManager().GetUser(request.UserID);

                        viewModel.Requests.Add(requestModel);
                    }
                }

                if (WebConfigurationManager.AppSettings["ExpertGroup"] == group.Name)
                {
                    int userId = RetrieveUserId();

                    // I am mimicking what ever happens on the tasks side of things.
                    IEnumerable<Group> groups 
                        = new GardenManager().GetGardenByUser(userId)
                        .Where(g => g.Name == WebConfigurationManager.AppSettings["ExpertGroup"]);

                    ExpertGroupDetailViewModel model = new ExpertGroupDetailViewModel()
                    {
                        GroupDetail 
                            = viewModel,
                        ExpertTaskDetail = new GardenTaskViewModel()
                        {
                            GroupsList = groups,
                        }
                    };

                    ViewBag.UserID = userId;

                    view = View("ExpertDetails", model);
                }
                else
                {
                    view = View(viewModel);
                }


            }
            return view;
        }

        /// <summary>
        /// Approves a join request
        /// Created by Nicholas King
        /// </summary>
        /// <param name="gRequest"></param>
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
            return RedirectToAction("Details", "Group", gRequest.GroupID);
        }

        /// <summary>
        /// Submits a GroupRequest
        /// 
        /// Created by: Nicholas King
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RequestJoinGroup(int? id)
        {
            if (id != null)
            {
                GroupRequest request = new GroupRequest();
                request.UserID = RetrieveUserId();
                request.RequestDate = DateTime.Now;
                request.GroupID = (int)id;

                GroupManager manager = new GroupManager();
                try
                {
                    if (manager.AddGroupMember(request) == 1)
                    {
                        return RedirectToAction("Index", "Group");
                    }

                }
                catch (Exception)
                {
                    //request failed
                }
            }
            return RedirectToAction("Index", "Group");
        }



        // GET: Group/Create
        public ActionResult Create()
        {
            var userName = User.Identity.GetUserName();
            var model = new com.GreenThumb.BusinessLogic.UserManager().GetUserByUserName(userName);
            return View(model);
        }

        // POST: Group/Create
        [HttpPost]


        //take in userid and group name/create something to bind these too.
        public ActionResult Create([Bind(Include = "GroupName")]string groupName)
        {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                var user = userManager.GetUserByUserName(User.Identity.GetUserName());

                try
                {
                    com.GreenThumb.BusinessLogic.GroupManager groupManager = new BusinessLogic.GroupManager();
                    groupManager.AddGroup(user.UserID, groupName); //hard coded garbage data-need to replace supplied by request
                    ViewBag.StatusMessage = "Your group was created!";

                }
                catch
                {
                    return View();
                }


            }
            return RedirectToAction("Index", "Group");
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

        // Created by: Trent Cullinan 04/05/2016
        private IEnumerable<GroupMemberViewModel> ConvertGroupMemberCollection(IEnumerable<GroupMember> groupMembers)
        {
            var groupMembersView = new List<GroupMemberViewModel>();

            foreach (GroupMember groupMember in groupMembers)
            {
                groupMembersView.Add(ConvertGroupMember(groupMember));
            }

            return groupMembersView;
        }

        // Created by: Trent Cullinan 04/05/2016
        private GroupMemberViewModel ConvertGroupMember(GroupMember groupMember)
        {
            return new GroupMemberViewModel()
            {
                UserID
                    = groupMember.User.UserID,
                Email
                    = groupMember.User.EmailAddress,
                UserName
                    = groupMember.User.UserName,
                FirstName
                    = groupMember.User.FirstName,
                LastName
                    = groupMember.User.LastName,
                DateCreated
                    = groupMember.CreatedDate
            };
        }

        // Created by: Trent Cullinan 04/05/2016
        private IEnumerable<GroupGardenViewModel> ConvertGardenCollection(IEnumerable<Garden> gardens)
        {
            var gardensView = new List<GroupGardenViewModel>();

            foreach (Garden garden in gardens)
            {
                gardensView.Add(ConvertGarden(garden));
            }

            return gardensView;
        }

        // Created by: Trent Cullinan 04/05/2016
        private GroupGardenViewModel ConvertGarden(Garden garden)
        {
            return new GroupGardenViewModel()
            {
                GardenID
                    = garden.GardenID,
                GardenName
                    = garden.GardenName,
                Description
                    = garden.GardenDescription
            };
        }




        #endregion
    }
}
