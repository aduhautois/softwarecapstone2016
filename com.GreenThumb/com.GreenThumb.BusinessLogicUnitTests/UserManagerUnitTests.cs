using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;
using com.GreenThumb.BusinessLogic.FakeStuff;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class UserManagerUnitTests
    {
        UserManager userManager = null;
        FakeUserManager fUserManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            userManager = new UserManager();
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for UserManager EditUserPersonalInfo class valid input 
        ///Date: 3/3/16
        ///</summary>
        [TestMethod]
        public void TestUserPersonalInformationInputReturnTrue()
        {
            //arrange
            int userID = 1000;
            string firstName = "Chris";
            string lastName = "Smith";
            string zip = "123456789";
            string emailAddress = "hello@gmail.com";
            int? regionId = 1;

            //act
            bool result = userManager.EditUserPersonalInfo(userID, firstName, lastName, zip, emailAddress, regionId);

            //assert
            Assert.AreEqual(true, result);
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for UserManager EditUserPersonalInfo class invalid zip
        ///Date: 3/3/16
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserPersonalInformationInputZipInvalid()
        {
            //arrange
            int userID = 1000;
            string firstName = "Chris";
            string lastName = "Smith";
            string zip = "1234567890";
            string emailAddress = "hello@gmail.com";
            int? regionId = null;

            //act
            bool result = userManager.EditUserPersonalInfo(userID, firstName, lastName, zip, emailAddress, regionId);

        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for UserManager EditUserPersonalInfo class invalid first name 
        ///Date: 3/3/16
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserPersonalInformationInputFirstNameInvalid()
        {
            //arrange
            int userID = 1000;
            string firstName = "";
            string lastName = "Smith";
            string zip = "123456789";
            string emailAddress = "";
            int? regionId = 1;

            //act
            bool result = userManager.EditUserPersonalInfo(userID, firstName, lastName, zip, emailAddress, regionId);

        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for UserManager EditUserPersonalInfo class invalid last name 
        ///Date: 3/3/16
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUserPersonalInformationInputLastNameInvalid()
        {
            //arrange
            int userID = 1000;
            string firstName = "Bob";
            string lastName = "";
            string zip = "123456789";
            string emailAddress = "";
            int? regionId = null;

            //act
            bool result = userManager.EditUserPersonalInfo(userID, firstName, lastName, zip, emailAddress, regionId);

        }

        ///<summary>
        ///Author: Chris Schwebach
        ///Test for UserManager GetPersonalInfo valid userID 
        ///Date: 3/3/16
        /// ///</summary>
        [TestMethod]
        public void TestGetUserPersonInfoListReturnInfo()
        {
            //arrange
            int UserID = 1000;

            //act
            var user = userManager.GetPersonalInfo(UserID);

            //assert
            //Assert.AreEqual(1, user.Count);
        }

        /// <summary>
        /// Function to test new user data By : Poonam Dubey, Dated : 23rd March 2016
        /// </summary>
        [TestMethod]
        public void TestInsertNewUser()
        {
            string fName = "Poonam";
            string lName = "Dubey";
            string zip = string.Empty;
            string emailAddress = string.Empty;
            string userName = "pdubey";
            string password = string.Empty;
            bool isActive = true;
            int? regionID = null;
            fUserManager = new FakeUserManager();
            //act
            int returnValue = fUserManager.AddNewUser(fName, lName, zip, emailAddress, userName, password, isActive, regionID);

            //assert
            Assert.AreEqual(1, returnValue);
        }

        /// <summary>
        /// Function to test user data which already exists By : Poonam Dubey, Dated : 23rd March 2016
        /// </summary>
        [TestMethod]
        public void TestInsertNewUserExistingUserName()
        {
            string fName = "Poonam";
            string lName = "Dubey";
            string zip = string.Empty;
            string emailAddress = string.Empty;
            string userName = "johnDoe";
            string password = string.Empty;
            bool isActive = true;
            int? regionID = null;
            fUserManager = new FakeUserManager();
            //act
            int returnValue = fUserManager.AddNewUser(fName, lName, zip, emailAddress, userName, password, isActive, regionID);

            //assert
            Assert.AreNotEqual(1, returnValue);
        }

    }
}
