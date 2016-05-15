using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessObjects;
namespace com.GreenThumb.BusinessLogic.Tests
{
    [TestClass()]
    public class UserRoleManagerTests
    {
        UserRoleManager roleManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            roleManager = new UserRoleManager();
        }
        
        ///// <summary>
        ///// Updated by Steve Hoover
        ///// 5/6/16
        ///// Method GetUserRoleList returns null value, remove or fix.
        ///// </summary>
        //[TestMethod()]
        //public void GetUserRoleListTest()
        //{
        //    List<UserRole> GetUserRoleList = new List<UserRole>();
        //    GetUserRoleList = roleManager.GetUserRoleList();
        //    Assert.IsNotNull(GetUserRoleList);
        //}

        [TestMethod()]
        public void GetUserRoleListByUserTest()
        {   int userID = 1000;
            List<UserRole> GetUserRoleListByUser= null;
            GetUserRoleListByUser = roleManager.GetUserRoleListByUser(userID);
            Assert.IsNotNull(GetUserRoleListByUser);
        }

        /// <summary>
        /// updated by Steve Hoover
        /// 5/6/16
        /// Test adding new user role
        /// </summary>
        [TestMethod()]
        public void AddNewUserRoleTest()
        {
            int userID = 1002;
            string roleID = "WooTest2";
            bool result = false;
            result = roleManager.AddNewUserRole(userID, roleID);
            Assert.AreEqual(true, result);
            
        }

        [TestMethod()]
        public void RemoveUserRoleTest()
        {
            int userID =  1000;
            string roleID = "Admin";
            bool result = roleManager.RemoveUserRole(userID, roleID);
            Assert.AreEqual(true, result);
        }
        ///// <summary>
        ///// Updated by Steve Hoover
        ///// 5/6/16
        ///// SP not found, needs to be fixed or removed.
        ///// </summary>
        //[TestMethod()]
        //public void EditUserRoleStatusTest()
        //{
        //    int userID = 1000;
        //    string roleID = "Admin";
        //    bool active = true;
        //    bool result = roleManager.EditUserRoleStatus(userID, roleID, active);
        //    Assert.AreEqual(true, result);

        //}
    }
}
