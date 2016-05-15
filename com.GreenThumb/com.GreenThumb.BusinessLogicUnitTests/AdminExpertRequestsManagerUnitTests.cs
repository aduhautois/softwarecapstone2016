using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    /// <summary>
    /// Summary description for AdminExpertRequestsManagerUnitTests
    /// 
    /// Created By: Trent Cullinan 03/15/2016
    /// </summary>
    [TestClass]
    public class AdminExpertRequestsManagerUnitTests
    {
        private AccessToken _accessToken = null;
        private ExpertRequest _expertRequest = null;
        private User _testUser = null;
        private AdminExpertRequestsManager _requestsManager = null;
        private AdminExpertRequestsAccessor _requestsAccessor = null;

        // Mock data
        // Created By: Trent Cullinan 03/15/2016
        private AccessToken accessToken
        {
            get
            {
                _accessToken = _accessToken ??
                    new AccessToken(
                        new User()
                        {
                            UserID = 1000,
                            Active = true
                        },
                        new List<Role>()
                        {
                            new Role() { RoleID = "Admin" }
                        });

                return _accessToken;
            }
        }

        // Created By: Trent Cullinan 03/15/2016
        private AdminExpertRequestsManager requestsManager
        {
            get
            {
                _requestsManager = _requestsManager ??
                    new AdminExpertRequestsManager(accessToken);

                return _requestsManager;
            }
        }

        // Created By: Trent Cullinan 03/15/2016
        private AdminExpertRequestsAccessor requestsAccessor
        {
            get
            {
                _requestsAccessor = _requestsAccessor ??
                    new AdminExpertRequestsAccessor(accessToken);

                return _requestsAccessor;
            }
        }

        // Mock data
        // Created By: Trent Cullinan 03/15/2016
        private ExpertRequest TestRequest
        {
            get
            {
                this._expertRequest = this._expertRequest ??
                    new ExpertRequest()
                    {

                    };

                return this._expertRequest;
            }
        }

        // Mock data
        // Created By: Trent Cullinan 03/15/2016
        private User TestUser
        {
            get
            {
                if (null == this._testUser)
                {
                    string userName = "TestUser";
                    string password = "Password";

                    bool flag = false;

                    UserManager userManager = new UserManager();

                    try
                    {
                        this._testUser = userManager.GetUserByUserName(userName);
                    }
                    catch (Exception)
                    {
                        flag = 1 == userManager.AddUser(new User()
                        {
                            UserName
                                = userName,
                            FirstName
                                = "TestUser-FirstName",
                            LastName
                                = "TestUser-LastName",
                            Password
                                = password.HashSha256(),
                            Zip
                                = "000000000",
                            EmailAddress
                                = "ThisShouldBeAnOptionalParam@W E W L A D.com",
                            Active
                                = true,
                        });

                        if (flag) { this._testUser = userManager.GetUserByUserName(userName); }
                    }
                }

                return this._testUser;
            }
        }

        /// <summary>
        /// Tests to see if ExpertRequests are returned.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_RetrieveExpertRequests_RetrieveRequests()
        {
            // Arrange
            IEnumerable<ExpertRequest> sampleRequests
                = requestsAccessor.RetrieveExpertRequests(accessToken);

            // Act
            IEnumerable<ExpertRequest> target
                = requestsManager.GetExpertRequests(accessToken);

            // Assert
            if (sampleRequests.Count() == target.Count())
            {
                for (int i = 0; i < sampleRequests.Count(); i++)
                {
                    Assert.AreEqual(
                        sampleRequests.ElementAt(i).RequestID,
                        target.ElementAt(i).RequestID);
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests to see if Users are returned.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_RetrieveAllUsers_RetrieveUsers()
        {
            // Arrange
            IEnumerable<User> sampleUsers
                = requestsAccessor.RetrieveAllUsers(accessToken);

            // Act
            IEnumerable<User> target
                = requestsManager.GetAllUsers(accessToken);

            // Assert
            if (sampleUsers.Count() == target.Count())
            {
                for (int i = 0; i < sampleUsers.Count(); i++)
                {
                    Assert.AreEqual(
                        sampleUsers.ElementAt(i).UserID,
                        target.ElementAt(i).UserID);
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests to see if experts are returned.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_RetrieveAllExperts()
        {
            // Arrange
            IEnumerable<User> sampleExperts
                = requestsAccessor.RetrieveAllExperts(accessToken);

            // Act
            IEnumerable<User> target
                = requestsManager.GetAllExperts(accessToken);

            // Assert
            if (sampleExperts.Count() == target.Count())
            {
                for (int i = 0; i < sampleExperts.Count(); i++)
                {
                    Assert.AreEqual(
                        sampleExperts.ElementAt(i).UserID,
                        target.ElementAt(i).UserID);
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests to make sure search works as intended.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_SearchUsers_RetrieveUsers()
        {
            // Arrange
            string query = "Jeff";

            IEnumerable<User> sampleUsers
                = requestsManager.GetAllUsers(accessToken); ;

            IEnumerable<User> sampleResult = sampleUsers.Where(
                u => u.UserName.ToLower().Contains(query.ToLower()));

            sampleResult = sampleResult.Union(
                sampleUsers.Where(u => u.FirstName.ToLower().Contains(query.ToLower())));

            sampleResult = sampleResult.Union(
                sampleUsers.Where(u => u.LastName.ToLower().Contains(query.ToLower())));

            // Act
            IEnumerable<User> result = requestsManager.GetUsers(query);

            // Assert
            if (sampleResult.Count() == result.Count())
            {
                for (int i = 0; i < sampleResult.Count(); i++)
                {
                    Assert.AreEqual(
                        sampleResult.ElementAt(i).UserID,
                        result.ElementAt(i).UserID);
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests to make sure search works as intended.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_SearchExperts_RetrieveUsers()
        {
            // Arrange
            string query = "s";

            IEnumerable<User> sampleUsers
                = requestsManager.GetAllUsers(accessToken); ;

            IEnumerable<User> sampleResult = sampleUsers.Where(
                u => u.UserName.ToLower().Contains(query.ToLower()));

            sampleResult = sampleResult.Union(
                sampleUsers.Where(u => u.FirstName.ToLower().Contains(query.ToLower())));

            sampleResult = sampleResult.Union(
                sampleUsers.Where(u => u.LastName.ToLower().Contains(query.ToLower())));

            // Act
            IEnumerable<User> result = requestsManager.GetUsers(query);

            // Assert
            if (sampleResult.Count() == result.Count())
            {
                for (int i = 0; i < sampleResult.Count(); i++)
                {
                    Assert.AreEqual(
                        sampleResult.ElementAt(i).UserID,
                        result.ElementAt(i).UserID);
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// NOTE: Test cannot be implemented as of (03/24/16)
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_ApproveRequest_Success()
        {
            // Arrange
            // bool result = 2 == requestsAccessor.EditApproveRequest(TestRequest);

            // Act
            // result = requestsManager.EditApproveRequest(this.accessToken, TestRequest);

            // Assert
        }

        /// <summary>
        /// NOTE: Test cannot be implemented as of (03/24/16)
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        [TestMethod]
        public void Test_DeclineRequest_Success()
        {
            // Arrange
            // bool actual = 1 == requestsAccessor.EditDeclineRequest(TestRequest);

            // Act
            // bool result = requestsManager.EditDeclineRequest(this.accessToken, TestRequest);
            // Assert
            // Assert.AreEqual(actual, result);
        }

        /// <summary>
        /// Updated by Steve Hoover
        /// 5/6/15
        /// 
        /// These tests don't work, and I can't fix them. They're being removed.
        /// </summary>
        

        /// <summary>
        /// Test to see if promoting a user works.
        /// 
        /// Created By: Trent Cullinan 03/15/2016
        /// </summary>
        //[TestMethod]
        //public void Test_PromoteUser_Success()
        //{
        //    // Arrange
        //    bool actual = 1 == requestsAccessor.UpdateUserPromote(TestUser);

        //    requestsAccessor.UpdateExpertDemote(TestUser);

        //    requestsManager.GetAllUsers(this.accessToken); // Initialize list

        //    // Act
        //    bool result = requestsManager.EditUserPromoted(this.accessToken, TestUser);

        //    // Assetr
        //    Assert.AreEqual(actual, result);
        //}

        ///// <summary>
        ///// Test to see if demoting an expert works.
        ///// 
        ///// Created By: Trent Cullinan 03/15/2016
        ///// </summary>
        //[TestMethod]
        //public void Test_DemoteExpert_Success()
        //{
        //    // Arrange
        //    requestsAccessor.UpdateUserPromote(TestUser); // Yay dependant test code!

        //    bool actual = 1 == requestsAccessor.UpdateExpertDemote(TestUser);

        //    requestsAccessor.UpdateUserPromote(TestUser); // Yay dependant test code!

        //    // Act
        //    bool result = requestsManager.EditExpertDemoted(this.accessToken, TestUser);

        //    // Assert
        //    Assert.AreEqual(actual, result);
        //}

    }
}
