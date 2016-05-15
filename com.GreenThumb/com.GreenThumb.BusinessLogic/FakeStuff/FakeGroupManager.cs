using com.GreenThumb.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Test Class Created by Kristine Johnson 2/28/16
/// Tests to ensure a group list is returned, works with test unit.
/// </summary>

namespace com.GreenThumb.BusinessLogic.FakeStuff
{
    public class FakeGroupManager : IGroupManager
    {

        public List<BusinessObjects.Group> GetGroupList(int OrganizationID)
        {
            var groups = new List<com.GreenThumb.BusinessObjects.Group>();
            if (OrganizationID == 1000)
            {
                groups.Add(new com.GreenThumb.BusinessObjects.Group()
                {
                    OrganizationID = 1000,
                    GroupID = 1000,
                    Name = "Group1",
                    GroupLeaderID = 1000

                });

                groups.Add(new com.GreenThumb.BusinessObjects.Group()
                {
                    OrganizationID = 1000,
                    GroupID = 1001,
                    Name = "Group2",
                    GroupLeaderID = 1001

                });
            }
            

            return groups;
            
        }

        public List<BusinessObjects.Group> Group
        {
            get { throw new NotImplementedException(); }
        }


        public List<BusinessObjects.Group> GetGroupsForUser(int userID)
        {
            List<BusinessObjects.Group> groups = new List<BusinessObjects.Group>();

            if (userID == 1)
            {
                groups.Add(new BusinessObjects.Group()
                {
                    Name = "A Group"
                });
            }

            return groups;
        }


        public bool AddGroup(int userID, string groupName)
        {
            bool groupAdded = false;
            if (userID == 1)
            {
                if (groupName.Length > 2 && groupName.Length < 100)
                {
                    groupAdded = true;
                }
            }
            return groupAdded;
        }
    }
}
