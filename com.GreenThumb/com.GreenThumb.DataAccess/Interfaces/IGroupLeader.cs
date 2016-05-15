///<summary>
///Author: Nasr Mohammed
///Group Leader Interface for the Buisness Object Layer
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess.Interfaces
{
    public interface IGroupLeader
    {
        ///<summary>
        ///Set up a garden
        ///@returns:  
        ///</summary> 
        void SetUpGarden(Garden gardenID, User user);

        ///<summary>
        ///Insert a garden in a garden table
        ///@returns: rows affected 
        ///</summary>
        int CreateGarden(Garden gardenID);

        ///<summary>
        ///Delete a garden from group a garden table
        ///@returns: rows affected 
        ///</summary>
        int DeleteGarden(Garden gardenID);

        ///<summary>
        ///Set up a member
        ///@returns:  
        ///</summary> 
        void SetUpMembers(User memberID, User user);


        ///<summary>
        ///Approve a design
        ///@returns: True if it's approved, false otherwise
        ///</summary>
        bool ApproveDesign(User user);

        ///<summary>
        ///Assigin a task to a group expert
        ///@returns:  
        ///</summary> 
        void AssignsTasksToGroupExperts(User expertID);


    }
}
















