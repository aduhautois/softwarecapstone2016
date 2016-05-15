/// <summary>
/// Ryan Taylor
/// Created: 2016/02/27
/// </summary>
/// <remarks>
/// Updated by Ryan Taylor 2016/03/03
/// Added Active property
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Role
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Basic Data Transfer Object to represent Role for Users
        /// from the Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public string RoleID { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public Role(string roleID, string description, int createdBy, DateTime createdDate)
        {
            this.RoleID = roleID;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
        }

        public Role()
        {
        }
    }
}
