using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Added by Sara Nanke on 03/04/2016
    /// This Class manages message objects to send to the view
    /// </summary>
    public class MessageManager : com.GreenThumb.BusinessLogic.Interfaces.IMessageManager
    {
        /// <summary>
        /// Added by Sara Nanke on 03/04/2016
        /// This method retrieves messages
        /// </summary>
        //public List<Message> GetUserMessages()
        //{
        //    List<Message> messages = new List<Message>();
        //    messages = MessageAccessor.RetrieveAdminMessages();

        //    return messages;
        //}

        /// <summary>
        /// ADDED BY Trevor 4/14/16
        /// Fixed to use current message table and stored procedure
        /// Gets Messages by Usersname
        /// </summary>
        /// <param name="Username"></param>
        /// <returns> A list of a specific users messages </returns>
        public List<Message> GetUserMessages(string Username)
        {
            var Messages = new List<Message>();
            try
            {
                Messages = MessageAccessor.RetrieveMessages(Username);
                return Messages;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// ADDED BY Trevor 2016/14/04
        /// Sends message to another user
        /// </summary>
        /// <param name="MessageContent"></param>
        /// <param name="Subject"></param>
        /// <param name="SenderUsername"></param>
        /// <param name="ReceiverUsername"></param>
        /// <returns> True or False based on success of message send</returns>
        public bool SendMessage(string MessageContent, string Subject, string SenderUsername, string ReceiverUsername)
        {
            bool result;
            try
            {
                result = MessageAccessor.CreateMessage(MessageContent, Subject, SenderUsername, ReceiverUsername);
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// ADDED BY Trevor 04/14/16
        /// Marks a message read when a user looks at it
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="MessageID"></param>
        /// <returns> Returns true or false based on success of marking a messsage read</returns>
        public bool EditMessageRead(string Username, int MessageID)
        {
            bool result;
            try
            {
                result = MessageAccessor.UpdateMessageRead(Username, MessageID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// ADDED BY Trevor 04/14/16
        /// Deletes a message from a users Outbox
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="MessageID"></param>
        /// <returns>Returns True or false based on success of deleting message from users outbox</returns>

        public bool EditMessageDeletedSender(string Username, int MessageID)
        {
            bool result;
            try
            {
                result = MessageAccessor.UpdateMessageDeletedSender(Username, MessageID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// ADDED BY Trevor 04/14/16
        /// Deletes a message from a users inbox
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="MessageID"></param>
        /// <returns>Retruns true or false based on success of deleting message from users inbox</returns>
        public bool EditMessageDeletedReceiver(string Username, int MessageID)
        {
            bool result;
            try
            {
                result = MessageAccessor.UpdateMessageDeletedReceiver(Username, MessageID);
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// ADDED BY Trevor Glisch
        /// Method to return the number of new messages when a user logs in
        /// </summary>
        /// <param name="Username"></param>
        /// <returns> Returns an count of the messages</returns>
        public int GetUnreadMessageCount(string Username)
        {
            int count;
            List<Message> Messages = GetUserMessages(Username);
            Messages = Messages.Where(s => s.Unread == true).ToList();
            count = Messages.Count;

            return count;

        }

        public List<User> GetUserNames()
        {
            try
            {
                return MessageAccessor.RetrieveUserNames();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
