using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessLogic.FakeStuff;
using com.GreenThumb.BusinessObjects;

/// <summary>
/// Test Class Created by Kristine Johnson 2/28/16
/// Tests to ensure a group list is returned.
/// </summary>

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class GroupManagerUnitTest
    {
        IGroupManager groupManager = null;
        Organization org = null;
        [TestInitialize]
        public void TestSetup()
        {
            groupManager = new FakeGroupManager();
           
        
        }
        [TestMethod]
        public void GetGroupListReturnGroupList()
        {
            org = new Organization() 
            {
                OrganizationID = 1000
            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            List<com.GreenThumb.BusinessObjects.Group> groupList = groupManager.GetGroupList(org.OrganizationID);
            Assert.AreEqual(2, groupList.Count);
            ///assert, that it's not null-populated with actual date, so put more then one assert statement
            ///List<Group> group = groupManager.Group;
            ///assert
            ///Assert.AreNotEqual(0, group.Count);

        }

        [TestMethod]
        public void GetGroupListOrgHasNoGroups()
        {
            org = new Organization()
            {
                OrganizationID = 1002
            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            List<com.GreenThumb.BusinessObjects.Group> groupList = groupManager.GetGroupList(org.OrganizationID);
            Assert.AreEqual(0, groupList.Count);
            ///assert, that it's not null-populated with actual date, so put more then one assert statement
            ///List<Group> group = groupManager.Group;
            ///assert
            ///Assert.AreNotEqual(0, group.Count);
            ///
        }



        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// </summary>
        [TestMethod]
        public void TestGetGroupsForUserReturnListWithGroup()
        {
            // Arrange
            int userID = 1;
            List<BusinessObjects.Group> group = null;
            int expectedCount = 1;

            // Act
            group = groupManager.GetGroupsForUser(userID);

            // Assert
            Assert.AreEqual(expectedCount, group.Count);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// </summary>
        [TestMethod]
        public void TestGetGroupsForUserReturnListNoGroups()
        {
            // Arrange
            int userID = 2;
            List<BusinessObjects.Group> group = null;
            int expectedCount = 0;

            // Act
            group = groupManager.GetGroupsForUser(userID);

            // Assert
            Assert.AreEqual(expectedCount, group.Count);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// </summary>
        [TestMethod]
        public void TestAddGroupPass()
        {
            // Arrange
            int userID = 1;
            string groupName = "12345";
            bool expectedResult = true;

            // Act + Assert
            Assert.AreEqual(expectedResult, groupManager.AddGroup(userID, groupName));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// </summary>
        [TestMethod]
        public void TestAddGroupFailUserID()
        {
            // Arrange
            int userID = 1;
            string groupName = "12345";
            bool expectedResult = true;

            // Act + Assert
            Assert.AreEqual(expectedResult, groupManager.AddGroup(userID, groupName));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// </summary>
        [TestMethod]
        public void TestAddGroupFailNameShort()
        {
            // Arrange
            int userID = 1;
            string groupName = "1";
            bool expectedResult = false;

            // Act + Assert
            Assert.AreEqual(expectedResult, groupManager.AddGroup(userID, groupName));
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 03/24/16
        /// </summary>
        [TestMethod]
        public void TestAddGroupFailNameLong()
        {
            // Arrange
            int userID = 1;
            string groupName = "";
            for (int i = 0; i < 25; i++)
            {
                groupName += "All work and no play make Jack a dull boy. ";
            }
            bool expectedResult = false;

            // Act + Assert
            Assert.AreEqual(expectedResult, groupManager.AddGroup(userID, groupName));
        }
        
    }
}
