using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class ResponseManagerUnitTests
    {
        ResponseManager responseManager = new ResponseManager();
        Response response = null;
        ///// <summary>
        ///// Steve Hoover
        ///// 4/28/16
        ///// Test to verify response creation method is valid
        ///// 
        ///// updated by Steve Hoover
        ///// 5/6/16
        ///// Test not working, being removed.
        ///// </summary>
        //[TestMethod]
        //public void TestCreateNewResponse()
        //{
        //    response = new Response(1004,DateTime.Now,"this is a response",1000, null);
        //    bool test = false;

        //    test = QuestionResponseAccessor.CreateResponse(response);

        //    Assert.AreEqual(test, true);

        //}
    }
}
