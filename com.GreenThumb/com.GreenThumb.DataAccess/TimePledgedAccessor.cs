using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObjects;
using System.Data;

namespace com.GreenThumb.DataAccess
{
    //<summary>
    //Emily West
    //Access time pledged stored procedures
    //</summary>

    public class TimePledgedAccessor
    {
        
        public static int PledgeVolunteerHours(DateTime start, DateTime end, DateTime date, int UserID)
       {
           int rowCount = 0;
           var conn = DBConnection.GetDBConnection();
           var query = "Donations.spInsertTimePledge";
           var cmd = new SqlCommand(query, conn);

           cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
           cmd.Parameters.AddWithValue("@StartTime", start);
           cmd.Parameters.AddWithValue("@FinishTime", end);
          cmd.Parameters.AddWithValue("@Date",date);
           cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
           cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

           try
           {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
           }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Selection!");
            }
            finally
            {
               conn.Close();
            }

            return rowCount;
        }
    }
}
