using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Created By: Luke Frahm
    /// Old object for Announcements was incorrectly coded and was deleted
    /// This object now contains correct fields reflecting database and updates
    /// done on the database to reflect business needs
    /// </summary>
    public class Announcements
    {
        public int AnnouncementID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupID { get; set; }
        public DateTime Date { get; set; }
        public string Announcement { get; set; }

        public Announcements() { }

        public Announcements(string userName,
                             string firstName,
                             string lastName,
                             int groupID,
                             DateTime date,
                             string announcement)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            GroupID = groupID;
            Date = date;
            Announcement = announcement;
        }

        public Announcements(int announcementID,
                             string userName,
                             string firstName,
                             string lastName,
                             int groupID,
                             DateTime date,
                             string announcement)
        {
            AnnouncementID = announcementID;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            GroupID = groupID;
            Date = date;
            Announcement = announcement;
        }
    }
}
