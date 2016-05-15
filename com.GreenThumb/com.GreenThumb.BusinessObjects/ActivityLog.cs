using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class ActivityLog
    {
        /// <summary>
        /// Added by Poonam Dubey on 02/27/2016
        /// </summary>

        public int ActivityLogID { get; set; }
        public int UserID { get; set; }
        public DateTime ActivityDate { get; set; }
        public string LogEntry { get; set; }
        public string UserAction { get; set; }

        public ActivityLog() { }

        public ActivityLog(int activityLogID,
                           int userID,
                           DateTime activityDate,
                           string logEntry,
                           string userAction)
        {
            ActivityLogID = activityLogID;
            UserID = userID;
            ActivityDate = activityDate;
            LogEntry = logEntry;
            UserAction = userAction;
        }
    }
}
