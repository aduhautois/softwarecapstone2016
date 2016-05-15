using com.GreenThumb.BusinessLogic.FakeStuff;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    /// <summary>
    /// Class to Unit test create garden functionality by Poonam Dubey
    /// </summary>
    [TestClass]
    public class GardenManagerUnitTest
    {
        IGardenManager gardenManager = null;
        Garden garden = null;
        [TestMethod]
        public void CreateSampleGarden()
        {
            garden = new Garden()
            {
                GroupID = 1000,
                UserID = 1001,
                GardenDescription = "This is a Test Garden",
                GardenRegion = "Test Garden region"

            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            bool isSuccess = gardenManager.AddNewGarden(garden);
            Assert.AreEqual(true, isSuccess);

        }

        [TestMethod]
        public void CreateGardenWithoutGroup()
        {
            garden = new Garden()
            {
                GroupID = 0,
                UserID = 1001,
                GardenDescription = "This is a Test Garden",
                GardenRegion = "Test Garden region"

            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            bool isSuccess = gardenManager.AddNewGarden(garden);
            Assert.AreNotEqual(true, isSuccess);

        }

        [TestMethod]
        public void CreateGardenWithoutUser()
        {
            garden = new Garden()
            {
                GroupID = 1000,
                UserID = 5,
                GardenDescription = "This is a Test Garden",
                GardenRegion = "Test Garden region"

            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            bool isSuccess = gardenManager.AddNewGarden(garden);
            Assert.AreNotEqual(true, isSuccess);

        }

        [TestMethod]
        public void CreateGardenWithoutDescription()
        {
            garden = new Garden()
            {
                GroupID = 1000,
                UserID = 1001,
                GardenDescription = null,
                GardenRegion = "Test Garden region"

            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            bool isSuccess = gardenManager.AddNewGarden(garden);
            Assert.AreNotEqual(true, isSuccess);

        }


        IGroupManager groupManager = null;
        Organization org = null;
        [TestInitialize]
        public void TestSetup()
        {
            groupManager = new FakeGroupManager();
            gardenManager = new FakeGardenManager();


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
    }
}
