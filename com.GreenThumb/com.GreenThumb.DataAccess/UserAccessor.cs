/// <summary>
/// Ryan Taylor
/// Created: 2016/02/26
/// Data Access methods relating to User objects
/// </summary>
/// <remarks>
/// Updated by Ryan Taylor 2016/03/03
/// </remarks>

using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    public class UserAccessor
    {
        //Updated class name 4/14/16 Emily
        public static User RetrieveUserByUsername(string username)
        {
            User user;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUserByUsername";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int? regionID = 0;
                    string zip = "";
                    if (!reader.IsDBNull(4))
                    {
                        zip = reader.GetString(4);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        // Rhett Allen 3/24/16 - regionID is now being assigned
                        regionID = reader.GetInt32(6);
                    }
                    else
                    {
                        // Rhett Allen 3/24/16 - added else. Made region default null instead of zero since region is nullable
                        regionID = null;
                    }
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        Zip = zip,
                        EmailAddress = reader.GetString(5),
                        RegionId = regionID,
                        Active = reader.GetBoolean(7)
                    };
                }
                else
                {
                    throw new ApplicationException("Data not found");
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
            return user;
        }
        public static User RetrieveUser()
        {
            User user;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUsers";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int? regionID = 0;
                    string zip = "";
                    if (!reader.IsDBNull(4))
                    {
                        zip = reader.GetString(4);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        // Rhett Allen 3/24/16 - regionID is now being assigned
                        regionID = reader.GetInt32(6);
                    }
                    else
                    {
                        // Rhett Allen 3/24/16 - added else. Made region default null instead of zero since region is nullable
                        regionID = null;
                    }
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        Zip = zip,
                        EmailAddress = reader.GetString(5),
                        RegionId = regionID,
                        Active = reader.GetBoolean(7)
                    };
                }
                else
                {
                    throw new ApplicationException("Data not found");
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
            return user;
        }
        //Updated class name 4/14/16 Emily
        public static int RetrieveUserByUsernameAndPassword(string username, string password)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUserWithUsernameAndPassword";

            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
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
        //Updated class name 4/14/16 Emily
        public static int CreatePasswordForUsername(string username, string oldPassword, string newPassword)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spUpdatePassword";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
            cmd.Parameters.AddWithValue("@newPassword", newPassword);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteNonQuery();
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
        
        /// <summary>
        /// Function to insert new user by : Poonam Dubey 
        /// Dated : 16th March 2016
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// //Updated class name 4/14/16 Emily
        public static int CreateUser(User user)
        {
            int count = 0;

            // What comes first...a connection! Eureka!
            var conn = DBConnection.GetDBConnection();

            var query = @"Admin.spInsertUsers";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Zip", user.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RegionID", ((object)user.RegionId) ?? DBNull.Value);

            try
            {
                // open the connection
                conn.Open();

                // execute the command with ExecuteScalar()
                count = (int)cmd.ExecuteScalar();
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

        public static List<Role> RetrieveRolesByUserID(int userID)
        {
            var roles = new List<Role>();
            var conn = DBConnection.GetDBConnection();

            var query = @"Admin.spSelectRoles";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1),
                            Active = reader.GetBoolean(2)
                        });
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found.");
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

            return roles;
        }

        ///<summary>
        ///Author: Chris Schwebach
        ///UpdateUserPersonalInfo gets a database connection and updates information 
        ///in the DB where _accessToken.UserID = UserID 
        ///Date: 3/3/16
        ///Updated Date: 3/8/16
        ///Updated regionID data retrieval 
        ///</summary>
        public static int UpdateUserPersonalInfo(int userID, string firstName, string lastName, string zip, string emailAddress, int? regionId)
        {
            int rowCount = 0;

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // we need a command object (the command text is in the stored procedure)
            var cmd = new SqlCommand("Admin.spUpdateUserPersonalInfo", conn);

            // set the command type for stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Zip", zip);
            cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);

            if (regionId == null || regionId.Equals(""))
            {
                cmd.Parameters.AddWithValue("@regionID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@regionID", regionId);
            }

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
        /*
         * Removed by Steve Hoover - 4/21/15
         * References to this method should be changed to RetrieveUserList
         * 
        ///<summary>
        ///Author: Chris Schwebach
        ///FetchUserPersonalInfo gets a database connection and retrieves user personal information 
        ///information in the DB where _accessToken.UserID = UserID 
        ///Date: 3/3/16
        ///Updated Date: 3/8/16
        ///Updated regionID data retrieval 
        ///</summary>
        public static List<User> RetrievePersonalInfo(int userID)
        {

            var user = new List<User>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUserPersonalInfo";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User currentUser = new User()
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        Zip = reader.GetString(2),
                        EmailAddress = reader.GetString(3),
                        RegionId = ((reader["RegionID"] != DBNull.Value) ? Convert.ToInt32(reader.GetInt32(4)) : 0)

                    };
                    user.Add(currentUser);
                }
                else
                {
                    throw new ApplicationException("Data not found");
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
            return user;
        }

        */
        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Edits the data fields for a user object in the database
        /// </summary>
        /// <param name="updateUser">The user that includes all of the updated fields</param>
        /// <param name="originalUser">The original user object to be checked for concurrency</param>
        /// <returns>A boolean based on if the user has been updated successfully</returns>
        public static bool UpdateUserInformation(User updatedUser, User originalUser)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Admin.spUpdateUser";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", updatedUser.UserID);
            cmd.Parameters.AddWithValue("@FirstName", updatedUser.FirstName);
            cmd.Parameters.AddWithValue("@LastName", updatedUser.LastName);
            cmd.Parameters.AddWithValue("@Zip", updatedUser.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", updatedUser.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", updatedUser.UserName);
            cmd.Parameters.AddWithValue("@PassWord", updatedUser.Password);
            cmd.Parameters.AddWithValue("@Active", updatedUser.Active);
            cmd.Parameters.AddWithValue("@RegionID", updatedUser.RegionId);

            cmd.Parameters.AddWithValue("@OriginalFirstName", originalUser.FirstName);
            cmd.Parameters.AddWithValue("@OriginalLastName", originalUser.LastName);
            cmd.Parameters.AddWithValue("@OriginalZip", originalUser.Zip);
            cmd.Parameters.AddWithValue("@OriginalEmailAddress", originalUser.EmailAddress);
            cmd.Parameters.AddWithValue("@OriginalUserName", originalUser.UserName);
            cmd.Parameters.AddWithValue("@OriginalPassWord", originalUser.Password);
            cmd.Parameters.AddWithValue("@OriginalActive", originalUser.Active);
            cmd.Parameters.AddWithValue("@OriginalRegionID", originalUser.RegionId);

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
        /// <summary>
        /// Rhett Allen
        /// Created: 2016/02/26
        /// 
        /// Get a single user based on the id in the database
        /// </summary>
        /// <param name="userID">The UserID in the database</param>
        /// <returns>The specified plant object</returns>
        public static User RetrieveUserByID(int userID)
        {
            User user = new User();

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUser";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        UserName = reader.GetString(5),
                        Password = reader.GetString(6),
                        Active = reader.GetBoolean(7)
                    };

                    // Rhett Allen 3/6/16 - changed ExecuteReader to accept null values
                    if (reader.IsDBNull(3))
                    {
                        user.Zip = null;
                    }
                    else
                    {
                        user.Zip = reader.GetString(3);
                    }

                    if (reader.IsDBNull(4))
                    {
                        user.EmailAddress = null;
                    }
                    else
                    {
                        user.EmailAddress = reader.GetString(4);
                    }

                    if (reader.IsDBNull(8))
                    {
                        user.RegionId = null;
                    }
                    else
                    {
                        user.RegionId = reader.GetInt32(8);
                    }

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
            return user;
        }
        
       
        /// <summary>
        /// Author: Ibrahim Abuzaid
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/4 By Ibarahim
        /// Updated Stored Procedure and Method Name 4/14/16 Emily
        /// 
        /// Updated method - SP calls for active 4/21/16 Steve Hoover
        /// </summary>
        public static List<User> RetrieveUserList(Active active)
        {
            var user = new List<User>();
            int _active = 0;

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectUsers";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (active == Active.active)
            {
                _active = 1;
            }

            cmd.Parameters.AddWithValue("@Active", _active);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
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
                            Password = reader.GetString(6),
                            Active = reader.GetBoolean(7)

                        };

                        if (reader.IsDBNull(3))
                        {
                            currentUser.Zip = null;
                        }
                        else
                        {
                            currentUser.Zip = reader.GetString(3);
                        }

                        if (reader.IsDBNull(4))
                        {
                            currentUser.EmailAddress = null;
                        }
                        else
                        {
                            currentUser.EmailAddress = reader.GetString(4);
                        }


                        if (reader.IsDBNull(8))
                        {
                            currentUser.RegionId = null;
                        }
                        else
                        {
                            currentUser.RegionId = reader.GetInt32(8);
                        }

                        user.Add(currentUser);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found");
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
            return user;
        }

        ///
         /// Is no need for two User Count methods. I commented this one out as the other one uses a 
         /// Stored Procedure. I'm leaving this here, just in case it is decided to just fix this one and delete the other for 
         /// some reason
         ///-Emily 4-14-16
         public static int RetrieveUserCount(int userID)
        {
            int count = 0;

            // let's try a scalar query

            // start with a connection object
            var conn = DBConnection.GetDBConnection();

            // write some command text
            string query = @"SELECT COUNT(*) " +
                           @"FROM Admin.Users ";

           

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
       
     
        public static int UpdateUser(User usr)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            string query = @"UPDATE Admin.Users SET " +
                        @"FirstName = @FirstName, LastName = @LastName,  Zip = @Zip, EmailAddress = @EmailAddress, " +
                        @"PassWord = @PassWord, Active = @Active, RegionID = @RegionID " +
                        @"WHERE UserID = @UserID ";

            var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserID", usr.UserID);
            cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
            cmd.Parameters.AddWithValue("@LastName", usr.LastName);
            cmd.Parameters.AddWithValue("@Zip", usr.Zip);
            cmd.Parameters.AddWithValue("@EmailAddress", usr.EmailAddress);
            cmd.Parameters.AddWithValue("@UserName", usr.UserName);
            cmd.Parameters.AddWithValue("@PassWord", usr.Password);
            cmd.Parameters.AddWithValue("@Active", usr.Active);
            cmd.Parameters.AddWithValue("@RegionID", usr.RegionId);


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

        /// <summary>
        /// Checks to see if the value exists as a username in the database.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Value to check database.</param>
        /// <returns>Whether the username exists in the database.</returns>
        public static bool CheckUserName(string userName)
        {
            bool flag = true;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spSelectUserNameCount", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", userName);

            try
            {
                conn.Open();

                flag = 1 == (int)cmd.ExecuteScalar();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return flag;
        }

        /// <summary>
        /// Checks to see if the value exists as a username with password in the database.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Value to check database.</param>
        /// <param name="passWord">Value to check database.</param>
        /// <returns>Whether the username exists with password in the database.</returns>
        public static bool CheckUserNameWithPassword(string userName, string passWord) 
        {
            bool flag = true;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spSelectUserWithUsernameAndPassword", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@PassWord", passWord);

            try
            {
                conn.Open();

                flag = 1 == (int)cmd.ExecuteScalar();
            }
            catch (SqlException) 
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return flag;
        }

        /// <summary>
        /// Confirms information entered by user is correct in database.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Username that relates.</param>
        /// <param name="email">Email to verified against database.</param>
        /// <param name="password">Password to be verified against database.</param>
        /// <returns>Whether all values can match.</returns>
        public static bool ConfirmUserInfo(string userName, string email, string password)
        {
            bool flag = false;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spSelectUserInformationCount", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", 
                userName);
            cmd.Parameters.AddWithValue("@EmailAddress", 
                email);
            cmd.Parameters.AddWithValue("@Password", 
                password);

            try
            {
                conn.Open();

                flag = 1 == (int)cmd.ExecuteScalar();
            }
            catch (SqlException) 
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return flag;
        }

        /// <summary>
        /// Change password for user.
        /// 
        /// Created by: Trent Cullinan 03/25/16
        /// </summary>
        /// <param name="userName">Username that relates.</param>
        /// <param name="oldPassWord">Password to be verified against database.</param>
        /// <param name="newPassWord">New password to be set in database.</param>
        /// <returns>Whether the action was successful.</returns>
        public static bool UpdateUserPassword(string userName, string oldPassWord, string newPassWord)
        {
            bool flag = false;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Admin.spUpdatePassword", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Parameters.AddWithValue("@oldPassWord", oldPassWord);
            cmd.Parameters.AddWithValue("@newPassWord", newPassWord);

            try
            {
                conn.Open();

                flag = 1 == (int)cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return flag;
        }
        public static int RetrieveUserCount()
        {
            int count = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("Admin.spGetUserCount", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (SqlException)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;

        }

        /// <summary>
        /// Poonam Dubey
        /// 18th April 2016
        /// Function to check if user is garden leader
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        //public static bool CheckIsGardenLeader(int userID)
        //{
        //    int count = 0;

        //    var conn = DBConnection.GetDBConnection();
        //    var cmd = new SqlCommand("Gardens.spCheckUserIsGroupLeader", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@UserID", userID);

        //    try
        //    {
        //        conn.Open();
        //        count = (int)cmd.ExecuteScalar();
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return count == 1;
        //}

    }
}
