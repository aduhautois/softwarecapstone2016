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
    /// Handles requests made to an organization leader.
    /// Created By: Trent Cullinan 02/24/2016
    /// </summary>
    public class OrgRequestsManager
    {
        private Organization organization;
        private OrgUserAccessor orgUserAccessor;
        private IEnumerable<GroupLeaderRequest> orgRequests;

        /// <summary>
        /// Initializes constructor and validates that whomever tries to access is the organization leader.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="organization">Organization to be referenced.</param>
        public OrgRequestsManager(AccessToken accessToken, Organization organization)
        {
            if (GetAccessToken(accessToken, organization))
            {
                try
                {
                    orgUserAccessor = new OrgUserAccessor(accessToken, organization);
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
        /// Retrieve current requests for an organization.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <returns>Collection of group leader requests.</returns>
        public IEnumerable<GroupLeaderRequest> GetOrgRequests(AccessToken accessToken)
        {
            if (GetAccessToken(accessToken, this.organization))
            {
                this.orgRequests = this.orgRequests ?? orgUserAccessor.FetchGroupLeaderRequests(accessToken);
            }
            else
            {
                throw new ArgumentException("User is not the leader of the organization.");
            }

            return this.orgRequests;
        }

        /// <summary>
        /// Approve a current request.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="request">Request to be approved.</param>
        /// <param name="groupMember">User to be changed.</param>
        /// <returns>Whether process was successful.</returns>
        public bool EditApproveRequest(AccessToken accessToken, GroupLeaderRequest request, GroupMember groupMember)
        {
            bool flag = false;

            if (GetAccessToken(accessToken, this.organization))
            {
                int result = orgUserAccessor.AddLeaderToGroup(accessToken, request.Group, groupMember) +
                    orgUserAccessor.ProcessRequest(accessToken, request);

                flag = 2 == result; // 2 rows should be affected.

                if (flag)
                {
                    // Remove request.
                    this.orgRequests = this.orgRequests.Except(this.orgRequests.Where(r => r.RequestID == request.RequestID));
                }
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return flag;
        }

        /// <summary>
        /// Decline a current request.
        /// Created By: Trent Cullinan 02/24/2016
        /// </summary>
        /// <param name="accessToken">Confirm user is valid to use method.</param>
        /// <param name="request">Request to be declined.</param>
        /// <returns>Whether process was successful.</returns>
        public bool EditDeclineRequest(AccessToken accessToken, GroupLeaderRequest request)
        {
            bool flag = false;

            if (GetAccessToken(accessToken, this.organization))
            {
                // Do nothing with request.
                flag = 1 == orgUserAccessor.ProcessRequest(accessToken, request); // 1 row should be affected.

                if (flag)
                {
                    // Remove request.
                    this.orgRequests = this.orgRequests.Except(this.orgRequests.Where(r => r.RequestID == request.RequestID));
                }
            }
            else
            {
                throw new Exception("User is not the leader of the organization.");
            }

            return flag;
        }

        // Created By: Trent Cullinan 02/24/2016
        private bool GetAccessToken(AccessToken accessToken, Organization organization)
        {
            return organization.OrganizationLeader.UserID == accessToken.UserID;
        }
    }
}
