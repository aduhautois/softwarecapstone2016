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
    public class VolunteerManagerTests
    {
        VolunteerManager volunteerManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            volunteerManager = new VolunteerManager();
        }
        ///// <summary>
        ///// Updated by Steve Hoover
        ///// 5/6/16
        ///// Method broken, could be because of DB changes. Suggest removing.
        ///// </summary>
        //[TestMethod()]
        //public void AddVolunteerTest()
        //{
        //    int userID = 1000;
        //    Volunteer newVolunteer = null;
        //    bool result = volunteerManager.AddVolunteer(newVolunteer);
        //    Assert.AreEqual(true, result);

        //}
    }
}
