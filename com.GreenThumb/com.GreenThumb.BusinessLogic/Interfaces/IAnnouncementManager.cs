using System;
namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Created By: Luke Frahm 4/22/2016
    /// AnnouncementManager Interface for the Buisness Logic Layer
    /// </summary>
    public interface IAnnouncementManager
    {
        bool CreateAnnouncement(int groupID, string Content, string username);
        System.Collections.Generic.List<com.GreenThumb.BusinessObjects.Announcements> GetAnnouncementsByGroupID(int groupID);
        System.Collections.Generic.List<com.GreenThumb.BusinessObjects.Announcements> GetAnnouncementsByGroupIDTop10(int userID);
        bool UpdateAnnouncement(com.GreenThumb.BusinessObjects.Announcements announcement);
    }
}
