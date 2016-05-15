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
    /// Blueprint Accessor 
    /// </summary>
   public class BlueprintAccessor
    {

        // call up blueprint by BlueprintID and return it 
       public static Blueprint RetrieveBlueprintById(int BlueprintID)
        {
            var blueprint = new Blueprint();
            var conn = DBConnection.GetDBConnection(); 
            var query = @"Expert.spSelectBlueprintByID";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BlueprintID", BlueprintID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    blueprint.Title = reader.GetString(0);
                    blueprint.Description = reader.GetString(1);
                    blueprint.DateCreated = reader.GetDateTime(2);
                    blueprint.ModifiedBy = reader.GetInt32(3);
                    blueprint.FilePath = reader.GetString(4);
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
            return blueprint;
        }


      // retrive list of all blueprints
       public static List<Blueprint> RetrieveAllBlueprints()
        {
            var Blueprint = new List<Blueprint>();

            var conn = DBConnection.GetDBConnection();        
            var query = @"Expert.spSelectAllBlueprints"; 
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
                        var currentBlueprint = new Blueprint()
                        {
                            Title = reader.GetString(0),
                            Description = reader.GetString(1),
                            DateCreated = reader.GetDateTime(2),
                            ModifiedBy = reader.GetInt32(3),
                            FilePath = reader.GetString(4)
                        };
                        Blueprint.Add(currentBlueprint);
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
            return Blueprint;
        }

       // upload blueprint to database
        public static bool UploadBlueprint(Blueprint blueprint)
        {
            bool flag = false;

            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spInsertExpertBluePrints";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", blueprint.Title);
            cmd.Parameters.AddWithValue("@Description", blueprint.Description);
            cmd.Parameters.AddWithValue("@DateCreated", blueprint.DateCreated);
            cmd.Parameters.AddWithValue("@CreatedBy", blueprint.ModifiedBy);
            cmd.Parameters.AddWithValue("@FilePath", blueprint.FilePath);

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
