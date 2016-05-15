using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class GroupRequest
    {
        // Added by Poonam Dubey on 02/27/2016
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public char RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }


        public GroupRequest() { }
        public GroupRequest(int groupID,
                             int userID,
                             char requestStatus,
                             DateTime requestDate,
                             int requestedBy,
                             DateTime approvedDate,
                             int approvedBy)
        {
            GroupID = groupID;
            UserID = userID;
            RequestStatus = requestStatus;
            RequestDate = requestDate;
            ApprovedDate = approvedDate;
            ApprovedBy = approvedBy;
        }
    }
}
