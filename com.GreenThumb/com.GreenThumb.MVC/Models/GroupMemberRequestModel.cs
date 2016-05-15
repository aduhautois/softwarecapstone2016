using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// Created by Nicholas King
    /// </summary>
    public class GroupMemberRequestModel
    {
        public User Requestor { get; set; }
        public GroupRequest Request { get; set; }

    }
}