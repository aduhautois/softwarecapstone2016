using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;
namespace com.GreenThumb.BusinessLogicUnitTests
{
    /// <summary>
    /// Test class to test groupLeaderRequestManager
    /// Created By: Nasr Mohammed 
    /// Date Created: 5/5/2016 
    /// </summary>
    /// 
    [TestClass]
    public class GroupLeaderRequestManagerUnitTests
    {
        GroupLeaderRequestManager groupLeaderRequestManager = null;
        private AccessToken _accToken = null;


        [TestInitialize]
        public void TestSetup()
        {
            // _accToken = new AccessToken();
            groupLeaderRequestManager = new GroupLeaderRequestManager(_accToken);
        }
        [TestMethod]
        public void TestGetUserGroups()
        {
            List<Group> userGroup = null;
            try
            {
                userGroup = groupLeaderRequestManager.GetUserGroups();
                Assert.IsNotNull(userGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);

            }
        }
    }
}
