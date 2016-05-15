using System;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    public interface IGardens
    {
        string Announcement { get; set; }
        int AnnouncementID { get; set; }
        DateTime Date { get; set; }
        int OrganizationID { get; set; }
        int UserID { get; set; }
    }
}
