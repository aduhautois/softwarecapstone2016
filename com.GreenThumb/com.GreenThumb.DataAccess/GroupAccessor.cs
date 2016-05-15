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
    /// Created by Kristine Johnson 2/28/16
    /// Takes an organization and gets a connection to the database to pull a list of groups.
    /// </summary>
    public class GroupAccessor
    {
        public static List<Group> RetrieveGroupList(int userID, Active recordType = Active.active)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            ///sent to Chris Sheenan 2/28 to add to database by Kristine Johnson

            string cmdText = @"Gardens.spSelectUserGroups";



            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //int groupID, int organizationID,string groupName,int groupLeaderID, bool active
            cmd.Parameters.AddWithValue("@UserID", userID);

            //// we can also create an output parameter
            //var o = new SqlParameter("Group", SqlDbType.I);
            //o.Direction = ParameterDirection.ReturnValue;
            //cmd.Parameters.Add(o);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    // now we just need a loop to process the reader
                    while (reader.Read())
                    {
                        Group currentGroup = new Group()
                        {  //int groupID, int organizationID,string groupName,int groupLeaderID, bool active
                            GroupID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            GroupLeaderID = reader.GetInt32(2)

                        };
                        groupList.Add(currentGroup); ///returns a group list
                    }
                }
                else
                {
                    var ax = new ApplicationException("No group was found");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return groupList;
        }


        public static int UpdateGroupMemberRequest(GroupRequest request)
        {
            int count = 0;

            var conn = DBConnection.GetDBConnection();
            string cmdText = "Admin.spAcceptRequest";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", request.GroupID);
            cmd.Parameters.AddWithValue("@UserID", request.UserID);
            cmd.Parameters.AddWithValue("@ApprovedID", request.ApprovedBy);
            cmd.Parameters.AddWithValue("@ApprovedDate", request.ApprovedDate);
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }


        /// <summary>
        /// retrieves a list of groups the user is not in
        /// Created by: Nicholas King 04/03/2016
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<Group> RetrieveJoinableGroups(int UserID)
        {
            List<Group> joinableGroups = new List<Group>();
            var conn = DBConnection.GetDBConnection();
            string cmdText = "Gardens.spSelectJoinableGroups";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Active", 1);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        joinableGroups.Add(new Group()
                        {
                            GroupID
                                = reader.GetInt32(0),
                            Name
                                = reader.GetString(1),
                            Active
                                = reader.GetBoolean(2),
                            GroupLeader = new GroupMember()
                            {
                                User = new User()
                                {
                                    UserID
                                        = reader.GetInt32(3),
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

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return joinableGroups;
        }

        /// <summary>
        /// added by Nicholas King
        /// creates a group leader request
        /// </summary>
        public static int CreateGroupLeaderRequest(int userID, int groupID, DateTime time)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Gardens.spInsertGroupLeaderRequest";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@groupID", groupID);
            cmd.Parameters.AddWithValue("@RequestDate", time);
            cmd.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);
            cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
            cmd.Parameters.AddWithValue("@RequestActive", 1);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }


        /// <summary>
        /// added by Nicholas King
        /// gets a list of all groups the user is in
        /// </summary>
        public static List<Group> RetrieveUsersGroups(int userID, Active recordType = Active.active)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            string cmdText = @"Gardens.spSelectUserGroups";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group currentGroup = new Group()
                        {
                            GroupID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        groupList.Add(currentGroup);
                    }
                }
                else
                {
                    var msg = new ApplicationException("No groups was found");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return groupList;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 03/23/16
        /// Inserts a new group into the database
        /// </summary>
        /// <param name="userID">ID of user creating the group</param>
        /// <param name="groupName">Name of the new group to be added</param>
        /// <returns>True if data was added, False otherwise</returns>
        public static bool CreateGroup(int userID, string groupName)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spInsertGroups";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupName", groupName);
            cmd.Parameters.AddWithValue("@GroupLeaderID", userID);
            cmd.Parameters.AddWithValue("@OrganizationID", 0);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount == 1;
        }

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created 03/31/16
        /// Query database to determine of the supplied user is a leader in the group.
        /// </summary>
        /// <param name="userID">ID of user to check status</param>
        /// <param name="groupID">ID of the group to query for user status</param>
        /// <returns>True if data was added, False otherwise</returns>
        public static bool RetrieveGroupLeaderStatus(int userID, int groupID)
        {
            bool isLeader = false;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spCheckLeaderStatus";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupId", groupID);
            cmd.Parameters.AddWithValue("@UserId", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                isLeader = reader.HasRows;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isLeader;
        }

        /// <summary>
        /// Ryan Taylor and Luke Frahm
        /// Created 03/31/16
        /// Update database to reflect new group name.
        /// </summary>
        /// <param name="groupID">ID of the group to alter</param>
        /// <param name="newGroupName">New name to be declared</param>
        /// <param name="oldGroupName">Old name to be replaced</param>
        /// <returns>True if data was updated, False otherwise</returns>
        public static bool UpdateGroupName(int groupID, string newGroupName, string oldGroupName)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spUpdateGroupName";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@NewGroupName", newGroupName);
            cmd.Parameters.AddWithValue("@OldGroupName", oldGroupName);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount == 1;
        }

        /// <summary>
        /// Poonam Dubey
        /// 04/06/2016
        /// Function to call DB and insert groupmember request
        /// 
        /// Altered by Nicholas King
        /// </summary>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public static int CreateGroupMember(GroupRequest reqObj)
        {
            string query = @"Admin.spInsertGroupRequest";
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", reqObj.GroupID);
            cmd.Parameters.AddWithValue("@UserID", reqObj.UserID);
            //This Para is not used by the stored procedure
            //cmd.Parameters.AddWithValue("@RequestStatus", reqObj.RequestStatus);
            cmd.Parameters.AddWithValue("@RequestDate", reqObj.RequestDate);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount;

        }

        /// <summary>
        /// Gets a list of group requests for the selected group
        /// 
        /// created by Nicholas King
        /// </summary>
        /// <param name="groupid">Id of the group</param>
        /// <returns></returns>
        public static List<GroupRequest> RetrieveGroupRequestsByGroup(int groupid)
        {
            List<GroupRequest> requests = new List<GroupRequest>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spGetRequestsForGroup", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID", groupid);
            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GroupRequest request = new GroupRequest();
                        request.UserID = reader.GetInt32(0);
                        request.RequestDate = reader.GetDateTime(1);
                        request.GroupID = groupid;
                        requests.Add(request);
                    }
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

            return requests;
        }

        /// <summary>
        /// Luke Frahm
        /// Created 03/31/16
        /// Update database to set this group to inactive.
        /// </summary>
        /// <param name="groupID">ID of the group to deactivate</param>
        /// <returns>True if deactivated, False otherwise</returns>
        public static bool UpdateDeactivateGroupByID(int groupID)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();
            string query = @"Gardens.spDeactivateGroupByID";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            cmd.Parameters.AddWithValue("@Active", 0);

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount == 1;
        }

        /// <summary>
        /// "But Trent... There are already two of these!" 
        /// Yes. Yes there is, and now there is a third.
        /// 
        /// Created By: Trent Cullinan 02/31/2016
        /// </summary>
        /// <param name="userId">User Id to get groups for.</param>
        /// <returns>Collection of groups that the user belongs to.</returns>
        public static IEnumerable<Group> RetrieveUserGroups(int userId)
        {
            IList<Group> groups = null;

            groups = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectUserGroups", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    groups = new List<Group>();

                    while (reader.Read())
                    {
                        groups.Add(new Group()
                        {
                            GroupID
                                = reader.GetInt32(0),
                            Name
                                = reader.GetString(1),
                            Active
                                = reader.GetBoolean(2),
                            GroupLeader = new GroupMember()
                            {
                                User = new User()
                                {
                                    UserID
                                        = reader.GetInt32(3),
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
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return groups;
        }

        /// <summary>
        /// Ryan Taylor
        /// Created 03/31/16
        /// </summary>
        /// <remarks>
        /// Trevor Glisch fixed values returned with stored procedure
        /// </remarks>
        /// <param name="groupID"></param>
        /// <returns>Members associtated with groupID</returns>
        public static List<GroupMember> RetrieveMemberList(int groupID)
        {
            var memberList = new List<GroupMember>();
            var conn = DBConnection.GetDBConnection();
            string cmdText = @"Gardens.spSelectGroupMembers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupID", groupID);
            // get all the users already accepted
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GroupMember currentMember = new GroupMember()
                        {
                            User = new User
                            {
                                UserID = reader.GetInt32(0),
                                UserName = reader.GetString(2),
                                FirstName = reader.GetString(4),
                                LastName = reader.GetString(5)
                            }
                        };
                        memberList.Add(currentMember);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return memberList;



        }

        /// <summary>
        /// Modifies the group member to either be 
        /// active or inactive for a particular group.
        /// 
        /// Created By: Trent Cullinan 02/31/2016
        /// </summary>
        /// <param name="userId">User from group to be modified.</param>
        /// <param name="groupId">Group the user belongs to.</param>
        /// <returns>Rows affected by change.</returns>
        public static int UpdateInactivateGroupMember(int userId, int groupId)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spUpdateGroupMemberInactive", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID",
                userId);
            cmd.Parameters.AddWithValue("@GroupID",
                groupId);

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


        /// <summary>
        /// Poonam Dubey
        /// 04/06/2016
        /// Function to fetch groups to view and request to join
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public static List<Group> RetrieveGroupsToView(int userID)
        {
            var groupList = new List<Group>();

            var conn = DBConnection.GetDBConnection();

            string cmdText = @"Gardens.spSelectGroupsToView";



            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group currentGroup = new Group()
                        { 
                            GroupID = reader.GetInt32(0),
                            Name = reader.GetString(1)

                        };
                        groupList.Add(currentGroup); ///returns a group list
                    }
                }
                else
                {
                    var ax = new ApplicationException("No group was found");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return groupList;
        }

        /// <summary>
        /// TODO: Talk to Ryan / Chris: Currently stored procedure is
        /// not in the database, I wrote my own and I see that
        /// his method has references to features so I don't want
        /// to step on that.
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="groupId">Group identifier</param>
        /// <returns>Collection of group members</returns>
        public static IEnumerable<GroupMember> RetrieveGroupMembers(int groupId)
        {
            var groupMembers = new List<GroupMember>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectGroupMembers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID", 
                groupId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    groupMembers.Add(new GroupMember()
                    {
                        User = new User
                        {
                            UserID 
                                = reader.GetInt32(0),
                            UserName
                                = reader.GetString(2),
                            EmailAddress 
                                = reader.GetString(3),
                            FirstName 
                                = reader.GetString(4),
                            LastName 
                                = reader.GetString(5)
                        },
                        CreatedDate
                            = reader.GetDateTime(1)
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return groupMembers;
        }

        /// <summary>
        /// Retrieve a group by identitier.
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="groupId">Group identifier</param>
        /// <returns>Group object</returns>
        public static Group RetrieveGroupById(int groupId)
        {
            Group group = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectGroupByID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID",
                groupId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    group = new Group()
                    {
                        Name
                            = reader.GetString(0),
                        GroupLeader = new GroupMember()
                        {
                            User = new User()
                            {
                                UserID
                                    = reader.GetInt32(1),
                                UserName
                                    = reader.GetString(2),
                                EmailAddress
                                    = reader.GetString(3),
                                FirstName
                                    = reader.GetString(4),
                                LastName
                                    = reader.GetString(5)
                            }
                        }

                    };
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

            return group;
        }

        /// <summary>
        /// Retrieve a groupId by groupName.
        /// 
        /// Created by: Chris Schwebach 04/22/2016
        /// </summary>
        /// <param name="groupId">Group identifier</param>
        /// <returns>Group object</returns>
        public static Group RetrieveGroupIdByGroupName(string groupName)
        {
            Group group = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Gardens.spSelectGroupIdByGroupName", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupName", groupName);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    group = new Group()
                    {
                        GroupID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        GroupLeaderID = reader.GetInt32(2)
                    };
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

            return group;
        }
    }
}
