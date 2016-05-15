using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Access things related to an Organization and it's users.
    /// Created By: Trent Cullinan 02/24/2016
    /// </summary>
    public class OrgUserAccessor
    {
        private Organization organization;

        /// <summary>
        /// Initializes constructor and validates that whomever tries to access is the organization leader.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="organization">Organization to be referenced.</param>
        public OrgUserAccessor(AccessToken accessToken, Organization organization)
        {
            if (CheckAccessToken(accessToken, organization))
            {
                this.organization = organization;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Retrieve users that belong to groups of an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <returns>Collection of group members.</returns>
        public IEnumerable<GroupMember> RetrieveOrganizationUsers(AccessToken accessToken)
        {
            IList<GroupMember> users = null;

            if (CheckAccessToken(accessToken, this.organization))
            {
                users = new List<GroupMember>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Admin.spSelectUsersByOrganization", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrganizationID",
                    this.organization.OrganizationID);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new GroupMember()
                        {
                            User = new User() 
                            {
                                UserID 
                                    = reader.GetInt32(0),
                                UserName 
                                    = reader.GetString(1),
                                FirstName 
                                    = reader.GetString(2),
                                LastName 
                                    = reader.GetString(3),
                                EmailAddress
                                    = reader.GetString(4)
                            },
                            Leader 
                                = reader.GetBoolean(5),
                            CreatedDate
                                = reader.GetDateTime(6)
                        });
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }
            
            return users;
        }

        /// <summary>
        /// Retrieve groups that belong to an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <returns>Collection of groups.</returns>
        public IEnumerable<Group> RetrieveOrgGroups(AccessToken accessToken)
        {
            IList<Group> groups = null;

            if (CheckAccessToken(accessToken, this.organization))
            {
                groups = new List<Group>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Admin.spSelectOrgGroups", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrganizationID",
                    this.organization.OrganizationID);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        groups.Add(new Group() 
                        {
                            GroupID 
                                = reader.GetInt32(0),
                            Name
                                = reader.GetString(1),
                            GroupLeaderID
                                = reader.GetInt32(2),
                            Active
                                = reader.GetBoolean(3),
                            GroupLeader = new GroupMember() 
                            {
                                User = new User()
                                {
                                    UserName 
                                        = reader.GetString(4),
                                    FirstName 
                                        = reader.GetString(5),
                                    LastName
                                        = reader.GetString(6),
                                    EmailAddress
                                        = reader.GetString(7)
                                }
                            }
                        });
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return groups;
        }

        /// <summary>
        /// Change the leader status ability for a user so that they can either add groups for an organization and lead them.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="groupMember">User to be changed.</param>
        /// <returns>Number of records processed.</returns>
        public int ChangeUserLeader(AccessToken accessToken, GroupMember groupMember)
        {
            int rowsAffected = 0;

            if (CheckAccessToken(accessToken, this.organization))
            {
                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Gardens.spUpdateUserOrgGroupLeader", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrganizationID",
                    this.organization.OrganizationID);
                cmd.Parameters.AddWithValue("@UserID",
                    groupMember.User.UserID);
                cmd.Parameters.AddWithValue("@Leader",
                    groupMember.Leader);

                try
                {
                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Retrieve the total number of groups that a user belongs to in an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Total count.</returns>
        public int RetrieveUserGroupCount(AccessToken accessToken, GroupMember groupMember)
        {
            int total = 0;

            if (CheckAccessToken(accessToken, this.organization))
            {
                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Gardens.spSelectUserGroupCount", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID",
                    groupMember.User.UserID);
                cmd.Parameters.AddWithValue("@OrganizationID",
                    this.organization.OrganizationID);

                try
                {
                    conn.Open();

                    total = (int)cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return total;
        }

        /// <summary>
        /// Set the user as the primary leader of a group.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="group">Group to be changed.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Number of records processed.</returns>
        public int ChangePrimaryGroupLeader(AccessToken accessToken, Group group, GroupMember groupMember)
        {
            int rowsAffected = 0;

            if (CheckAccessToken(accessToken, this.organization))
            {
                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Gardens.spUpdatePrimaryGroupLeader", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@GroupID", 
                    group.GroupID);
                cmd.Parameters.AddWithValue("@UserID",
                    groupMember.User.UserID);

                try
                {
                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Add user as a leader of a group.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="group">Group to be changed.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Number of records processed.</returns>
        public int AddLeaderToGroup(AccessToken accessToken, Group group, GroupMember groupMember)
        {
            int rowsAffected = 0;

            if (CheckAccessToken(accessToken, this.organization))
            {
                try
                {
                    rowsAffected = UpdateGroupLeader(group, groupMember);
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Remove user as a leader of a group.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="group">Group to be changed.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Number of records processed.</returns>
        public int RemoveLeaderFromGroup(AccessToken accessToken, Group group, GroupMember groupMember)
        {
            int rowsAffected = 0;

            if (CheckAccessToken(accessToken, this.organization))
            {
                try
                {
                    rowsAffected = UpdateGroupLeader(group, groupMember, active: false);
                }
                catch (SqlException)
                {
                    throw;
                }
                
            }
            else
            {
                throw new ArgumentException();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Retrieve current requests for an organization. (GroupLeader)
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <returns>Collection of group leader requests.</returns>
        public IEnumerable<GroupLeaderRequest> FetchGroupLeaderRequests(AccessToken accessToken)
        {
            IList<GroupLeaderRequest> orgRequests = null;

            if (CheckAccessToken(accessToken, this.organization))
            {
                orgRequests = new List<GroupLeaderRequest>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Gardens.spSelectGroupLeaderRequests", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrganizationID", 
                    this.organization.OrganizationID);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orgRequests.Add(new GroupLeaderRequest() 
                        {
                            RequestID 
                                = reader.GetInt32(0),
                            User = new User()
                            {
                                UserID 
                                    = reader.GetInt32(1),
                                UserName 
                                    = reader.GetString(4),
                                FirstName 
                                    = reader.GetString(5),
                                LastName 
                                    = reader.GetString(6),
                                EmailAddress 
                                    = reader.GetString(7)
                            },
                            Group = new Group()
                            {
                                GroupID 
                                    = reader.GetInt32(2),
                                Name 
                                    = reader.GetString(3)
                            },
                            RequestDate 
                                = reader.GetDateTime(8)
                        });
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return orgRequests;
        }

        /// <summary>
        /// Confirm that a request has been processed.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="request">Request to be confirmed processed.</param>
        /// <returns>Number of records processed.</returns>
        public int ProcessRequest(AccessToken accessToken, GroupLeaderRequest request)
        {
            int rowsAffected = 0;

            if (CheckAccessToken(accessToken, this.organization))
            {
                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Gardens.spUpdateOrgGroupLeaderRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RequestID", 
                    request.RequestID);
                cmd.Parameters.AddWithValue("@UserID", 
                    accessToken.UserID);

                try
                {
                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Retrieve the groups that a user leads. (Not including primary_leading)
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="groupMember">User to be referenced.</param>
        /// <returns>Collection of groups.</returns>
        public IEnumerable<Group> RetrieveUserLeads(AccessToken accessToken, GroupMember groupMember)
        {
            IList<Group> userLeads = null;

            if (CheckAccessToken(accessToken, this.organization))
            {
                userLeads = new List<Group>();

                var conn = DBConnection.GetDBConnection();

                var cmd = new SqlCommand("Gardens.spSelectOrgUserLeads", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", 
                    groupMember.User.UserID);
                cmd.Parameters.AddWithValue("@OrganizationID",
                    this.organization.OrganizationID);

                try
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userLeads.Add(new Group() 
                        {
                            GroupID 
                                = reader.GetInt32(0),
                            Name 
                                = reader.GetString(1),
                        });
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return userLeads;
        }

        // Created By: Trent Cullinan 02/24/2016
        private bool CheckAccessToken(AccessToken accessToken, Organization organization)
        {
            return organization.OrganizationLeader.UserID == accessToken.UserID;
        }

        // Created By: Trent Cullinan 02/24/2016
        private int UpdateGroupLeader(Group group, GroupMember groupMember, bool active = true)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spUpdateGroupLeader", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID",
                group.GroupID);
            cmd.Parameters.AddWithValue("@UserID",
                groupMember.User.UserID);
            cmd.Parameters.AddWithValue("@Active",
                active);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}
