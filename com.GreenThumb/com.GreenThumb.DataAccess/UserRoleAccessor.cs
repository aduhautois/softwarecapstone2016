using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccessor
{
    public class UserRoleAccessor
    {
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/25 By Ibarahim
        /// </summary>
        public static List<UserRole> RetrieveUserRoleList()
        {
            // create a list to hold the returned data
            var userRoleList = new List<UserRole>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection

            /*         string query = @"SELECT UserID, RoleID, CreatedBy, CreatedDate " +
                                    @"FROM Admin.UserRoles ";
   
                     query += @"ORDER BY UserID, RoleID "; */
            string cmdText = "Admin.spSelectUserRole";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            

            // Try catch block to deal with the data
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
                        UserRole currentUserRole = new UserRole()
                        {
                            UserID = reader.GetInt32(0),
                            RoleID = reader.GetString(1),
                            CreatedBy = reader.GetInt32(2),
                            CreatedDate = reader.GetDateTime(3),
                            Active = reader.GetBoolean(4)
                        };

                        userRoleList.Add(currentUserRole);
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
            return userRoleList;
        }
        /// <summary>
        /// 
        /// method to retrieve user roles by passing userID
        /// 
        /// 
        /// Added 4-15-16 By Ibarahim Abuzaid
        /// </summary>
        public static List<UserRole> RetrieveUserRoleListByUser(int userID)
        {
            // create a list to hold the returned data
            var userRoleList = new List<UserRole>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a query to send through the connection

            string cmdText = "Admin.spSelectUserRoleByUser";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);

            // Try catch block to deal with the data
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
                        UserRole currentUserRole = new UserRole()
                        {
                            UserID = reader.GetInt32(0),
                            RoleID = reader.GetString(1),
                            Active = reader.GetBoolean(4)
                        };
                        if (reader.IsDBNull(2))
                        {
                            currentUserRole.CreatedBy = null;
                        }
                        else
                        {
                            currentUserRole.CreatedBy = reader.GetInt32(2);
                        }
                        if (reader.IsDBNull(3))
                        {
                            currentUserRole.CreatedDate = null;
                        }
                        else
                        {
                            currentUserRole.CreatedDate = reader.GetDateTime(3);
                        }
                        userRoleList.Add(currentUserRole);
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
            return userRoleList;
        }

        public static int RetrieveUserRoleCount()
        {
            int count = 0;

            // let's try a scalar query

            // start with a connection object
            var conn = DBConnection.GetDBConnection();

            // prepare the querry command
            string query = @"SELECT COUNT(*) " +
                           @"FROM Admin.UserRoles ";

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

        public static int InsertUserRole(UserRole userRole)
        {
            int count = 0;

            // prepare the data connection
            var conn = DBConnection.GetDBConnection();

            // What comes next is a command text
            /*    string query = @"INSERT INTO Admin.UserRoles " +
                               @"(UserID, RoleID, CreatedBy, CreatedDate " +
                               @"VALUES " +
                               @"('" + userRole.UserID + "', '" + userRole.RoleID + "', '" + 
                               @"', '" + userRole.CreatedBy + "', '" + userRole.CreatedDate + "') ";*/
            var cmdText = "Admin.spInsertUserRoles";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // get a command object
            cmd.Parameters.AddWithValue("@UserID", userRole.UserID);
            cmd.Parameters.AddWithValue("@RoleID", userRole.RoleID);
            

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

        // should not be used after approval by
        public static int UpdateUserRole(UserRole userRole)
        {
            int rowCount = 0;

            // begin with a connection
            var conn = DBConnection.GetDBConnection();

            // get some commandText
            string cmdText = "Admin.spUpdateUserRoles";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // here is where things change a bit
            // first, we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to construct and add the parameters

            // this is the all-at-once way
            cmd.Parameters.AddWithValue("@UserID", userRole.UserID);
            cmd.Parameters.AddWithValue("@RoleID", userRole.RoleID);
            cmd.Parameters.AddWithValue("@CreatedBy", userRole.CreatedBy );
            cmd.Parameters.AddWithValue("@CreatedDate", userRole.CreatedDate);

            // we can also create an output parameter
            var o = new SqlParameter("Rowcount", SqlDbType.Int);
            o.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(o);

            try
            {
                // open the connection
                conn.Open();

                // execute  the command
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

        public static int UpdateUserRoleRemove(int usr, String role)
        {
            int rowCount = 0;

            // begin with a connection
            var conn = DBConnection.GetDBConnection();

            // get some commandText
            string cmdText = "Admin.spUpdateUserRoleRemove";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // here is where things change a bit
            // first, we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to construct and add the parameters

            // this is the all-at-once way
            cmd.Parameters.AddWithValue("@UserID", usr);
            cmd.Parameters.AddWithValue("@RoleID", role);

            // we can also create an output parameter
            var o = new SqlParameter("Rowcount", SqlDbType.Int);
            o.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(o);

            try
            {
                // open the connection
                conn.Open();

                // execute  the command
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
    

          public static int UpdateUserRoleStatus(int usr, String role, bool active)
        {
            int rowCount = 0;

            // begin with a connection
            var conn = DBConnection.GetDBConnection();

            // get some commandText
        //    string cmdText = "Admin.spUpdateUserRoleStatus"; //waiting for Chris approval
        //    var query = "UPDATE Admin.UserRoles SET Active = @Active WHERE " +
        //                @"UserID = @UserID AND RoleID = @RoleID";
            var cmdText = "Admin.spUpdateUserRoleActive";
            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // here is where things change a bit
            // first, we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // we need to construct and add the parameters

            // this is the all-at-once way
            cmd.Parameters.AddWithValue("@UserID", usr);
            cmd.Parameters.AddWithValue("@RoleID", role);
            cmd.Parameters.AddWithValue("@Active", active);

            // we can also create an output parameter
            var o = new SqlParameter("Rowcount", SqlDbType.Int);
            o.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(o);

            try
            {
                // open the connection
                conn.Open();

                // execute  the command
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
