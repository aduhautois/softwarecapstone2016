using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using com.GreenThumb.BusinessObject;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Author name?
    /// Created: 03/01/2016
    /// This class adds an administrator to the database.
    /// </summary>
    class AdminAccessor
    {

        /// <summary>
        /// Author name?
        /// Created: 03/01/2016
        /// This method contains the SQL for adding an administrator to the database.
        /// </summary>

        public static int CreateAdmin(com.GreenThumb.BusinessObject.AdministratorAccount admin)
        {
            int count = 0;


            var conn = DBConnection.GetDBConnection();

            string query = @"INSERT INTO User " +
                           @"(FirstName, LastName, LocalPhone, " +
                           @"EmailAddress, UserName, Password) " +
                           @"VALUES " +
                           @"('" + admin.FirstName + "', '" + admin.LastName +
                           @"', '" + admin.Zip + "', '" + admin.EmailAddress +
                           @"', '" + admin.UserName + "', '" + admin.Password + "') ";

            // get a command object
            var cmd = new SqlCommand(query, conn);

            try
            {
                // open the connection
                conn.Open();

                // execute the command with ExecuteNonQuery()
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }


    }
}
