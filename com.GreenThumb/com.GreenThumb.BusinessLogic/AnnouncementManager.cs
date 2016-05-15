using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessLogic.Interfaces;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Created By: Luke Frahm 4/22/2016
    /// Announcement manager created to handle logic for announcements
    /// </summary>
    public class AnnouncementManager : com.GreenThumb.BusinessLogic.IAnnouncementManager
    {
        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Take a new announcement created by user and pass it to data access.
        /// </summary>
        /// <param name="announcement">Announcement object with all object details.</param>
        /// <returns>Boolean based on result from data access layer.</returns>
        public bool CreateAnnouncement(int groupID, string Content, string username)
        {
            bool success;
            try
            {
                var user = UserAccessor.RetrieveUserByUsername(username);
                success = AnnouncementAccessor.CreateAnnouncement(groupID, Content, user);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Retrieve a list of all announcements from a group.
        /// </summary>
        /// <param name="groupID">The ID of the group to query for announcements.</param>
        /// <returns>List of announcements based on result from data access layer.</returns>
        public List<Announcements> GetAnnouncementsByGroupID(int groupID)
        {
            var announcements = new List<Announcements>();
            try
            {                
                    announcements = AnnouncementAccessor.RetrieveAnnouncementsByGroupID(groupID);
            }
            catch (Exception)
            {
                throw;
            }

            return announcements;
        }

        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Retrieve a list of most recent 10 announcements from a group.
        /// </summary>
        /// <param name="userID">User ID from logged in user to retrieve announcements at time of log in.</param>
        /// <returns>List of announcements based on result from data access layer.</returns>
        public List<Announcements> GetAnnouncementsByGroupIDTop10(int userID)
        {
            // create group manager and IEnumerable<Group> to hold group
            GroupManager gm = new GroupManager();
            var gl = gm.GetUserGroups(userID);

            // initialize Lists to hold announcements
            var allAnnouncments = new List<Announcements>();
            var announcements = new List<Announcements>();
            try
            {
                // search in each group the user belongs to
                foreach (var g in gl)
                {
                    // search and add 10 most recent announcements for the group
                    announcements = AnnouncementAccessor.RetrieveAnnouncementsByGroupIDTop10(g.GroupID);

                    // add these announcements to a list to be sent back: this holds all messages for all groups
                    foreach (var a in announcements)
                    {
                        allAnnouncments.Add(a);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            // sort announcements by date: most recent at top
            allAnnouncments.Sort((x,y) => x.Date.CompareTo(y.Date));
            allAnnouncments.Reverse();

            announcements.Clear();
            for (int i = 0; i < 10 && i < allAnnouncments.Count; i++)
            {
                announcements.Add(allAnnouncments.ElementAt(i));
            }
            return announcements;
        }

        /// <summary>
        /// Created By: Luke Frahm 4/22/2016
        /// Update an existing announcement as changed by user and pass it to data access.
        /// </summary>
        /// <param name="announcement">Announcement object with all object details.</param>
        /// <returns>Boolean based on result from data access layer.</returns>
        public bool UpdateAnnouncement(Announcements announcement)
        {
            bool success;
            try
            {
                success = AnnouncementAccessor.UpdateAnnouncement(announcement);
            }
            catch (Exception)
            {
                throw;
            }
            return success;
        }
    }
}
