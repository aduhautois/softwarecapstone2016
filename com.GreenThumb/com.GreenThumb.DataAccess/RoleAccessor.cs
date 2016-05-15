using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    public class RoleAccessor
    {
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// </summary>
        /// changed method name from FetchRoleList 4/21/16 Steve Hoover
        public static List<Role> RetrieveRoleList()
        {
            // create a list to hold the returned data
            var roleList = new List<Role>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection

            string query = @"SELECT RoleID, Description, CreatedBy, CreatedDate " +
                           @"FROM Admin.Roles ";
          
            query += @"ORDER BY RoleID ";

            // create a command object
            var cmd = new SqlCommand(query, conn);

            // always using try catch when dealing with data
            try
            {
                // open connection
                conn.Open();

                // execute the command and return a data reader
                SqlDataReader reader = cmd.ExecuteReader();

                // before trying to read the reader, be sure it has data
                if (reader.HasRows)
                {
                    // now we just need a loop to process the reader
                    while (reader.Read())
                    {
                        Role currentRole = new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1),
                            CreatedBy = reader.GetInt32(2),
                            CreatedDate = reader.GetDateTime(3)
                        };           
                        roleList.Add(currentRole);             
                    }
                }
            }
            catch (Exception)
            {
                // rethrow all Exceptions, let the logic layer sort them out
                throw;
            }
            finally
            {
                conn.Close();
            }
            // this list may be empty, if so, the logic layer will need to deal with it
            return roleList;
        }

        // changed method name from FetchRoleCount 4/21/16 Steve Hoover
        public static int RetrieveRoleCount()
        {
            int count = 0;

            // start with a connection object
            var conn = DBConnection.GetDBConnection();

            // write some command text
            string query = @"SELECT COUNT(*) " +
                           @"FROM Admin.Roles ";
            var cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }

            return count;
        }
        // changed method name from InsertRole 4/21/16 Steve Hoover
        public static int CreateRole(Role role)
        {
            int count = 0;

            // First Prepare the Connection
            var conn = DBConnection.GetDBConnection();

            // Then prepare a command text
            string query = @"INSERT INTO Admin.Roles " +
                           @"(RoleID, Description, CreatedBy, CreatedDate " +
                           @"VALUES " +
                           @"('" + role.RoleID + "', '" + role.Description +
                           @"', '" + role.CreatedBy + "', '" + role.CreatedDate + "') ";

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
