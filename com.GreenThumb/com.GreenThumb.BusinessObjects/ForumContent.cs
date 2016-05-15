using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class ForumContent
    {

        public int MessageID { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDate { get; set; }
        public string Subject { get; set; }
        public int MessageSender { get; set; }
        public bool Active { get; set; }

        public ForumContent(int MessageID, string MessageContent, DateTime MessageDate, string Subject,
                             int MessageSender, bool Active)
        { 

            this.MessageID = MessageID;
            this.MessageContent = MessageContent;
            this.MessageDate = MessageDate;
            this.Subject = Subject;
            this.MessageSender = MessageSender;
            this.Active = Active;


        }
        
    }
}
