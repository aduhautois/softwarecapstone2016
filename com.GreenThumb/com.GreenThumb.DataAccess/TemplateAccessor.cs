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
    /// Stenner kvindlog
    /// Template Accessor 
    /// </summary>
    public class TemplateAccessor
    {

        // call up Template by TemplateID and return it 
        public static Template RetrieveTemplateById(int TemplateID)
        {
            var template = new Template();
            var conn = DBConnection.GetDBConnection(); 
            var query = @"Expert.spSelectTemplateByID"; 
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TemplateID", TemplateID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    template.Title = reader.GetString(0);
                    template.Description = reader.GetString(1);
                    template.DateCreated = reader.GetDateTime(2);
                    template.ModifiedBy = reader.GetInt32(3);
                    template.FilePath = reader.GetString(4);
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
            return template;
        }


        // retrive list of all Templates
        public static List<Template> RetrieveAllTemplate()
        {
            var Template = new List<Template>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectAllTemplates"; 
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var currentTemplate = new Template()
                        {
                            Title = reader.GetString(0),
                            Description = reader.GetString(1),
                            DateCreated = reader.GetDateTime(2),
                            ModifiedBy = reader.GetInt32(3),
                            FilePath = reader.GetString(4)
                        };
                        Template.Add(currentTemplate);
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
            return Template;
        }

        // upload Template to database
        public static bool UploadTemplate(Template template)
        {
            
            bool flag = false;

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spInsertExpertTemplate";  
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", template.Title);
            cmd.Parameters.AddWithValue("@Description", template.Description);
            cmd.Parameters.AddWithValue("@DateCreated", template.DateCreated);
            cmd.Parameters.AddWithValue("@CreatedBy", template.ModifiedBy);
            cmd.Parameters.AddWithValue("@FilePath", template.FilePath);

            try
            {
                // open the connection
                conn.Open();

                // execute the command with ExecuteScalar()
                cmd.ExecuteScalar();
                flag = true;
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
