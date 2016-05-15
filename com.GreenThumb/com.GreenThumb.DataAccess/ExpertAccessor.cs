using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Nicholas King
    /// </summary>
    public class ExpertAccessor
    {
        public static int CreateGardenTemplate(byte[] file, int userID, string fileName)
        {

            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spInsertGardenTemplate";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ImageName", fileName);
            cmd.Parameters.AddWithValue("@CreatedBy", userID);
            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@ImageFile", file);

            try
            {
                conn.Open();
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


        ///<summary>
        ///Author: Nicholas King 
        ///Date: 3/19/16
        ///Retrieve lists of garden templete 
        ///</summary>
        public static List<GardenTemplate> RetrieveAllGardenTemplates()
        {
            var templateList = new List<GardenTemplate>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectAllGardenTemplateNames";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GardenTemplate newTemplate = new GardenTemplate()
                        {
                            TemplateName = reader.GetString(0),
                            CreateDate = reader.GetDateTime(1)
                        };
                        templateList.Add(newTemplate);
                    }
                }
                else
                {
                    var msg = new ApplicationException("No Templates were found");
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
            return templateList;
        }

        ///<summary>
        ///Author: Nicholas King 
        ///Date: 3/19/16
        ///Retrieve a garden templete name
        ///</summary>
        public static byte[] RetrieveGardenTemplate(string fileName)
        {
            byte[] data;
            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectGardenTemplate";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FileName", fileName);

            try
            {
                conn.Open();
                data = cmd.ExecuteScalar() as byte[];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return data;
        }

        ///<summary>
        ///Author: Stenner Kvindlog 
        ///submits application to database to be reviewed
        ///Date: 3/19/16
        ///</summary>
        /// <remarks>
        /// Updated by: Chris Sheehan
        /// Date: 4/28/16
        /// </remarks>
        public static bool CreateExpertApplication(String Title, String Description, int UserID, DateTime Time)
        {
            var conn = DBConnection.GetDBConnection();
            var query = "Admin.spInsertExpertRequest";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Content", Description);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

            bool flag = false;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    flag = true;
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

            return flag;
        }
    }
}
