/// <summary>
/// Ryan Taylor
/// Created: 2016/02/25
/// Test class to test ISecurityManager Interface
/// </summary> 

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic.FakeStuff;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class SecurityManagerUnitTests
    {
        private ISecurityManager _security;
        private AccessToken _token;

        [TestInitialize]
        public void TestInitialize()
        {
            _security = new FakeSecurityManager();
        }

        [TestMethod]
        public void TestExistingUserReturnsValidAccessToken()
        {
            // Arange
            string username = "ryant";
            string password = "password";

            // Act
            _token = _security.ValidateExistingUser(username, password);

            // Assert
            Assert.AreEqual(username, _token.UserName);

        }

        [TestMethod]
        public void TestExistingUserReturnsNullAccessToken()
        {
            // Arange
            string username = "notryant";
            string password = "notpassword";

            // Act
            _token = _security.ValidateExistingUser(username, password);

            // Assert
            Assert.AreEqual(null, _token);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _security = null;
        }
    }
}
