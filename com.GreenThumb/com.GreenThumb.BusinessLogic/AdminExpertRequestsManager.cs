using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System.Data.SqlClient;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Allows an administrator to process ExpertRequests and set Users as experts.
    /// Users can be promoted to expert and experts can be demoted.
    /// 
    /// Created By: Trent Cullinan 03/15/2016
    /// </summary>
    public class AdminExpertRequestsManager
    {
        private const string ADMIN = "Admin";

        private AccessToken accessToken
            = null;
        private AdminExpertRequestsAccessor adminExpertRequestsAccessor
            = null;
        private List<ExpertRequest> _requests
            = null;
        private IList<User> _users
            = null;
        private IList<User> _experts
            = null;

        // Created By: Trent Cullinan 03/15/2016
        private List<ExpertRequest> requests
        {
            get
            {
                try
                {
                    this._requests = this._requests ??
                        adminExpertRequestsAccessor.RetrieveExpertRequests(accessToken).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return this._requests;
            }

            set { this._requests = value; }
        }

        // Created By: Trent Cullinan 03/15/2016
        private IList<User> users
        {
            get
            {
                try
                {
                    this._users = this._users ??
                        adminExpertRequestsAccessor.RetrieveAllUsers(accessToken).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return this._users;
            }

            set { this._users = value; }
        }

        // Created By: Trent Cullinan 03/15/2016
        private IList<User> experts
        {
            get
            {
                try
                {
                    this._experts = this._experts ??
                        adminExpertRequestsAccessor.RetrieveAllExperts(accessToken).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return this._experts;
            }

            set { this._experts = value; }
        }

        /// <summary>
        /// Only constructor to verify that client instaniating this object is an administrator.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        public AdminExpertRequestsManager(AccessToken accessToken)
        {
            if (accessToken != null)
            {
                try
                {
                    adminExpertRequestsAccessor
                        = new AdminExpertRequestsAccessor(accessToken);
                }
                catch (Exception) { throw; }

                this.accessToken = accessToken;
            }
            else
            {
                throw new Exception("No access token.");
            }
        }

        /// <summary>
        /// Retrieve ExpertRequests to be processed by an administrator.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="refresh">Retrieve fresh data from database.</param>
        /// <returns>Collection of ExpertRequests that need reviewed and processed.</returns>
        public List<ExpertRequest> GetExpertRequests(AccessToken accessToken)
        {
            //if (GetAdminRoleStatus(accessToken))
            //{
                
                    try
                    {
                        this.requests
                            = adminExpertRequestsAccessor.RetrieveExpertRequests(accessToken).ToList();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
               
            //}
            //else
            //{
            //    throw new Exception("User must be an admin to use this feature.");
            //}

            return this.requests;
        }

        /// <summary>
        /// Retrieve all Users who are not an active expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="refresh">Retrieve fresh data from database.</param>
        /// <returns>Collection of Users that are not experts.</returns>
        public IEnumerable<User> GetAllUsers(AccessToken accessToken, bool refresh = false)
        {
            if (GetAdminRoleStatus(accessToken))
            {
                if (refresh)
                {
                    try
                    {
                        this.users
                            = adminExpertRequestsAccessor.RetrieveAllUsers(accessToken).ToList();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            else
            {
                throw new Exception("User must be an admin to use this feature.");
            }

            return this.users;
        }

        /// <summary>
        /// Retrieve users who are an active expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="refresh">Retrieve fresh data from database.</param>
        /// <returns>Collection of Users that are experts.</returns>
        public IEnumerable<User> GetAllExperts(AccessToken accessToken, bool refresh = false)
        {
            if (GetAdminRoleStatus(accessToken))
            {
                if (refresh)
                {
                    try
                    {
                        this.experts
                            = adminExpertRequestsAccessor.RetrieveAllExperts(accessToken).ToList();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            else
            {
                throw new Exception("User must be an admin to use this feature.");
            }

            return this.experts;
        }

        /// <summary>
        /// Search non-expert Users with the value provided.
        /// Searches UserNames first, then FirstNames, and then LastNames.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="query">Value to search with.</param>
        /// <returns>Collection of users that meet the search query.</returns>
        public IEnumerable<User> GetUsers(string query)
        {
            return GetUserCollection(this.users, query);
        }

        /// <summary>
        /// Searches expert Users with the value provided.
        /// Searches UserNames first, then FirstNames, and then LastNames.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="query">Value to search with.</param>
        /// <returns>Collection of users that meet the search query.</returns>
        public IEnumerable<User> SearchExperts(string query)
        {
            return GetUserCollection(this.experts, query);
        }

        /// <summary>
        /// Approve the expert request and set user contained in request as an expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="request">ExpertRequest to be declined.</param>
        /// <returns>Whether the action was successful.</returns>
        public bool AddRequestApproved(AccessToken accessToken, ExpertRequest request)
        {
            bool flag = false;

            
                try
                {
                    flag = 2 == adminExpertRequestsAccessor.UpdateExpertRequestApprove(request); // 2 records should be affected.

                    if (flag) { RemoveRequest(request); }
                }
                catch (SqlException ex)
                {
                    //throw new Exception("Error with database, try again later.");
                    throw new Exception(ex.Message);
            }
            

            return flag;
        }

        /// <summary>
        /// Decline the expert request and do nothing with the user contained in the request.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="request">ExpertRequest to be declined.</param>
        /// <returns>Whether the action was successful.</returns>
        public bool AddRequestDeclined(AccessToken accessToken, ExpertRequest request)
        {
            bool flag = false;

            if (GetAdminRoleStatus(accessToken))
            {
                try
                {
                    flag = 1 == adminExpertRequestsAccessor.UpdateExpertRequestDecline(request); // 1 record should be affected.

                    if (flag) { RemoveRequest(request); }
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return flag;
        }

        /// <summary>
        /// Promote a User to an expert.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="user">User that is not an expert to be promoted.</param>
        /// <returns>Whether the action was successful.</returns>
        public bool EditUserPromoted(AccessToken accessToken, User user)
        {
            bool flag = false;

            if (GetAdminRoleStatus(accessToken))
            {
                if (0 == GetUserExpertCount(user))
                {
                    try
                    {
                        flag = 1 == adminExpertRequestsAccessor.UpdateUserPromote(user); // 1 record should be affected.

                        if (flag)
                        {
                            var refUser = this.users.Single(u => u.UserID == user.UserID); // Get reference from the collection itself.

                            this.experts.Add(refUser);

                            this.users.Remove(refUser);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            return flag;
        }

        /// <summary>
        /// Demote a User from expert status.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        /// <param name="accessToken">To confirm access as administrator.</param>
        /// <param name="user">User that is an expert to be demoted.</param>
        /// <returns>Whether the action was successful.</returns>
        public bool EditExpertDemoted(AccessToken accessToken, User user)
        {
            bool flag = false;

            if (GetAdminRoleStatus(accessToken))
            {
                if (1 == GetUserExpertCount(user))
                {
                    try
                    {
                        flag = 1 == adminExpertRequestsAccessor.UpdateExpertDemote(user); // 1 record should be affected.

                        if (flag)
                        {
                            var refUser = this.experts.Single(e => e.UserID == user.UserID); // Get reference from the collection itself.

                            this.users.Add(refUser);

                            this.experts.Remove(refUser);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            return flag;
        }

        // Created By: Trent Cullinan 03/15/2016
        private bool GetAdminRoleStatus(AccessToken accessToken)
        {
            return 0 < accessToken.Roles.Where(r => r.RoleID.Equals(ADMIN)).Count();
        }

        // Created By: Trent Cullinan 03/15/2016
        private IEnumerable<User> GetUserCollection(IEnumerable<User> users, string query)
        {
            IEnumerable<User> results = null;

            query = query.ToLower();

            // Search UserNames
            results = users.Where(
                u => u.UserName.ToLower().Contains(query));
            // Search FirstNames
            results = results.Union(
                users.Where(u => u.FirstName.ToLower().Contains(query)));
            // Search LastNames
            results = results.Union(
                users.Where(u => u.LastName.ToLower().Contains(query)));

            return results;
        }

        // Created By: Trent Cullinan 03/15/2016
        private int GetUserExpertCount(User user)
        {
            return experts.Where(u => u.UserID == user.UserID).Count();
        }

        // Created By: Trent Cullinan 03/15/2016
        private void RemoveRequest(ExpertRequest request)
        {
            this.requests.Remove(this.requests.Single(r => r.RequestID == request.RequestID));
        }


        ///<summary>
        ///Author: Stenner Kvindlog 
        ///passes application data to data access layer
        ///Date: 3/19/16
        ///</summary>
        public bool AddExpertApplication(String Title, String Description, int UserID, DateTime Time)
        {

            try
            {
                bool myBool = ExpertAccessor.CreateExpertApplication(Title, Description, UserID, Time);
                return myBool;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
