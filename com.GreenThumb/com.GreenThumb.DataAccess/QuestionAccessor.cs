using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using System.Data.SqlClient;

namespace com.GreenThumb.DataAccess
{
    public class QuestionAccessor
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets a question based off the ID
        /// </summary>
        /// <param name="questionID">QuestionID</param>
        /// <returns>Question desired</returns>
        public static Question RetrieveQuestionByID(int questionID)
        {
            Question question = new Question();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestionByID";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QuestionID", questionID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    question = new Question()
                    {
                        QuestionID = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Content = reader.GetString(3),
                        CreatedBy = reader.GetInt32(5),
                        CreatedDate = reader.GetDateTime(6)
                    };

                    if (reader.IsDBNull(2))
                    {
                        question.Category = null;
                    }
                    else
                    {
                        question.Category = reader.GetString(2);
                    }

                    if (reader.IsDBNull(4))
                    {
                        question.RegionID = null;
                    }
                    else
                    {
                        question.RegionID = (short)reader.GetInt32(4);
                    }

                    if (reader.IsDBNull(7))
                    {
                        question.ModifiedBy = null;
                    }
                    else
                    {
                        question.ModifiedBy = reader.GetInt32(7);
                    }

                    if (reader.IsDBNull(8))
                    {
                        question.ModifiedDate = null;
                    }
                    else
                    {
                        question.ModifiedDate = reader.GetDateTime(8);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return question;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions related to a specified region
        /// </summary>
        /// <param name="regionID">RegionID</param>
        /// <returns>All questions based on a specified region</returns>
        public static List<Question> RetrieveQuestionsByRegionID(int regionID)
        {
            List<Question> questions = new List<Question>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestionsByRegionID";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegionID", regionID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question()
                        {
                            QuestionID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(3),
                            CreatedBy = reader.GetInt32(5),
                            CreatedDate = reader.GetDateTime(6)
                        };

                        if (reader.IsDBNull(2))
                        {
                            question.Category = null;
                        }
                        else
                        {
                            question.Category = reader.GetString(2);
                        }

                        if (reader.IsDBNull(4))
                        {
                            question.RegionID = null;
                        }
                        else
                        {
                            question.RegionID = (short)reader.GetInt32(4);
                        }

                        if (reader.IsDBNull(7))
                        {
                            question.ModifiedBy = null;
                        }
                        else
                        {
                            question.ModifiedBy = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            question.ModifiedDate = null;
                        }
                        else
                        {
                            question.ModifiedDate = reader.GetDateTime(8);
                        }

                        questions.Add(question);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions asked by a user
        /// </summary>
        /// <param name="userID">UserID of user that asked questions</param>
        /// <returns>All questions asked by a user</returns>
        public static List<Question> RetrieveQuestionsByUserID(int userID)
        {
            List<Question> questions = new List<Question>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestionsByUserID";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question()
                        {
                            QuestionID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(3),
                            CreatedBy = reader.GetInt32(5),
                            CreatedDate = reader.GetDateTime(6)
                        };

                        if (reader.IsDBNull(2))
                        {
                            question.Category = null;
                        }
                        else
                        {
                            question.Category = reader.GetString(2);
                        }

                        if (reader.IsDBNull(4))
                        {
                            question.RegionID = null;
                        }
                        else
                        {
                            question.RegionID = (short)reader.GetInt32(4);
                        }

                        if (reader.IsDBNull(7))
                        {
                            question.ModifiedBy = null;
                        }
                        else
                        {
                            question.ModifiedBy = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            question.ModifiedDate = null;
                        }
                        else
                        {
                            question.ModifiedDate = reader.GetDateTime(8);
                        }

                        questions.Add(question);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions with a keyword like the question title or content
        /// </summary>
        /// <param name="keyword">Keyword that is compared with question titles and content</param>
        /// <returns>All questions like question titles and content</returns>
        public static List<Question> RetrieveQuestionsWithKeyword(string keyword)
        {
            List<Question> questions = new List<Question>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestionsWithKeyword";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyword", keyword);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question()
                        {
                            QuestionID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(3),
                            CreatedBy = reader.GetInt32(5),
                            CreatedDate = reader.GetDateTime(6)
                        };

                        if (reader.IsDBNull(2))
                        {
                            question.Category = null;
                        }
                        else
                        {
                            question.Category = reader.GetString(2);
                        }

                        if (reader.IsDBNull(4))
                        {
                            question.RegionID = null;
                        }
                        else
                        {
                            question.RegionID = (short)reader.GetInt32(4);
                        }

                        if (reader.IsDBNull(7))
                        {
                            question.ModifiedBy = null;
                        }
                        else
                        {
                            question.ModifiedBy = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            question.ModifiedDate = null;
                        }
                        else
                        {
                            question.ModifiedDate = reader.GetDateTime(8);
                        }

                        questions.Add(question);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions with a keyword like the question title or content and specified region
        /// </summary>
        /// <param name="keyword">Keyword that is compared with question titles and content</param>
        /// <param name="regionID">RegionID</param>
        /// <returns>All questions like question titles and content and specified region</returns>
        public static List<Question> RetrieveQuestionsWithKeywordAndRegion(string keyword, int? regionID)
        {
            List<Question> questions = new List<Question>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestionsWithKeywordAndRegion";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Keyword", keyword);

            if(regionID == null)
            {
                cmd.Parameters.AddWithValue("@RegionID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegionID", regionID);
            }
            

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question()
                        {
                            QuestionID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(3),
                            CreatedBy = reader.GetInt32(5),
                            CreatedDate = reader.GetDateTime(6)
                        };

                        if (reader.IsDBNull(2))
                        {
                            question.Category = null;
                        }
                        else
                        {
                            question.Category = reader.GetString(2);
                        }

                        if (reader.IsDBNull(4))
                        {
                            question.RegionID = null;
                        }
                        else
                        {
                            question.RegionID = (short)reader.GetInt32(4);
                        }

                        if (reader.IsDBNull(7))
                        {
                            question.ModifiedBy = null;
                        }
                        else
                        {
                            question.ModifiedBy = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            question.ModifiedDate = null;
                        }
                        else
                        {
                            question.ModifiedDate = reader.GetDateTime(8);
                        }

                        questions.Add(question);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions with no region
        /// </summary>
        /// <returns>All questions with no region</returns>
        public static List<Question> RetrieveQuestionsWithNoRegion()
        {
            List<Question> questions = new List<Question>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestionsWithNoRegion";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question()
                        {
                            QuestionID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(3),
                            CreatedBy = reader.GetInt32(5),
                            CreatedDate = reader.GetDateTime(6)
                        };

                        if (reader.IsDBNull(2))
                        {
                            question.Category = null;
                        }
                        else
                        {
                            question.Category = reader.GetString(2);
                        }

                        if (reader.IsDBNull(4))
                        {
                            question.RegionID = null;
                        }
                        else
                        {
                            question.RegionID = (short)reader.GetInt32(4);
                        }

                        if (reader.IsDBNull(7))
                        {
                            question.ModifiedBy = null;
                        }
                        else
                        {
                            question.ModifiedBy = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            question.ModifiedDate = null;
                        }
                        else
                        {
                            question.ModifiedDate = reader.GetDateTime(8);
                        }

                        questions.Add(question);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all questions
        /// </summary>
        /// <returns>All questions</returns>
        public static List<Question> RetrieveQuestions()
        {
            List<Question> questions = new List<Question>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectQuestions";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question()
                        {
                            QuestionID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(3),
                            CreatedBy = reader.GetInt32(5),
                            CreatedDate = reader.GetDateTime(6)
                        };

                        if (reader.IsDBNull(2))
                        {
                            question.Category = null;
                        }
                        else
                        {
                            question.Category = reader.GetString(2);
                        }

                        if (reader.IsDBNull(4))
                        {
                            question.RegionID = null;
                        }
                        else
                        {
                            question.RegionID = (short)reader.GetInt32(4);
                        }

                        if (reader.IsDBNull(7))
                        {
                            question.ModifiedBy = null;
                        }
                        else
                        {
                            question.ModifiedBy = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8))
                        {
                            question.ModifiedDate = null;
                        }
                        else
                        {
                            question.ModifiedDate = reader.GetDateTime(8);
                        }

                        questions.Add(question);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Create a question to the database
        /// </summary>
        /// <param name="question">Question to be inserted</param>
        /// <returns>True if the question was created successfully</returns>
        public static bool CreateQuestion(Question question)
        {
            bool inserted = false;

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spInsertQuestion";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", question.Title);
            cmd.Parameters.AddWithValue("@Content", question.Content);
            cmd.Parameters.AddWithValue("@CreatedBy", question.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", question.CreatedDate);

            if(question.RegionID == null)
            {
                cmd.Parameters.AddWithValue("@RegionID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegionID", question.RegionID);
            }

            if (question.Category == null)
            {
                cmd.Parameters.AddWithValue("@Category", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Category", question.Category);
            }

            if (question.ModifiedBy == null)
            {
                cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ModifiedBy", question.ModifiedBy);
            }

            if (question.ModifiedDate == null)
            {
                cmd.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ModifiedDate", question.ModifiedDate);
            }

            try
            {
                conn.Open();

                if(cmd.ExecuteNonQuery() == 1)
                {
                    inserted = true;
                }
            }
            catch(Exception)
            {
                throw;
            }
            return inserted;
        }
    }
}
