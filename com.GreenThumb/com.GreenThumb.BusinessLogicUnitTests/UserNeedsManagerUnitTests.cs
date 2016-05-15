using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class UserNeedsManagerUnitTests
    {
        UserNeedsManager userNeedsManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            userNeedsManager = new UserNeedsManager();
        }
        [TestMethod]
        public void TestGetSentContributions()
        {
            IEnumerable<NeedContribution> contributions = null;
            contributions = userNeedsManager.GetSentContributions();
            Assert.IsNotNull(contributions);
        }
        /// <summary>
        /// updated by Steve Hoover
        /// 5/6/16
        /// Returns null because no AcceptedContributions exist. Unable to find method(s) to create
        /// accepted contributions. Add info to DB or remove test if method not used.
        /// </summary>
        //[TestMethod]
        //public void TestGetAcceptedContributions()
        //{
        //    IEnumerable<NeedContribution> contributions = null;
        //    contributions = userNeedsManager.GetAcceptedContributions();
        //    Assert.IsNotNull(contributions);
        //}
        [TestMethod]
        public void TestGetDeclinedContributions()
        {
            IEnumerable<NeedContribution> contributions = null;
            contributions = userNeedsManager.GetDeclinedContributions();
            Assert.IsNotNull(contributions);
        }
        /// <summary>
        /// updated by Steve Hoover
        /// 5/6/16
        /// Returns null because no AvailableNeeds exist. Unable to find method(s) to create
        /// available needs. Add info to DB or remove test if method not used.
        /// </summary>
        //[TestMethod]
        //public void TestGetAvailableNeeds()
        //{
        //    IEnumerable<GardenNeed> needs = null;
        //    needs = userNeedsManager.GetAvailableNeeds();
        //    Assert.IsNotNull(needs);
        //}
        [TestMethod]
        public void TestRetrieveAllContributions()
        {
            IEnumerable<NeedContribution> contributions = null;
            contributions = userNeedsManager.RetrieveAllContributions();
            Assert.IsNotNull(contributions);
        }
    }
}
