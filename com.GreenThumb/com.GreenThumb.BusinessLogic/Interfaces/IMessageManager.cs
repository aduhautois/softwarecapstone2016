using System;
using System.Collections.Generic;
namespace com.GreenThumb.BusinessLogic.Interfaces
{
    /// <summary>
    /// Ryan Taylor
    /// Created: 4/14/2016
    /// </summary>
    public interface IMessageManager
    {
        System.Collections.Generic.List<com.GreenThumb.BusinessObjects.Message> GetUserMessages(string Username);
        bool EditMessageDeletedReceiver(string Username, int MessageID);
        bool EditMessageDeletedSender(string Username, int MessageID);
        bool EditMessageRead(string Username, int MessageID);
        bool SendMessage(string MessageContent, string Subject, string SenderUsername, string ReceiverUsername);
        List<com.GreenThumb.BusinessObjects.User> GetUserNames();
    }
}
