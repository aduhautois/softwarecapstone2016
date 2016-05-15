using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Manages users for an organization.
    /// Created By: Trent Cullinan 02/24/2016
    /// </summary>
    public class OrgUserManager
    {
        private Organization organization;
        private OrgUserAccessor orgUserAccessor;
        private IEnumerable<GroupMember> orgUsers;

        /// <summary>
        /// Initializes constructor and validates that whomever tries to access is the organization leader.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="organization">Organization to be referenced.</param>
        public OrgUserManager(AccessToken accessToken, Organization organization)
        {
            if (CheckAccessToken(accessToken, organization))
            {
                try
                {
                    orgUserAccessor = new OrgUserAccessor(accessToken, organization);

                    organization.OrganizationGroups = orgUserAccessor.RetrieveOrgGroups(accessToken);
                }
                catch (SqlException)
                {
                    throw new Exception("Error with database, try again later.");
                }
                catch (ArgumentException)
                {
                    throw new Exception("User is not the leader of the organization.");
                }

                this.organization = organization;
            }
            else
            {
                throw new ArgumentException("User is not the leader of the organization.");
            }
        }

        /// <summary>
        /// Retrieve the users that are members of groups within an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="refresh"></param>
        /// <returns>Collection of group members.</returns>
        public IEnumerable<GroupMember> GetOrgUsers(AccessToken accessToken, bool refresh = false)
        {
            if (null == this.orgUsers || refresh)
            {
                if (CheckAccessToken(accessToken, this.organization))
                {
                    try
                    {
                        this.orgUsers = orgUserAccessor.RetrieveOrganizationUsers(accessToken);
                    }
                    catch (SqlException)
                    {
                        throw new Exception("Error with database, try again later.");
                    }
                    catch (ArgumentException)
                    {
                        throw new Exception("User is not the leader of the organization.");
                    }
                }
                else
                {
                    throw new Exception("User is not the leader of the organization.");
                }
            }

            return this.orgUsers;
        }

        /// <summary>
        /// Retrieve groups that belong to the organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <returns>Collection of groups.</returns>
        public IEnumerable<Group> GetOrgGroups(AccessToken accessToken)
        {
            IEnumerable<Group> orgGroups = null;

            if (CheckAccessToken(accessToken, this.organization))
            {
                try
                {
                    orgGroups = orgUserAccessor.RetrieveOrgGroups(accessToken);
                }
                catch (SqlException)
                {
                    throw new Exception("Error with database, try again later.");
                }
                catch (ArgumentException)
                {
                    throw new Exception("User is not the leader of the organization.");
                }
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return orgGroups;
        }

        /// <summary>
        /// Retrieve the groups that the user is the leader of.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Collection of groups.</returns>
        public IEnumerable<Group> GetUserOrgGroups(AccessToken accessToken, GroupMember groupMember)
        {
            IEnumerable<Group> orgUserGroups = null;

            if (CheckAccessToken(accessToken, this.organization))
            {
                try
                {
                    orgUserGroups = orgUserAccessor.RetrieveUserLeads(accessToken, groupMember);
                }
                catch (SqlException)
                {
                    throw new Exception("Error with database, try again later.");
                }
                catch (ArgumentException)
                {
                    throw new Exception("User is not the leader of the organization.");
                }
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return orgUserGroups;
        }

        /// <summary>
        /// Change the status for a user to be able to lead groups.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="groupMember">User to be changed.</param>
        /// <returns>Whether process was successful.</returns>
        public bool EditUserLeader(AccessToken accessToken, GroupMember groupMember)
        {
            bool flag = false;

            if (CheckAccessToken(accessToken, this.organization))
            {
                try
                {
                    groupMember.Leader = !groupMember.Leader;

                    flag = orgUserAccessor.RetrieveUserGroupCount(accessToken, groupMember) 
                        == orgUserAccessor.ChangeUserLeader(accessToken, groupMember);
                }
                catch (SqlException)
                {
                    throw new Exception("Error with database, try again later.");
                }
                catch (Exception)
                {
                    throw new Exception("User is not the leader of the organization.");
                }
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return flag;
        }

        /// <summary>
        /// Add user to be a leader of a group.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="group">Group to be changed.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Whether process was successful.</returns>
        public bool AddGroupLeader(AccessToken accessToken, Group group, GroupMember groupMember)
        {
            bool flag = false;

            if (CheckAccessToken(accessToken, this.organization))
            {
                flag = 1 == orgUserAccessor.AddLeaderToGroup(accessToken, group, groupMember);
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return flag;
        }

        /// <summary>
        /// Remove user as a leader from a group.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="group">Group to be changed.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Whether process was successful.</returns>
        public bool RemoveGroupLeader(AccessToken accessToken, Group group, GroupMember groupMember)
        {
            bool flag = false;

            if (CheckAccessToken(accessToken, this.organization))
            {
                flag = 1 == orgUserAccessor.RemoveLeaderFromGroup(accessToken, group, groupMember);
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return flag;
        }

        /// <summary>
        /// Set user as the primary leader of a group.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="group">Group to be changed.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Whether process was successful.</returns>
        public bool EditPrimaryLeader(AccessToken accessToken, Group group, GroupMember groupMember)
        {
            bool flag = false;

            if (CheckAccessToken(accessToken, this.organization))
            {
                flag = 1 == orgUserAccessor.ChangePrimaryGroupLeader(accessToken, group, groupMember);
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return flag;
        }

        /// <summary>
        /// Retrieve a GroupMember from a User (reference).
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="user">User to be referenced.</param>
        /// <returns>GroupMember object.</returns>
        public GroupMember GetGroupMember(AccessToken accessToken, User user)
        {
            if (!CheckAccessToken(accessToken, this.organization))
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return orgUsers.Single(g => g.User.UserID == user.UserID);
        }

        // Created By: Trent Cullinan 02/24/2016
        private bool CheckAccessToken(AccessToken accessToken, Organization organization)
        {
            return organization.OrganizationLeader.UserID == accessToken.UserID;
        }
    }
}
