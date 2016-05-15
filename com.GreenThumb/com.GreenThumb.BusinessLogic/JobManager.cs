using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Retrieve, select and update a task for a garden
    /// Created By: Nasr Mohammed 3/4/2016 
    /// Modified on: 3/15/2016
    /// </summary>
    public class JobManager
    {
        /// <summary>
        /// created by: Nasr Mohammed
        /// Created: 3/4/2016
        /// </summary>
        /// <param name="job"></param>
        /// <returns> if it succssuful will add a task to database</returns>
        public bool AddNewTask(Job job)
        {

            try
            {
                if (JobAccessor.CreateTask(job) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// created by: Nasr Mohammed
        /// Created: 3/4/2016
        /// </summary>
        /// <returns> get a list of jobs</returns>
        public List<Job> GetTaskList()
        {

            try
            {
                return JobAccessor.RetrieveTasks();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(" No records Found!.", ex);
            }

        }

        /// <summary>
        /// created by: Nasr Mohammed
        /// Created: 3/4/2016
        /// </summary>
        /// <param name="job"></param>
        /// <returns> update the task record</returns>
        public bool EditTask(Job job, Job oldJob)
        {

            try
            {
                bool myJob = JobAccessor.UpdateTask(job, oldJob);
                Console.WriteLine(myJob);
                return myJob;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// created by: Nasr Mohammed
        /// Created: 3/4/2016
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns> get a task record</returns>
        public Job GetJob(int jobId)
        {

            try
            {
                return JobAccessor.RetrieveJob(jobId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(" No records Found!", ex);
            }
        }

        /// <summary>
        /// created by: Steve Hoover
        /// Created: 4/1/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> get user by id/returns>
        public List<Job> RetrieveJobByUserId(int userId)
        {
            try
            {
                return JobAccessor.RetrieveJobByUserId(userId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(" No records Found!", ex);
            }
        }


        /// <summary>
        /// created by: Steve Hoover
        /// Created: 4/1/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> get task  by garden id/returns>
        public List<Job> RetrieveJobByGardenId(int gardenId)
        {
            try
            {
                return JobAccessor.RetrieveJobByGardenId(gardenId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No records found!", ex);
            }
        }
        /// <summary>
        /// created by: Steve Hoover
        /// Created: 4/1/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> get garden by user id/returns>
        public List<int> RetrieveGardenIdByUserId(int userId)
        {
            try
            {
                return JobAccessor.RetrieveGardenIdByUserId(userId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No records found!", ex);
            }
        }

        /// <summary>
        /// 
        /// This method adds a test user to validate the functionality of the CompleteTask things.
        /// Steve Hoover 3-24-16
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void AddTestUser(int userId)
        {
            var testJob = new Job
            {
                GardenID = 1000,
                Description = "Digging holes",
                DateAssigned = DateTime.Now,
                // DateCompleted = DateTime.Now,
                AssignedTo = userId,
                AssignedFrom = 1002,
                UserNotes = "Dig holes until you can't feel your arms."
            };
            try
            {
                Console.WriteLine("Test job commented out in JobManager");
                //JobAccessor.CreateTask(testJob);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// created by: Nasr Mohammed
        /// Created: 3/4/2016
        /// </summary>
        /// <param name="userID"></param>
        /// <returns> get a list of garden by userId</returns>
        public List<Garden> GetGardensForUser(int userID)
        {
            List<Garden> gardens = null;

            try
            {
                gardens = JobAccessor.RetrieveUsersGardens(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return gardens;
        }

        /// <summary>
        /// Poonam Dubey
        /// 14th April 2016
        /// Manager function to fetch tasks based on garden
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public List<com.GreenThumb.BusinessObjects.Task> RetrieveTasksByGardenId(int gardenId)
        {
            try
            {
                return JobAccessor.RetrieveTasksByGardenId(gardenId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("No records found!", ex);
            }
        }

        /// <summary>
        /// Poonam Dubey
        /// 18th April 2016
        /// Function to Deactivate a task
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public bool DeactivateTask(int taskID)
        {
            try
            {
                return JobAccessor.DeactivateTask(taskID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error Deactivating task!", ex);
            }
        }

        /// <summary>
        /// Poonam Dubey
        /// 19th April 2016
        /// Function to Volunteer for a task 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool VolunteerForTask(int taskID, int userID)
        {
            try
            {
                return JobAccessor.VolunteerForTask(taskID, userID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error Volunteering for task!", ex);
            }
        }

        /// <summary>
        /// Poonam Dubey
        /// 19th April 2016
        /// Function to Mark task as completed
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool CompleteTask(int taskID)
        {
            try
            {
                return JobAccessor.CompleteTask(taskID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error Marking task as Completed!", ex);
            }
        }

    }
}
