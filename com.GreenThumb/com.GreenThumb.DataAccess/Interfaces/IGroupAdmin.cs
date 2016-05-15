///<summary>
///Author: Nasr Mohammed
///Group Admin Interface for the Buisness Object Layer
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess.Interfaces
{
    public interface IGroupAdmin
    {
        ///<summary>
        ///Add a leader to group leader table
        ///@returns: rows affected 
        ///</summary>
        int AddGroupLeader(int groupLeader);

        ///<summary>
        ///Delete a leader from group leader table
        ///@returns: rows affected 
        ///</summary>
        int DeleteGroupLeader(int groupLeader);

        ///<summary>
        ///Approve the request
        ///@returns: True if it's approved, false otherwise
        ///</summary>
        bool ApproveRequest(User user);


    }
}