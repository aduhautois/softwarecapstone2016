using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Create By: Kristine Johnson 3/20/16
    /// <paramref name="org" references the organization that we are inserting/>
    /// <paramref name="groupLeaderID" references the groupleader creating the org/>
    /// </summary>
    public class CreateOrgAccessor
    {

        /// <summary>
        /// These comments added by TRex
        /// This method allows a group leader to add an organization
        /// </summary>
        /// <param name="org"></param>
        /// <param name="groupLeaderID"></param>
        /// <returns> rowCount </returns>
        public static int InsertOrganization(Organization org, int groupLeaderID)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // we need a command object (the command text is in the stored procedure)
            var cmd = new SqlCommand("Gardens.spInsertOrganizations", conn);

            // set the command type for stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrganizationName", org.Name);
            cmd.Parameters.AddWithValue("@OrganizationLeader", groupLeaderID);
            cmd.Parameters.AddWithValue("@ContactPhone", org.ContactPhone);



            cmd.Parameters.Add(new SqlParameter("@RowCount", SqlDbType.Int));
            cmd.Parameters["@RowCount"].Direction = ParameterDirection.ReturnValue;

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }
        
    }
}
