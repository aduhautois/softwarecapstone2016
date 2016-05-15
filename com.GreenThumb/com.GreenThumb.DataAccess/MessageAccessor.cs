using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Added by Sara Nanke on 03/04/2016
    /// This class contains the SQL for the message object.
    /// </summary>
    public class MessageAccessor
    {
        /// <summary>
        /// Added by Sara Nanke on 03/04/2016
        /// This method contains the SQL for retrieving a message from the database.
        /// </summary>
        public static List<Message> fetchAdminMessages()
        {
            var messages = new List<Message>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spDisplayMessages";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Message message = new Message()
                    {
                        //MessageID, MessageContent, MessageDate, Subject, MessageSender, Active
                        MessageID = reader.GetInt32(0),
                        MessageContent = reader.GetString(1),
                        MessageDate = reader.GetDateTime(2),
                        MessageSubject = reader.GetString(3),
                        MessageSender = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };

                    messages.Add(message);
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception)
            {
                //there are no admin messages, do nothing
            }
            finally
            {
                conn.Close();
            }

            return messages;
        }
        /// <summary>
        /// Added by Trevor 04/14/16
        /// Method to get a list of a users messages using their username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>A list of Messages</returns>
        public static List<Message> RetrieveMessages(string Username)
        {
            var messages = new List<Message>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectMessage";
            var cmd = new SqlCommand(query, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Username", Username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Message message = new Message()
                        {
                            //MessageID, MessageContent, MessageDate, Subject, MessageSender, Active
                            MessageID = reader.GetInt32(0),
                            MessageContent = reader.GetString(1),
                            MessageDate = reader.GetDateTime(2),
                            MessageSubject = reader.GetString(3),
                            MessageSender = reader.GetString(4),
                            MessageReceiver = reader.GetString(5),
                            Unread = reader.GetBoolean(6),
                            SenderDeleted = reader.GetBoolean(7),
                            RecieverDeleted = reader.GetBoolean(8)

                        };

                        messages.Add(message);
                    }

                }
                else
                {
                    throw new ApplicationException("No Messages for user");
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Could not retreive messages for user at this time.");
            }
            finally
            {
                conn.Close();
            }

            return messages;
        }
        /// <summary>
        /// Added by Trevor 04/14/16
        /// Method to mark messages as read. Takes in username and MessageID
        /// Returns true or false wether or not it succeded
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="MessageID"></param>
        /// <returns>Boolean based on result</returns>
        public static bool UpdateMessageRead(string Username, int MessageID)
        {
            int result;
            bool succeded;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spMarkMessageRead";
            var cmd = new SqlCommand(query, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("MessageID", MessageID);
            cmd.Parameters.AddWithValue("Username", Username);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    succeded = true;

                }
                else
                {
                    succeded = false;
                }

            }
            catch (Exception)
            {
                throw new ApplicationException("Problem Occured Marking Message Read. Please Try Again.");
            }
            finally
            {
                conn.Close();
            }

            return succeded;
        }
        /// <summary>
        /// Added by Trevor 04/14/16
        /// Method to Mark a Messsage Deleted from the sent box on the app
        /// Takes the username and message id and returns true of false based on success
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="MessageID"></param>
        /// <returns>Boolean based on result</returns>
        public static bool UpdateMessageDeletedSender(string Username, int MessageID)
        {
            int result;
            bool succeded;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spMarkMessageSenderDeleted";
            var cmd = new SqlCommand(query, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("MessageID", MessageID);
            cmd.Parameters.AddWithValue("Username", Username);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    succeded = true;

                }
                else
                {
                    succeded = false;
                }

            }
            catch (Exception)
            {
                throw new ApplicationException("Message Could Not Be Deleted");
            }
            finally
            {
                conn.Close();
            }

            return succeded;
        }
        /// <summary>
        /// Added by Trevor 04/14/16
        /// Method to Mark a Messsage Deleted from the Inbox on the app
        /// Takes the username and message id and returns true of false based on success
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="MessageID"></param>
        /// <returns> Boolean based on result</returns>
        public static bool UpdateMessageDeletedReceiver(string Username, int MessageID)
        {
            int result;
            bool succeded;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spMarkMessageReceiverDeleted";
            var cmd = new SqlCommand(query, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("MessageID", MessageID);
            cmd.Parameters.AddWithValue("Username", Username);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    succeded = true;

                }
                else
                {
                    succeded = false;
                }

            }
            catch (Exception)
            {
                throw new ApplicationException("Message Could Not Be Deleted");
            }
            finally
            {
                conn.Close();
            }

            return succeded;
        }
        /// <summary>
        /// Added by Trevor 04/14/16
        /// Method to send a message to another user.
        /// Takes the Message Content, Subject, Sender and Receiver Username
        /// </summary>
        /// <param name="MessageContent"></param>
        /// <param name="Subject"></param>
        /// <param name="SenderUsername"></param>
        /// <param name="ReceiverUsername"></param>
        /// <returns>Returns boolean base of result of sending</returns>

        public static bool CreateMessage(string MessageContent, string Subject, string SenderUsername, string ReceiverUsername)
        {
            int result;
            bool succeded;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spInsertMessage";
            var cmd = new SqlCommand(query, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("MessageContent", MessageContent);
            cmd.Parameters.AddWithValue("Subject", Subject);
            cmd.Parameters.AddWithValue("Sender", SenderUsername);
            cmd.Parameters.AddWithValue("Receiver", ReceiverUsername);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    succeded = true;

                }
                else
                {
                    succeded = false;
                }

            }
            catch (Exception)
            {
                throw new ApplicationException("Message Could Not Be Sent");
            }
            finally
            {
                conn.Close();
            }

            return succeded;
        }

        public static List<User> RetrieveUserNames()
        {
            List<User> userList = new List<User>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectAllUserNames";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User currentUser = new User()
                        {
                            UserName = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        };
                        userList.Add(currentUser);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found");
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

            return userList;
        }

        //public static List<Message> RetrieveAdminMessages()
        //{
        //    var messages = new List<Message>();
        //    var conn = DBConnection.GetDBConnection();
        //    var query = @"Admin.spDisplayMessages";
        //    var cmd = new SqlCommand(query, conn);

        //    cmd.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        conn.Open();
        //        var reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            reader.Read();
        //            Message message = new Message()
        //            {
        //                //MessageID, MessageContent, MessageDate, Subject, MessageSender, Active
        //                MessageID = reader.GetInt32(0),
        //                MessageContent = reader.GetString(1),
        //                MessageDate = reader.GetDateTime(2),
        //                MessageSubject = reader.GetString(3),
        //                MessageSender = reader.GetInt32(4),
        //                Active = reader.GetBoolean(5)
        //            };

        //            messages.Add(message);
        //        }
        //        else
        //        {
        //            throw new ApplicationException("Data not found");
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        //there are no admin messages, do nothing
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return messages;
        //}
    }
}
