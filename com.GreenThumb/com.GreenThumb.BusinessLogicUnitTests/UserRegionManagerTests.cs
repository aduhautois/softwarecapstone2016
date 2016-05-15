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
    public class UserRegionManagerTests
    {
        UserRegionManager regionManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            regionManager = new UserRegionManager();
        }
        ///// <summary>
        ///// Updated by Steve Hoover
        ///// 5/6/15
        ///// Method returning null, recommend fixing or removing.
        ///// </summary>
        //[TestMethod()]
        //public void GetUserListTest()
        //{
        //    List<User> GetUserList = null;
        //    GetUserList = regionManager.GetUserList();
        //    Assert.IsNotNull(GetUserList);

        //}

        

        [TestMethod()]
        public void EditUserDataTest()
        {
            int userID =1000;
            int regionID = 1;
            bool result = regionManager.EditUserData(userID, regionID);
            Assert.AreEqual(true, result);

        }

        [TestMethod()]
        public void GetAndDisplayUserRecordTest()
        {
            User user = null;
            int userID = 1000;
            user = regionManager.GetAndDisplayUserRecord(userID);
            Assert.IsNotNull(user);

        }
    }
}
