using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    public class ResponseManager
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Creates a question response
        /// </summary>
        /// <param name="response">The response to be added</param>
        /// <returns>True if the response was successfully created</returns>
        public bool AddResponse(Response response)
        {
            bool created = false;

            try
            {
                created = QuestionResponseAccessor.CreateResponse(response);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Response could not be created: " + ex.Message);
            }

            return created;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Edits an existing response
        /// </summary>
        /// <param name="newResponse">The response with changes added</param>
        /// <param name="oldResponse">The unchanged response</param>
        /// <returns>True if the response was successfully edited</returns>
        public bool EditResponse(Response newResponse, Response oldResponse)
        {
            bool created = false;

            try
            {
                created = QuestionResponseAccessor.UpdateResponse(newResponse, oldResponse);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Response could not be updated: " + ex.Message);
            }

            return created;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all responses to a certain question
        /// </summary>
        /// <param name="questionID">Question ID</param>
        /// <returns>All responses to a certain question</returns>
        public List<Response> GetResponsesByQuestionID(int questionID)
        {
            List<Response> responses = new List<Response>();

            try
            {
                responses = QuestionResponseAccessor.RetrieveResponsesByQuestionID(questionID);
            }
            catch(Exception)
            {
                throw new ApplicationException("Responses could not be retrieved");
            }

            return responses;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets a response based on the question and owner of the response
        /// </summary>
        /// <param name="questionID">Question ID</param>
        /// <param name="userID">User ID of the user that owns the response</param>
        /// <returns>The response to a question that a user has replied to</returns>
        public Response GetResponseByQuestionIDAndUser(int questionID, int userID)
        {
            Response response = new Response();

            try
            {
                response = QuestionResponseAccessor.RetrieveResponseByQuestionIDAndUser(questionID, userID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Responses could not be retrieved");
            }

            return response;
        }
    }
}
