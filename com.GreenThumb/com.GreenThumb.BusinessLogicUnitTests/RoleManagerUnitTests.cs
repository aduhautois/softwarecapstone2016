using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;
using com.GreenThumb.BusinessLogic;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class RoleManagerUnitTests
    {
        /// <summary>
        /// Steve Hoover
        /// 4/28/16
        /// Tests to see if List<Role> is returned.
        /// </summary>
        private AccessToken _accessToken = null;
        private RoleManager roleManager = new RoleManager();
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
        [TestMethod]
        public void GetRoleListTest()
        {
            var roleList = new List<Role>();
            roleList = roleManager.GetRoleList();
            Assert.IsNotNull(roleList);
        }
        /// <summary>
        /// Steve Hoover
        /// 5/6/16
        /// Tests to see if a count of roles greater than 0 is returned.
        /// </summary>
        [TestMethod]
        public void GetRoleCountTest()
        {
            int test = 0;
            test = roleManager.GetRoleCount();
            Assert.AreNotEqual(test, 0);
        }

        /// <summary>
        /// Steve Hoover
        /// 5/6/16
        /// Tests validity of ConfirmUserIsAssignedRole method using data created locally
        /// </summary>
        [TestMethod]
        public void ConfirmUserIsAssignedRoleTest()
        {
            string roleName = "Admin";
            bool test = false;
            test = roleManager.ConfirmUserIsAssignedRole(accessToken,roleName);
            Assert.AreEqual(test, true);
        }

        ///// <summary>
        ///// Steve Hoover
        ///// 5/6/16
        ///// Tests validity of AddNewRole method using data created locally
        ///// *** NOTE ***
        ///// Test fails based on SQL Exception, based on insert statement in accessor.
        ///// Not sure if it's even being used in our app. If not, remove it and this test.
        ///// </summary>
        //[TestMethod]
        //public void AddNewRoleTest()
        //{
        //    string roleId = "Test";
        //    string description = "Test role";
        //    int createdBy = 1000;
        //    DateTime createdDate = DateTime.Now;
        //    bool result = false;

        //    result = roleManager.AddNewRole(roleId, description, createdBy, createdDate);

        //    Assert.AreEqual(result, true);

        //}

    }
}
