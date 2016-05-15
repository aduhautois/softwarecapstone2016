using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;

/// <summary>
/// Created by Kristine Johnson 2/28/16
/// Selects a list of groups within an organziation
/// </summary>

namespace com.GreenThumb.BusinessLogic
{
    public class GroupManager : com.GreenThumb.BusinessLogic.Interfaces.IGroupManager
    {

        public List<Group> GetGroupList(int OrganizationID)
        {

            try
            {
                return GroupAccessor.RetrieveGroupList(OrganizationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Created by: Nicholas King 04/03/2016
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Group> GetGroupsToJoin(int userID)
        {
            List<Group> joinable;

            try
            {
                joinable = GroupAccessor.RetrieveJoinableGroups(userID);
            }
            catch (Exception){
                joinable = new List<Group>();
            } //returns an empty list

            return joinable;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/21/16
        /// Get all groups the associated user is affiliated with
        /// </summary>
        /// <param name="userID">ID of user used for search, passed from Access Token</param>
        /// <returns>List of Groups the user belongs to</returns>
        public List<Group> GetGroupsForUser(int userID)
        {
            List<Group> groups = null;

            try
            {
                groups = GroupAccessor.RetrieveUsersGroups(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return groups;
        }


        /// <summary>
        /// Ryan Taylor
        /// Created: 03/21/16
        /// Add a new group that this user will be the leader of
        /// </summary>
        /// <param name="userID">ID of user creating the group, passed from Access Token</param>
        /// <param name="groupName">The name of the new group</param>
        /// <returns></returns>
        public bool AddGroup(int userID, string groupName)
        {
            try
            {
                return GroupAccessor.CreateGroup(userID, groupName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created: 03/31/16
        /// Check to see if the user is a leader in the group
        /// </summary>
        /// <param name="userID">ID of user to query</param>
        /// <param name="groupID">ID of the group to check for leader</param>
        /// <returns>
        /// Boolean of leader status in selected group
        /// </returns>
        public bool GetLeaderStatus(int userID, int groupID)
        {
            try
            {
                return GroupAccessor.RetrieveGroupLeaderStatus(userID, groupID);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created: 03/31/16
        /// Check to see if the user is a leader in the group
        /// </summary>
        /// <param name="groupID">ID of the group to check for leader</param>
        /// <param name="newGroupName">New name to be declared</param>
        /// <param name="oldGroupName">Old name to be replaced</param>
        /// <returns>
        /// Boolean of result: success or failure
        /// </returns>
        public bool EditGroupName(int groupID, string newGroupName, string oldGroupName)
        {
            try
            {
                return GroupAccessor.UpdateGroupName(groupID, newGroupName, oldGroupName);
            }
            catch (Exception)
            {
                throw new ApplicationException("Group name could not be changed.");
            }
        }
        /// <summary>
        /// Poonam Dubey
        /// 04/06/2016
        /// Function to call accessor and insert group member request
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public int AddGroupMember(GroupRequest reqObj)
        {
            try
            {
                return GroupAccessor.CreateGroupMember(reqObj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Luke Frahm
        /// Created: 03/31/16
        /// Set the group to Active = false
        /// </summary>
        /// <param name="group">Group object containing name of group to be deactivated</param>
        /// <returns>
        /// Boolean of result for deactivating group
        /// </returns>
        public bool EditDeactivateGroup(Group group)
        {
            try
            {
                return GroupAccessor.UpdateDeactivateGroupByID(group.GroupID);
            }
            catch (Exception)
            {

                throw new ApplicationException("Group could not be closed.");
            }
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 03/31/16
        /// Get The Members of a group based on the groupID
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <returns>List of GroupMembers</returns>
        public List<GroupMember> GetGroupMembers(int groupID)
        {
            List<GroupMember> memberList;

            try
            {
                memberList = GroupAccessor.RetrieveMemberList(groupID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Unable to retrieve members for this group.");
            }

            return memberList;
        }
        /// Retrieve a collection of groups with a group leader that the user id is a member of.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="userId">User Id of user to retrieve groups for.</param>
        /// <returns>Collection of groups that a user belongs to.</returns>
        public IEnumerable<Group> GetUserGroups(int userId)
        {
            IEnumerable<Group> groups = new List<Group>(); // Empty collection to return

            try
            {
                groups = GroupAccessor.RetrieveUserGroups(userId);
            }
            catch (Exception) { } // groups will be an empty collection

            return groups;
        }

        /// <summary>
        /// Marks the user as inactive for the group.
        /// 
        /// Created by: Trent Cullinan 03/31/2016
        /// </summary>
        /// <param name="userId">User Id of user leaving.</param>
        /// <param name="groupId">Group Id of which group.</param>
        /// <returns>Whether the group removal was successful.</returns>
        public bool EditLeaveGroup(int userId, int groupId)
        {
            bool flag = false;

            try
            {
                // 1 row should be affected
                flag =
                    1 == GroupAccessor.UpdateInactivateGroupMember(userId, groupId);
            }
            catch (Exception) { } // flag set to false

            return flag;
        }

        /// <summary>
        /// Poonam Dubey
        /// 04/06/2016
        /// Function to fetch groups to view
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Group> GetGroupsToView(int userID)
        {
            List<Group> groups = null;

            try
            {
                groups = GroupAccessor.RetrieveGroupsToView(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return groups;
        }

        /// <summary>
        /// Retrieves a group by it's identifier.
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="groupId">Identifier to be used.</param>
        /// <returns>Group that was requested by Id</returns>
        public Group GetGroup(int groupId)
        {
            Group group = null;

            try
            {
                group = GroupAccessor.RetrieveGroupById(groupId);

                if (null != group)
                {
                    group.UserList = GroupAccessor.RetrieveGroupMembers(groupId);

                    if (null != group.UserList)
                    {
                        group.GroupID = groupId;
                    }
                }
            }
            catch (Exception) { } // group will be null.

            return group;
        }

        /// <summary>
        /// Gets a list of group requests baised on group id
        /// Created by Nicholas King
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public List<GroupRequest> GetGroupRequests(int groupid)
        {
            List<GroupRequest> requests = new List<GroupRequest>();
            if (groupid != 0)
            {
                try
                {
                    requests = GroupAccessor.RetrieveGroupRequestsByGroup(groupid);
                }
                catch (Exception)
                {
                    //causes an empty but not null list to return
                }
            }
            return requests;
        }


        public bool UpateAcceptGroupRequest(GroupRequest request)
        {
            bool result = false;

            try
            {
                result = (GroupAccessor.UpdateGroupMemberRequest(request) == 1);
            }
            catch (Exception)
            {
                result = false;
            }
            
            
            return result;
        }

        /// <summary>
        /// Retrieves group by groupName.
        /// 
        /// Created by: Chris Schwebach 04/22/2016
        /// </summary>
        /// <param name="groupId">Identifier to be used.</param>
        /// <returns>Group that was requested by Id</returns>
        public Group RetrieveGroupByName(string groupName)
        {
            Group group = null;

            try
            {
                group = GroupAccessor.RetrieveGroupIdByGroupName(groupName);
        
            }
            catch (Exception) { } // group will be null.

            return group;
        }

        /// <summary>
        /// Really bad method, just using duct tape.
        /// 
        /// Created by: Trent Cullinan 05/05/2016
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsGroupMember(string groupName, int userId)
        {
            bool flag = false;

            List<Group> groups = null;
            try
            {
                groups = GroupAccessor.RetrieveUsersGroups(userId);
            }
            catch (Exception) { }

            if (null != groups)
            {
                flag = 0 < groups.Where(g => g.Name == groupName).Count();
            }

            return flag;
        }

    }
}
