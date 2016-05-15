


using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.MVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Controllers
{
    [Authorize]
    public class GardenTaskController : Controller
    {

        /// <summary>
        /// Author: Poonam Dubey
        /// Date : Apr.15th.2016
        /// Controller to manage the Task
        /// </summary>


        

        // GET: GardenTask
        public ActionResult Index()
        {
            int userId = RetrieveUserId();
            IEnumerable<Group> model = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            GardenTaskViewModel modelObj = new GardenTaskViewModel();
            modelObj.GroupsList = model;
            ViewBag.UserID = userId;

            

            return View(modelObj);

        }

        public ActionResult ViewTask(int gardenID)
        {
            int userId = RetrieveUserId();
            IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            IEnumerable<Task> jobs = new com.GreenThumb.BusinessLogic.JobManager().RetrieveTasksByGardenId(gardenID);
            GardenTaskViewModel modelObj = new GardenTaskViewModel();
            modelObj.GroupsList = groups;
            modelObj.JobList = jobs;
            ViewBag.GardenID = gardenID;
            ViewBag.UserID = userId;

            ViewBag.GroupLeader = new GroupManager().
                    GetLeaderStatus(
                        userId, new GardenManager().RetrieveGardenGroupId(gardenID)
                    );

            return View("Index", modelObj);

        }

        public ActionResult SaveTask(int gardenID, string description, string userNotes)
        {
            int userId = RetrieveUserId();
            ViewBag.GardenID = gardenID;
            Job jobData = new Job();
            jobData.GardenID = gardenID;
            jobData.Description = description;
            jobData.UserNotes = userNotes;
            jobData.DateAssigned = DateTime.Now;
            jobData.AssignedFrom = userId;
            var result = new com.GreenThumb.BusinessLogic.JobManager().AddNewTask(jobData);
            IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            GardenTaskViewModel modelObj = new GardenTaskViewModel();
            modelObj.GroupsList = groups;
            return View("Index", modelObj);

        }

        public ActionResult DeactivateTask(int taskID, int gardenID)
        {
            var result = new com.GreenThumb.BusinessLogic.JobManager().DeactivateTask(taskID);
            int userId = RetrieveUserId();
            IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            IEnumerable<Task> jobs = new com.GreenThumb.BusinessLogic.JobManager().RetrieveTasksByGardenId(gardenID);
            GardenTaskViewModel modelObj = new GardenTaskViewModel();
            modelObj.GroupsList = groups;
            modelObj.JobList = jobs;
            ViewBag.GardenID = gardenID;
            return View("Index", modelObj);
        }


        public ActionResult VolunteerTask(int taskID, int gardenID)
        {
            int userId = RetrieveUserId();
            var data = new com.GreenThumb.BusinessLogic.JobManager().VolunteerForTask(taskID, userId);
            IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            IEnumerable<Task> jobs = new com.GreenThumb.BusinessLogic.JobManager().RetrieveTasksByGardenId(gardenID);
            GardenTaskViewModel modelObj = new GardenTaskViewModel();
            modelObj.GroupsList = groups;
            modelObj.JobList = jobs;
            ViewBag.GardenID = gardenID;
            ViewBag.UserID = userId;
            return View("Index", modelObj);
        }

        public ActionResult CompleteTask(int taskID, int gardenID)
        {
            var data = new com.GreenThumb.BusinessLogic.JobManager().CompleteTask(taskID);
            int userId = RetrieveUserId();
            IEnumerable<Group> groups = new com.GreenThumb.BusinessLogic.GardenManager().GetGardenByUser(userId);
            IEnumerable<Task> jobs = new com.GreenThumb.BusinessLogic.JobManager().RetrieveTasksByGardenId(gardenID);
            GardenTaskViewModel modelObj = new GardenTaskViewModel();
            modelObj.GroupsList = groups;
            modelObj.JobList = jobs;
            ViewBag.GardenID = gardenID;
            ViewBag.UserID = userId;
            return View("Index", modelObj);
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