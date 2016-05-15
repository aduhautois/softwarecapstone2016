using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessLogic.FakeStuff;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    /// <summary>
    /// Created By: Kristine Johnson
    /// </summary>
    [TestClass]
    public class CreateOrgUnitTest
    {
        ICreateOrgManager createOrgManager = null;
        Organization organization = null;
       

        [TestInitialize]
        public void TestSetup()
        {
            createOrgManager = new FakeCreateOrgManager();
            
        }
        [TestMethod]
        public void CreateANewOrganizationReturnTrue()
        {
            ///arrange  

                AccessToken _accessToken = new AccessToken();
                organization = new Organization()
                {
                
                Name = "NEWORG",
                ContactPhone = "3197842222" 
        };
            
            ///act          
                bool org = createOrgManager.AddNewOrganization(_accessToken.UserID, organization.Name, organization.ContactPhone);
            ///assert
            Assert.AreEqual(true, org);
    
            

        }
    }
}
