using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class UserRole
    {
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
        public int UserID { get; set; }
        public string RoleID { get; set; }
       
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Active { get; set; }

        public UserRole()
        {

        }

        public UserRole(int userID, string roleID)
        {
            this.UserID = userID;
            this.RoleID = roleID;
        }
        public UserRole (int userID,
                      string roleID,       
                         int? createdBy,
                         DateTime? createdDate)
        {
            UserID = userID;
            RoleID = roleID;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
        }
    }
}
