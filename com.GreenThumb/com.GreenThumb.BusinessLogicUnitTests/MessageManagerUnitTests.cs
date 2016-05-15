using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic.FakeStuff;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    /// <summary>
    /// Ryan Taylor
    /// Created: 4/14/2016
    /// 
    /// Test class to test IMessageManager Interface
    /// </summary>

    [TestClass]
    public class MessageManagerUnitTests
    {
        private IMessageManager _mgr;

        [TestInitialize]
        public void TestStartup()
        {
            _mgr = new FakeMessageManager();
        }

        [TestMethod]
        public void TestGetUserMessageExpectingOneMessage()
        {
            // Arrange
            List<Message> msg;
            string username = "jeffb";
            int expectedCount = 1;

            // Act
            msg = _mgr.GetUserMessages(username);

            // Assert
            Assert.AreEqual(expectedCount, msg.Count);
        }

        [TestMethod]
        public void TestGetUserMessageExpectingNoMessage()
        {
            // Arrange
            List<Message> msg;
            string username = "notrealuser";
            int expectedCount = 0;

            // Act
            msg = _mgr.GetUserMessages(username);

            // Assert
            Assert.AreEqual(expectedCount, msg.Count);
        }

        [TestMethod]
        public void TestMarkMessageDeletedReceiverPass()
        {
            // Arrange
            string username = "jeffb";
            int messageID = 1000;
            bool expectedResult = true;
            bool result;

            // Act
            result = _mgr.EditMessageDeletedReceiver(username, messageID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMarkMessageDeletedReceiverFail()
        {
            // Arrange
            string username = "jeffb";
            int messageID = 1001;
            bool expectedResult = false;
            bool result;

            // Act
            result = _mgr.EditMessageDeletedReceiver(username, messageID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMarkMessageDeletedSenderPass()
        {
            // Arrange
            string username = "jeffb";
            int messageID = 1000;
            bool expectedResult = true;
            bool result;

            // Act
            result = _mgr.EditMessageDeletedSender(username, messageID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMarkMessageDeletedSenderFail()
        {
            string username = "jeffb";
            int messageID = 1001;
            bool expectedResult = false;
            bool result;

            // Act
            result = _mgr.EditMessageDeletedSender(username, messageID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMarkMessageReadPass()
        {
            // Arrange
            string username = "jeffb";
            int messageID = 1000;
            bool expectedResult = true;
            bool result;

            // Act
            result = _mgr.EditMessageRead(username, messageID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMarkMessageReadFail()
        {
            string username = "jeffb";
            int messageID = 1001;
            bool expectedResult = false;
            bool result;

            // Act
            result = _mgr.EditMessageRead(username, messageID);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestSendMessagePass()
        {
            // Arrange
            string content = "content", subject = "subject", sender = "jeffb", receiver = "guy";
            bool expectedResult = true;
            bool result;

            // Act
            result = _mgr.SendMessage(content, subject, sender, receiver);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestSendMessageFail()
        {
            // Arrange
            string content = "content", subject = "subject", sender = "jeffb", receiver = "notAUser";
            bool expectedResult = false;
            bool result;

            // Act
            result = _mgr.SendMessage(content, subject, sender, receiver);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCleanup]
        public void TestTeardown()
        {
            _mgr = null;
        }
    }
}
