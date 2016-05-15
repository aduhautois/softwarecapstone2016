using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObjects;


namespace com.GreenThumb.DataAccess
{
    public class VolunteerAccessor
    {
        
       
       

        
  
         
            public static bool CreateVolunteer(Volunteer volunteer)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Gardens.spInsertVolunteers";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", volunteer.UserID);
            cmd.Parameters.AddWithValue("@NeedID", volunteer.NeedID);
            cmd.Parameters.AddWithValue("@DateWIllVolunteer", volunteer.DateWillVolunteer);
            cmd.Parameters.AddWithValue("@Description", volunteer.Description);
           

            bool updated = false;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    updated = true;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return updated;
        }

       
    }
}

    
