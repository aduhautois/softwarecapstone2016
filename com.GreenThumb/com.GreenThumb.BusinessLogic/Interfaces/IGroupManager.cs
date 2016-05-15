using System;
using System.Collections.Generic;
///author Kristine Johnson
///
namespace com.GreenThumb.BusinessLogic.Interfaces
{
   public interface IGroupManager
    {
        System.Collections.Generic.List<com.GreenThumb.BusinessObjects.Group> GetGroupList(int OrganizationID);
        List<com.GreenThumb.BusinessObjects.Group> GetGroupsForUser(int userID);
        bool AddGroup(int userID, string groupName);
    }
}
