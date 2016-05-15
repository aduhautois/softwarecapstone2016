using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Message
    {
        /// <summary>
        /// Added by Poonam Dubey on 02/27/2016
        /// Changed by Trevor 04/14/16 To reflect Changes to DB Table
        /// </summary>

        public int MessageID { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDate { get; set; }
        //public TimeSpan MessageTime { get; set; }
        public string MessageSubject { get; set; }
        public string MessageReceiver { get; set; }
        public bool Unread { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecieverDeleted { get; set; }
        public string MessageSender { get; set; }
        public bool Active { get; set; }


        public Message() { }

        public Message(int messageID,
                        string messageContent,
                        DateTime messageDate,
                        string messageSubject,
                        string messageSender,
                        bool active)
        {
            MessageID = messageID;
            MessageContent = messageContent;
            MessageDate = messageDate;
            MessageSubject = messageSubject;
            MessageSender = messageSender;
            Active = active;
        }
    }
}
