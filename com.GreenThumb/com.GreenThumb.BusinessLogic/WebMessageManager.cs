using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    public class WebMessageManager : IMessageManager
    {
        public List<Message> GetUserMessages(string Username)
        {
            var messages = new List<Message>();
            try
            {
                messages = MessageAccessor.RetrieveMessages(Username);
            }
            catch (Exception) { }
            return messages;
        }

        public bool EditMessageDeletedReceiver(string Username, int MessageID)
        {
            bool result;
            try
            {
                result = MessageAccessor.UpdateMessageDeletedReceiver(Username, MessageID);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool EditMessageDeletedSender(string Username, int MessageID)
        {
            bool result;
            try
            {
                result = MessageAccessor.UpdateMessageDeletedSender(Username, MessageID);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool EditMessageRead(string Username, int MessageID)
        {
            bool result;
            try
            {
                result = MessageAccessor.UpdateMessageRead(Username, MessageID);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool SendMessage(string MessageContent, string Subject, string SenderUsername, string ReceiverUsername)
        {
            bool result;
            try
            {
                result = MessageAccessor.CreateMessage(MessageContent, Subject, SenderUsername, ReceiverUsername);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public List<User> GetUserNames()
        {
            List<User> userList = null;
            try
            {
                userList = MessageAccessor.RetrieveUserNames();
            }
            catch (Exception) { }
            return userList;
        }

        public int GetUnreadMessageCount(string Username)
        {
            int count;
            List<Message> Messages = GetUserMessages(Username);
            Messages = Messages.Where(s => s.Unread == true).ToList();
            count = Messages.Count;
            return count;
        }
    }
}
