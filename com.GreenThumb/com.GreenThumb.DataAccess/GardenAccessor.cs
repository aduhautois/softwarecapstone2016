using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Garden Accessor by Poonam Dubey
    /// </summary>
    public class GardenAccessor
    {
        /// <summary>
        /// Function used to create Gardens
        /// Author: Poonam Dubey
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        public static bool CreateGarden(Garden garden)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spInsertGardens";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", garden.GroupID);
            cmd.Parameters.AddWithValue("@UserID", garden.UserID);
            cmd.Parameters.AddWithValue("@GardenDescription", garden.GardenDescription);
            cmd.Parameters.AddWithValue("@GardenRegion", garden.GardenRegion);
            cmd.Parameters.AddWithValue("@GardenName", garden.GardenName);
            bool updated = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    updated = true;
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

            return updated;
        }

        /// <summary>
        /// Created by: Kristine
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="garden"></param>
        /// <returns></returns>
        public static bool CreateAddGarden(Garden garden)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spInsertGardens";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", garden.GroupID);
            cmd.Parameters.AddWithValue("@GardenName", garden.GardenName);
            cmd.Parameters.AddWithValue("@UserID", garden.UserID);
            cmd.Parameters.AddWithValue("@GardenDescription", garden.GardenDescription);
            cmd.Parameters.AddWithValue("@GardenRegion", garden.GardenRegion);
            bool updated = false;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    updated = true;
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

            return updated;
        }


        /// <summary>
        /// Accessor function fetch all gardens : Poonam Dubey  (20th March 2016)
        /// </summary> 
        /// <returns></returns>
        public static List<Garden> RetrieveGardens()
        {
            // create a list to hold the returned data
            var gardenList = new List<Garden>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection
            string query = "sp_GetGardens";

            // create a command object - SP
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute the command and return a data reader
                SqlDataReader reader = cmd.ExecuteReader();

                // before trying to read the reader, be sure it has data
                if (reader.HasRows)
                {
                    // now we just need a loop to process the reader
                    while (reader.Read())
                    {
                        Garden garden = new Garden()
                        {
                           
                        };


                        gardenList.Add(garden);
                    }
                }

            }
            catch (Exception)
            {
                // rethrow all Exceptions, let the logic layer sort them out
                throw;
            }
            finally
            {
                conn.Close();
            }
            // this list may be empty, if so, the logic layer will need to deal with it
            return gardenList;
        }

        /// <summary>
        /// Author: Chris Schwebch
        /// Updated: Nick King
        /// Date: 04/6/16
        /// Gets gardens the user belongs to returns garen list
        /// </summary> 
        public static List<Group> RetrieveGardenInfo(int userID)
        {
            //var gardenInfo = new List<Garden>();

            string groupName = null;
            Group _Group = new Group();

            List<Group> groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            string query = "Gardens.spSelectUserGardens";

            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            //cmd.Parameters.AddWithValue("@Active", 1);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (groupName == null || !groupName.Equals(reader.GetString(6)))
                        {

                            _Group = new Group();
                            _Group.GardenList = new List<Garden>();
                            groupName = reader.GetString(6);
                            _Group.Name = reader.GetString(6);
                            //_Group.GroupLeader = reader.String(7);
                            _Group.GroupID = reader.GetInt32(1);
                            groupList.Add(_Group);

                        }
                        if (groupName.Equals(reader.GetString(6)))
                        {
                            Garden _Garden = new Garden();
                            _Garden.GardenID = reader.GetInt32(0);
                            _Garden.GroupID = reader.GetInt32(1);
                            _Garden.GardenName = reader.GetString(2);
                            _Garden.UserID = reader.GetInt32(3);
                            _Garden.GardenDescription = reader.GetString(4);
                            _Garden.GardenRegion = reader.GetString(5);


                            _Group.GardenList.Add(_Garden);
                        }


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

            return groupList;
        }

        /// <summary>
        /// Retrieve the gardens that belong to a group.
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="groupId">Group identifier to retrieve groups by.</param>
        /// <returns>Collection of gardens</returns>
        public static IEnumerable<Garden> RetrieveGroupGardens(int groupId)
        {
            List<Garden> gardens = new List<Garden>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectGroupGardens", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID",
                groupId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    gardens.Add(new Garden()
                    {
                        GardenID
                            = reader.GetInt32(0),
                        GardenName
                            = reader.GetString(1),
                        GardenDescription
                            = reader.GetString(2)
                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return gardens;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/21/16
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public static Group RetrieveGroupByGarden(int gardenId)
        {
            Group group = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectGroupByGardenID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID", gardenId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    group = new Group()
                    {
                        GroupID 
                            = reader.GetInt32(0),
                        Name 
                            = reader.GetString(2),
                        GroupLeader = new GroupMember()
                        {
                            User = new User()
                            {
                                UserID = reader.GetInt32(1)
                            }
                        }
                    };
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return group;
        }
    }
}
