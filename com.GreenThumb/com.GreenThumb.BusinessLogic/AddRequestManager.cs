using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Comments added byTRex
    /// This class processes the request to add a group leader
    /// and retrieves groups
    /// </summary>
    public class AddRequestManager
    {
        private AccessToken myAccessToken = null;
        private List<Group> myUserGroups = null;
        public AddRequestManager(AccessToken myaccesstoken)
        {
            this.myAccessToken = myaccesstoken;
        }
        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method retrieves groups.
        /// </summary>
        /// <returns></returns>
        public List<Group> GetUserGroups()
        {
            myUserGroups = DataAccess.GroupAccessor.RetrieveUsersGroups(myAccessToken.UserID, Active.active);

            return myUserGroups;
        }
        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method adds a group leader.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public string AddGroupLeaderRequest(string groupName)
        {
            string returned = "";
            int count = 0;

            if(groupName != "")
            {
                //finds the group in the list that matches the selected group
                Group groupSelected = myUserGroups.Where(x => x.Name.Equals(groupName)).First();
                
                try
                {
                    count = DataAccess.GroupAccessor.CreateGroupLeaderRequest(myAccessToken.UserID, groupSelected.GroupID, DateTime.Now);
                }
                catch (Exception)
                {

                    returned = "Sorry your request didn't go through, please try again";
                }

            }
            if (count == 1)
            {
                returned = "Your request was submited successfully";
            }
            else
            {
                returned = "Sorry your request didn't go through, please try again";
            }

            return returned;
        } 
    }
}
