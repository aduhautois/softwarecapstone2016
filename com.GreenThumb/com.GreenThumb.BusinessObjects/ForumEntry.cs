using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class ForumEntry
    {

        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public DateTime DateSent { get; set; }
        public int ReadBy { get; set; }
        public DateTime DateRead { get; set; }
        public string MessageContent { get; set; }

        public ForumEntry(int MessageID, int SenderID, DateTime DateSent, int ReadBy, DateTime DateRead,
                           string MessageContent)
        {

            this.MessageID = MessageID;
            this.MessageContent = MessageContent;
            this.SenderID = SenderID;
            this.DateSent = DateSent;
            this.ReadBy = ReadBy;
            this.DateRead = DateRead;
            

        }

    }
}
