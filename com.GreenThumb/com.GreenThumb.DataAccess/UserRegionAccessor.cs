using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObject;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess
{
    public class UserRegionAccessor
    {
        public static List<User> RetrieveUserList(Active group = Active.active)
        {
            // create a list to hold the returned data
            var userList = new List<User>();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a command object
            var cmdText = "Admin.spSelectUsers";
            Console.WriteLine(cmdText);
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Active", Active.active);


            //    var cmd = new SqlCommand(query, conn);

            // be safe, not sorry! use a try-catch
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

                        User currentUser = new User()
                        {
                            UserID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Zip = reader.GetString(3),
                            EmailAddress = reader.GetString(4),
                            UserName = reader.GetString(5),
                            //         Password = reader.GetString(6),
                            Active = reader.GetBoolean(7)
                            //        

                        };
                        var region = reader.GetValue(8);
                        currentUser.RegionId = DBNull.Value.Equals(region) ? -1 : (Int32)region;
                        userList.Add(currentUser);

                    }
                }

            }
            catch (Exception)
            {
                // rethrow all Exceptions, let the logic layer sort them out
                Console.WriteLine("iS THERE any failure here : ");

                throw;
            }
            finally
            {
                conn.Close();
            }
            // this list may be empty, if so, the logic layer will need to deal with it
            return userList;
        }

        public static int RetrieveUserCount(Active group = Active.active)
        {
            int count = 0;

            // let's try a scalar query

            // start with a connection object
            var conn = DBConnection.GetDBConnection();

            // write some command text
            string query = @"SELECT COUNT(*) " +
                           @"FROM Admin.Users ";

            // include our WHERE logic
            if (group == Active.active)
            {
                query += @"WHERE Active = 1 ";
            }
            else if (group == Active.inactive)
            {
                query += @"WHERE Active = 0 ";
            }

            // create a command object
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

        public static User RetrieveUser(int userID)
        {
            // create a list to hold the returned data
        //    var userList = new User();

            // get a connection to the database
            var conn = DBConnection.GetDBConnection();

            // create a command object
            var cmdText = "Admin.spSelectUser";
            Console.WriteLine(cmdText);
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);


            //    var cmd = new SqlCommand(query, conn);

            // be safe, not sorry! use a try-catch
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

                    reader.Read();
                   
                        User currentUser = new User()
                        {
                            UserID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Zip = reader.GetString(3),
                            EmailAddress = reader.GetString(4),
                            UserName = reader.GetString(5),
                            //         Password = reader.GetString(6),
                            Active = reader.GetBoolean(7)
                            //        

                        };
                        var region = reader.GetValue(8);
                        currentUser.RegionId = DBNull.Value.Equals(region) ? -1 : (Int32)region;
                        return currentUser;

                    
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                // rethrow all Exceptions, let the logic layer sort them out
                Console.WriteLine("iS THERE any failure here : ");

                throw;
            }
            finally
            {
                conn.Close();
            }
            
           
        }

  /*      public static int InsertUser(User usr)
        {
            int count = 0;

            // We First make the connection
            var conn = DBConnection.GetDBConnection();

            // get a command object

            var cmdText = "Admin.spInsertUsers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //       cmd.Parameters.AddWithValue("@UserID", usr.UserID);
            cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
            cmd.Parameters.AddWithValue("@LastName", usr.LastName);
            cmd.Parameters.AddWithValue("@Zip", usr.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", usr.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", usr.UserName);
            cmd.Parameters.AddWithValue("@PassWord", usr.Password);
            cmd.Parameters.AddWithValue("@Active", usr.Active);
            cmd.Parameters.AddWithValue("@RegionID", Convert.DBNull);

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
        } */

        public static int UpdateUser(int userID, int regionID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

         //   var cmdText = "Admin.spUpdateUser";
            var query = "UPDATE Admin.Users SET RegionID = @RegionID WHERE UserID = @UserID";
            var cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserID", userID);
        /*    cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
            cmd.Parameters.AddWithValue("@LastName", usr.LastName);
            cmd.Parameters.AddWithValue("@Zip", usr.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", usr.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", usr.UserName);
            cmd.Parameters.AddWithValue("@PassWord", usr.PassWord);
            cmd.Parameters.AddWithValue("@Active", usr.Active); */
            cmd.Parameters.AddWithValue("@RegionID", regionID);
            
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }
    }
}
