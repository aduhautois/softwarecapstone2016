using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Retrieve, select and update a task for a garden
    /// Created By: Nasr Mohammed 3/4/2016 
    /// Modified on: 3/15/2016
    /// </summary>
    public class JobAccessor
    {
        /// <summary>
        /// Retrieve a list of tasks.
        /// Created By: Nasr Mohammed 3/4/2016 Modified 3/15/2016
        /// </summary>
        /// <returns>A list of tasks.</returns>
        public static List<Job> RetrieveTasks()
        {

            List<Job> jobs = new List<Job>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.spSelectTasks";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Job job = new Job()
                          {
                        JobID = reader.GetInt32(0),
                        GardenID = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        DateAssigned = reader.GetDateTime(3),
                        AssignedFrom = reader.GetInt32(4),
                        UserNotes = reader.GetString(5),
                        Active = reader.GetBoolean(6)
                    };
                        jobs.Add(job);
                    }
                }
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return jobs;
        }

       
        /// <summary>
        /// Update a task in a garden.
        /// Created By: Nasr Mohammed 3/4/2016 
        /// Modified on: 3/15/2016
        /// </summary>
        /// <param name="job">The task field that should be updated </param>
        /// <param name="originalJob">The orignial taks field</param>
        /// <returns>A boolean if the task updated successfully</returns>
        public static bool UpdateTask(Job job, Job originalJob)
        {

            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spUpdateTasks";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskID", job.JobID);
            cmd.Parameters.AddWithValue("@Description", job.Description);
            cmd.Parameters.AddWithValue("@dateAssigned", job.DateAssigned);
            cmd.Parameters.AddWithValue("@dateCompleted", job.DateCompleted);
            cmd.Parameters.AddWithValue("@assignedTo", job.AssignedTo);
            cmd.Parameters.AddWithValue("@assignedFrom", job.AssignedFrom);
            cmd.Parameters.AddWithValue("@userNotes", job.UserNotes);
            cmd.Parameters.AddWithValue("@active", job.Active);

            //cmd.Parameters.AddWithValue("@OriginalgardenID", originalJob.GardenID);
            //cmd.Parameters.AddWithValue("@OriginalDescription", originalJob.Description);
            //cmd.Parameters.AddWithValue("@OriginalActive", originalJob.Active);

            bool flag = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    Console.WriteLine("Accessor works");
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Accessor broken");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }

        /// <summary>
        /// Insert a task in a garden.
        /// Created By: Nasr Mohammed 3/4/2016 
        /// Modified on: 3/15/2016
        /// </summary>
        /// <param name="job">The task that should be created </param>
        /// <returns>A rowsAffected if it's inserted successfully</returns>
        public static int CreateTask(Job job)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"Gardens.spInsertTasks";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@gardenID", job.GardenID);
            cmd.Parameters.AddWithValue("@description", job.Description);
            cmd.Parameters.AddWithValue("@dateAssigned", job.DateAssigned);
            cmd.Parameters.AddWithValue("@assignedFrom", job.AssignedFrom);
            cmd.Parameters.AddWithValue("@userNotes", job.UserNotes);

            try
            {
                conn.Open();
                rowsAffected = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Select a task based on task ID.
        /// Created By: Nasr Mohammed 3/4/2016 Modified 3/15/2016
        /// </summary>
        /// <param name="job">The taskID should be passed to retrive a task </param>
        /// <returns>specific task.</returns>
        public static Job RetrieveJob(int jobId)
        {
            Job job = new Job();

            var conn = DBConnection.GetDBConnection();
            var query = @"SELECT TaskID, GardenID, Description , DateAssigned, DateCompleted, AssignedTo, AssignedFrom, UserNotes, Active " +
                         @"FROM Gardens.Tasks ";
            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@JobID", jobId);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    job = new Job()
                    {
                        JobID = reader.GetInt32(0),
                        GardenID = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        DateAssigned = reader.GetDateTime(3),
                        DateCompleted = reader.GetDateTime(4),
                        AssignedTo = reader.GetInt32(5),
                        AssignedFrom = reader.GetInt32(6),
                        UserNotes = reader.GetString(7),
                        Active = reader.GetBoolean(8)
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return job;
        }

        public static List<Job> RetrieveJobByUserId(int userId)
        {
            var jobs = new List<Job>();
            var conn = DBConnection.GetDBConnection();
            // need to send Chris stored procedure
            var query = @"SELECT TaskID, GardenID, Description , DateAssigned, DateCompleted, AssignedTo, AssignedFrom, UserNotes, Active " +
                         @"FROM Gardens.Tasks " +
                         @"WHERE AssignedTo=" + userId + "AND Active=1";
            var cmd = new SqlCommand(query, conn);

            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var job = new Job();

                        job.JobID = reader.GetInt32(0);
                        job.GardenID = reader.GetInt32(1);
                        job.Description = reader.GetString(2);
                        job.DateAssigned = reader.GetDateTime(3);
                        job.DateCompleted = reader.GetDateTime(4);
                        job.AssignedTo = reader.GetInt32(5);
                        job.AssignedFrom = reader.GetInt32(6);
                        job.UserNotes = reader.GetString(7);
                        job.Active = reader.GetBoolean(8);

                        jobs.Add(job);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return jobs;
        }
        /// <summary>
        /// Select a task based on gardenId
        /// Created by Steve Hoover 3/31/16
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public static List<Job> RetrieveJobByGardenId(int gardenId)
        {
            var jobs = new List<Job>();
            var conn = DBConnection.GetDBConnection();
            // need to send Chris stored procedure
            var query = @"SELECT TaskID, GardenID, Description , DateAssigned, DateCompleted, AssignedTo, AssignedFrom, UserNotes, Active " +
                         @"FROM Gardens.Tasks " +
                         @"WHERE GardenID=" + gardenId + "AND Active=1";
            var cmd = new SqlCommand(query, conn);

            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var job = new Job();

                        job.JobID = reader.GetInt32(0);
                        job.GardenID = reader.GetInt32(1);
                        job.Description = reader.GetString(2);
                        job.DateAssigned = reader.GetDateTime(3);
                        job.DateCompleted = reader.GetDateTime(4);
                        job.AssignedTo = reader.GetInt32(5);
                        job.AssignedFrom = reader.GetInt32(6);
                        job.UserNotes = reader.GetString(7);
                        job.Active = reader.GetBoolean(8);

                        jobs.Add(job);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return jobs;
        }

        public static List<int> RetrieveGardenIdByUserId(int userId)
        {
            var ints = new List<int>();
            var conn = DBConnection.GetDBConnection();
            // need to send Chris stored procedure
            var query = @"SELECT GardenID " +
                         @"WHERE UserID=" + userId + "AND Active=1";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ints.Add(reader.GetInt32(0));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return ints;
        }

        public static List<Garden> RetrieveUsersGardens(int userID, Active recordType = Active.active)
        {
            var gardenList = new List<Garden>();

            var conn = DBConnection.GetDBConnection();

            string cmdText = @"Gardens.spSelectListOfGardens";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Garden currentGroup = new Garden()
                        {
                            GardenID = reader.GetInt32(0),
                            GardenName = reader.GetString(1)
                        };
                        gardenList.Add(currentGroup);
                    }
                }
                else
                {
                    var msg = new ApplicationException("No garden found");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return gardenList;
        }


        /// <summary>
        /// Poonam Dubey
        /// 14th April 2016
        /// Funtion to fetch tasks based on gardenid  
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public static List<com.GreenThumb.BusinessObjects.Task> RetrieveTasksByGardenId(int gardenId)
        {
            var tasks = new List<com.GreenThumb.BusinessObjects.Task>();
            var conn = DBConnection.GetDBConnection();
            string cmdText = @"Gardens.spSelectTasksForGarden";

            // need to send Chris stored procedure
            //var query =   @"SELECT TaskID, Description , CONVERT(VARCHAR(20),DateAssigned) 'AssignedOn',   ISNULL(CONVERT(VARCHAR(20),DateCompleted),'') 'CompletedOn', ISNULL(AUU.FirstName + ' ' + AUU.LastName,'') 'AssignedTo', AU.FirstName + ' ' + AU.LastName 'AssignedBy' , UserNotes, GT.Active " +
            //              @"FROM Gardens.Tasks GT INNER JOIN Admin.Users AU " +
            //              @"ON GT.AssignedFrom = AU.UserID " + 
            //              @"LEFT JOIN  Admin.Users AUU ON GT.AssignedTo = AUU.UserID " +
            //              @"WHERE GardenID=" + gardenId + " AND GT.Active=1";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID", gardenId);

            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var task = new com.GreenThumb.BusinessObjects.Task();

                        task.TaskID = reader.GetInt32(0);
                        task.TaskDescription = reader.GetString(1);
                        task.AssignedOn = reader.GetString(2);
                        task.CompletedOn = reader.GetString(3);
                        task.AssignedTo = reader.GetString(4);
                        task.AssignedBy = reader.GetString(5);
                        task.UserNotes = reader.GetString(6);
                        task.Active = reader.GetBoolean(7);
                        task.AssignedToUserID = reader.GetInt32(8);
                        tasks.Add(task);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return tasks;
        }

        /// <summary>
        /// Poonam Dubey
        /// 18th April 2016
        /// Function to deactivate task
        /// </summary>
        /// <param name="taskID"></param>
        public static bool DeactivateTask(int taskID)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spDeactivateTask";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskID", taskID);

            bool flag = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }


        /// <summary>
        /// Poonam Dubey
        /// 19th April 2016
        /// Data access class to volunteer for a task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool VolunteerForTask(int taskID, int userID)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spVolunteerForTask";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskID", taskID);
            cmd.Parameters.AddWithValue("@UserID", userID);

            bool flag = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }

        /// <summary>
        /// Poonam Dubey
        /// 19th April 2016
        /// Function to mark a task as completed
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static bool CompleteTask(int taskID)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spMarkTaskAsComplete";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskID", taskID);

            bool flag = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}
