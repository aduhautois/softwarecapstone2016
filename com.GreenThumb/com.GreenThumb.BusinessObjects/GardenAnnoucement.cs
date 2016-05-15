using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Dat Tran
namespace com.GreenThumb.BusinessObjects
{
    public class Gardens
    {

        public int AnnouncementID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int OrganizationID { get; set; }
        public string Announcement { get; set; }



        public Gardens(int announcementID,
                    int userID,
                    DateTime date,
                    int organizationID,
                    string announcement)
        {
            AnnouncementID = announcementID;
            UserID = userID;
            date = Date;
            OrganizationID = organizationID;
            Announcement = announcement;
        }

    }
}
