using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Comments added by TRex 4/19/16
    /// This class allows volunteer hours to be pledged.
    /// </summary>
   public class DonationManager
    {
        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method adds volunteer hours.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="finishTime"></param>
        /// <param name="datePledged"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public int AddVolunteerHours(DateTime startTime, DateTime finishTime, DateTime datePledged, int p)
        {
            try
           {
                int timePledge = TimePledgedAccessor.PledgeVolunteerHours(startTime, finishTime, datePledged, p);
                return timePledge;
           }
            catch (Exception)
           {

                throw;
           }
        }

       
    }
}
