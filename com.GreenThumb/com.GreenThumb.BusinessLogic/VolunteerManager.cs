using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
namespace com.GreenThumb.BusinessLogic
{
    public class VolunteerManager
    {


        


        ///<summary>
        ///Author: Teresa Determann         
        ///AddVolunteer adds a volunteer 
        //calling to the VolunteerAccessor
        ///Date: 3/24/16
        ///</summary>
        public bool AddVolunteer(Volunteer newVolunteer)
        {
            try
            {
                bool myBool = VolunteerAccessor.CreateVolunteer(newVolunteer);
                return myBool;
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}