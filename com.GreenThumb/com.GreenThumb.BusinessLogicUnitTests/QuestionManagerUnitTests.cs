using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;

namespace com.GreenThumb.BusinessLogicUnitTests
{

    [TestClass]
    public class QuestionManagerUnitTests
    {
        QuestionManager questionManager = null;
        Question question = null;

        [TestInitialize]
        public void TestSetup()
        {
            questionManager = new QuestionManager();
        }

        /// <summary>
        /// Steve Hoover
        /// Created Date: 4/28/16
        /// Tests to verify question created successfully
        /// </summary>
        [TestMethod]
        public void TestCreateQuestion()
        {
            // arrange
            bool test = false;
            question = new Question(1001,
            "test",
            "test",
            "test again",
            4,
            1001,
            DateTime.Now,
            1001,
            DateTime.Now);

            // act
            test = questionManager.AddQuestion(question);

            // assert
            Assert.AreEqual(test, true);
        }
        /// <summary>
        /// Steve Hoover
        /// 4/29/16
        /// Test expects a non-null value returned to question
        /// </summary>
        [TestMethod]
        public void TestGetQuestionById()
        {
            int questionId = 1000;
            Question question = new Question();

            question = questionManager.GetQuestionByID(questionId);

            Assert.IsNotNull(question);
        }
        /// <summary>
        /// Steve Hoover
        /// 4/29/16
        /// Test expects a non-null list of values returned to questions
        /// </summary>
        [TestMethod]
        public void TestGetQuestionsByRegionId()
        {
            int regionId = 1000;
            List<Question> questions = new List<Question>();

            questions = questionManager.GetQuestionsByRegionID(regionId);

            Assert.IsNotNull(questions);
        }

        /// <summary>
        /// Steve Hoover
        /// 4/29/16
        /// Test expects a non-null list of values returned to questions based on the userId
        /// </summary>
        [TestMethod]
        public void TestGetQuestionsByUserId()
        {
            int userId = 1000;
            List<Question> questions = new List<Question>();

            questions = questionManager.GetQuestionsByUserID(userId);

            Assert.IsNotNull(questions);
        }

        /// <summary>
        /// Steve Hoover
        /// 4/29/16
        /// Test expects a non-null list of values returned to questions based on a string keyword
        /// </summary>
        [TestMethod]
        public void TestGetQuestionsByKeyword()
        {
            string keyword = "garden";
            List<Question> questions = new List<Question>();

            questions = questionManager.GetQuestionsWithKeyword(keyword);

            Assert.IsNotNull(questions);
        }
        /// <summary>
        /// Steve Hoover
        /// 4/29/16
        /// Test expects a non-null list of values returned to questions based on a string keyword and an int regionId
        /// </summary>
        [TestMethod]
        public void TestGetQuestionsByKeywordAndRegionId()
        {
            string keyword = "garden";
            int regionId = 1000;
            List<Question> questions = new List<Question>();

            questions = questionManager.GetQuestionsWithKeywordAndRegion(keyword, regionId);

            Assert.IsNotNull(questions);
        }

    }
}
