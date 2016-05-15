using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Created By: Luke Frahm 4/22/2016
    /// Data access class that does CRUD operations on Gardens.Announcements database table.
    /// </summary>
    public class AnnouncementAccessor
    {
        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Take a new announcement created by user and add it to the database.
        /// </summary>
        /// <param name="announcement">Announcement object with all object details.</param>
        /// <returns>Boolean based on procedure result.</returns>
        public static bool CreateAnnouncement(int groupID, string Content, User user)
        {
            int result;
            bool succeded;
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.spInsertAnnouncements";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("UserName", user.UserName);
            cmd.Parameters.AddWithValue("FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("LastName", user.LastName);
            cmd.Parameters.AddWithValue("GroupID", groupID);
            cmd.Parameters.AddWithValue("Date", DateTime.Now);
            cmd.Parameters.AddWithValue("Announcement", Content);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    succeded = true;
                }
                else
                {
                    succeded = false;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Announcement could not be sent.");
            }
            finally
            {
                conn.Close();
            }
            return succeded;
        }

        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Retrieve all announcements linked to a GroupID.
        /// </summary>
        /// <param name="announcement">Announcement object with all object details.</param>
        /// <returns>List of Announcements based on procedure result.</returns>
        public static List<Announcements> RetrieveAnnouncementsByGroupID(int groupID)
        {
            var anouncements = new List<Announcements>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.SelectAnnouncementsByGroupID";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GroupID", groupID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Announcements message = new Announcements()
                        {
                            AnnouncementID = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            GroupID = reader.GetInt32(4),
                            Date = reader.GetDateTime(5),
                            Announcement = reader.GetString(6)
                        };
                        anouncements.Add(message);
                    }
                }
                else
                {
                    throw new ApplicationException("No group announcements.");
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Could not retreive announcements for this group.");
            }
            finally
            {
                conn.Close();
            }
            return anouncements;
        }

        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Retrieve TOP 10 announcements linked to a GroupID.
        /// This is a business requirement and will be primarily used on the group homescreen.
        /// </summary>
        /// <param name="announcement">Announcement object with all object details.</param>
        /// <returns>List of Announcements based on procedure result.</returns>
        public static List<Announcements> RetrieveAnnouncementsByGroupIDTop10(int groupID)
        {
            var anouncements = new List<Announcements>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.spSelectAnnouncementsByGroupIDTop10";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GroupID", groupID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Announcements message = new Announcements()
                        {
                            AnnouncementID = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            GroupID = reader.GetInt32(4),
                            Date = reader.GetDateTime(5),
                            Announcement = reader.GetString(6)
                        };
                        anouncements.Add(message);
                    }
                }
                else
                {
                    anouncements.Add(new Announcements(0, "", "", "", 0, DateTime.Now, "No new announcements!"));
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Could not retreive announcements for this group.");
            }
            finally
            {
                conn.Close();
            }
            return anouncements;
        }

        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Updates an existing announcement with new data.
        /// </summary>
        /// <param name="announcement">Announcement object with all object details.</param>
        /// <returns>Boolean based on procedure result</returns>
        public static bool UpdateAnnouncement(Announcements announcement)
        {
            int result;
            bool succeded;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spUpdateAnnouncements";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("AnnouncementID", announcement.AnnouncementID);
            cmd.Parameters.AddWithValue("UserName", announcement.UserName);
            cmd.Parameters.AddWithValue("FirstName", announcement.FirstName);
            cmd.Parameters.AddWithValue("LastName", announcement.LastName);
            cmd.Parameters.AddWithValue("GroupID", announcement.GroupID);
            cmd.Parameters.AddWithValue("Date", announcement.Date);
            cmd.Parameters.AddWithValue("Announcement", announcement.Announcement);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    succeded = true;
                }
                else
                {
                    succeded = false;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Problem occurred while trying to update the announcement.");
            }
            finally
            {
                conn.Close();
            }
            return succeded;
        }
    }
}
