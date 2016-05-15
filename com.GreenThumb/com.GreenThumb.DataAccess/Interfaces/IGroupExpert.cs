///<summary>
///Author: Nasr Mohammed
///Group Expert Interface for the Buisness Object Layer
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess.Interfaces
{
    public interface IGroupExpert
    {
        ///<summary>
        ///Add task to taks table
        ///@returns: rows affected 
        ///</summary> 
        int ReceiveTasks(Job task);

        ///<summary>
        ///Insert DATA into task tabe
        ///@returns: rows affected 
        ///</summary>
        int InputDataForTask(Job task);

        ///<summary>
        ///Design a garden
        ///@returns: rows affected 
        ///</summary> a garden
        Garden DesignGarden(Garden gardenID);

        ///<summary>
        ///Fetech a list of notification
        ///@returns: rows affected 
        ///</summary> a notification
        List<Notification> ReceiveNotification(string notice);

        /////<summary>
        /////record the volunter hours
        /////@returns: True if it's recorded, false otherwise
        /////</summary>
        //bool RecordsHours(VolunteerHours hour);

    }
}
















