using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class QuestionManager
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Creates a question for experts to respond to
        /// </summary>
        /// <param name="question">The question asked</param>
        /// <returns>True if the question was created successfully</returns>
        public bool AddQuestion(Question question)
        {
            bool created = false;

            try
            {
                created = QuestionAccessor.CreateQuestion(question);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Question could not be created: " + ex.Message);
            }

            return created;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets a single question based on the id
        /// </summary>
        /// <param name="questionID">Question ID</param>
        /// <returns>Desired question</returns>
        public Question GetQuestionByID(int questionID)
        {
            Question question = new Question();

            try
            {
                question = QuestionAccessor.RetrieveQuestionByID(questionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Question could not be retrieved: " + ex.Message);
            }

            return question;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions for a region
        /// </summary>
        /// <param name="regionID">Region ID</param>
        /// <returns>List of questions related to a region</returns>
        public List<Question> GetQuestionsByRegionID(int regionID)
        {
            List<Question> questions = new List<Question>();

            try
            {
                questions = QuestionAccessor.RetrieveQuestionsByRegionID(regionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Questions could not be retrieved: " + ex.Message);
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions asked by a user
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>List of questions asked by a user</returns>
        public List<Question> GetQuestionsByUserID(int userID)
        {
            List<Question> questions = new List<Question>();

            try
            {
                questions = QuestionAccessor.RetrieveQuestionsByUserID(userID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Questions could not be retrieved: " + ex.Message);
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions that are like the keyword
        /// </summary>
        /// <param name="keyword">The string that is compared to question titles and content</param>
        /// <returns>List of questions that have the keyword in the title or content</returns>
        public List<Question> GetQuestionsWithKeyword(string keyword)
        {
            List<Question> questions = new List<Question>();

            try
            {
                questions = QuestionAccessor.RetrieveQuestionsWithKeyword(keyword);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Questions could not be retrieved: " + ex.Message);
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions that are like the keyword and are for a region
        /// </summary>
        /// <param name="keyword">The string that is compared to question titles and content</param>
        /// <param name="regionID">Region ID</param>
        /// <returns>List of questions that have the keyword in the title or content and are related to the region</returns>
        public List<Question> GetQuestionsWithKeywordAndRegion(string keyword, int? regionID)
        {
            List<Question> questions = new List<Question>();

            try
            {
                questions = QuestionAccessor.RetrieveQuestionsWithKeywordAndRegion(keyword, regionID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Questions could not be retrieved: " + ex.Message);
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions that have a null region
        /// </summary>
        /// <returns>List of questions with no region</returns>
        public List<Question> GetQuestionsWithNoRegion()
        {
            List<Question> questions = new List<Question>();

            try
            {
                questions = QuestionAccessor.RetrieveQuestionsWithNoRegion();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Questions could not be retrieved: " + ex.Message);
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions
        /// </summary>
        /// <returns>List of all questions</returns>
        public List<Question> GetQuestions()
        {
            List<Question> questions = new List<Question>();

            try
            {
                questions = QuestionAccessor.RetrieveQuestions();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Questions could not be retrieved: " + ex.Message);
            }

            return questions;
        }
    }
}
