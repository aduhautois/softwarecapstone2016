using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess
{
    public class NutrientAccessor
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 4/7/16
        /// Adds a single nutrient to a plant
        /// </summary>
        /// <param name="nutrientID">Nutrient ID of nutrient to be added</param>
        /// <param name="plantID">Plant ID of plant the nutrient is added to</param>
        /// <returns>True if the nutrient was added successfully</returns>
        public static bool InsertPlantNutrients(int nutrientID, int? plantID)
        {
            bool inserted = false;

            var conn = DBConnection.GetDBConnection();
            var query = "Expert.spInsertPlantNutrients";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PlantID", plantID);
            cmd.Parameters.AddWithValue("@NutrientID", nutrientID);

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    inserted = true;
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

            return inserted;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 4/7/16
        /// Gets a list of nutrients
        /// </summary>
        /// <returns>List of nutrients</returns>
        public static List<Nutrient> RetrieveNutrient()
        {
            List<Nutrient> nutrients = new List<Nutrient>();

            var conn = DBConnection.GetDBConnection();
            var query = "Expert.spSelectNutrients";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Nutrient nutrient = new Nutrient()
                        {
                            NutrientID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreatedBy = reader.GetInt32(3),
                            CreatedDate = reader.GetDateTime(4)
                        };

                        if (reader.IsDBNull(5))
                        {
                            nutrient.ModifiedBy = null;
                        }
                        else
                        {
                            nutrient.ModifiedBy = reader.GetInt32(5);
                        }

                        if(reader.IsDBNull(6))
                        {
                            nutrient.ModifiedDate = null;
                        }
                        else
                        {
                            nutrient.ModifiedDate = reader.GetDateTime(6);
                        }

                        nutrients.Add(nutrient);
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

            return nutrients;
        }

        /// <summary>
        /// Rhett Allen
        /// Created Date: 4/7/16
        /// Gets a list of all the nutrients for a single plant
        /// </summary>
        /// <param name="plantID">The plant ID of the plant to get the nutrients from</param>
        /// <returns>List of all the nutrients for a single plant</returns>
        public static List<Nutrient> RetrievePlantNutrients(int? plantID)
        {
            List<Nutrient> nutrients = new List<Nutrient>();

            var conn = DBConnection.GetDBConnection();
            var query = "Expert.spSelectPlantNutrients";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PlantID", plantID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Nutrient nutrient = new Nutrient()
                        {
                            NutrientID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreatedBy = reader.GetInt32(3),
                            CreatedDate = reader.GetDateTime(4)
                        };

                        if (reader.IsDBNull(5))
                        {
                            nutrient.ModifiedBy = null;
                        }
                        else
                        {
                            nutrient.ModifiedBy = reader.GetInt32(5);
                        }

                        if (reader.IsDBNull(6))
                        {
                            nutrient.ModifiedDate = null;
                        }
                        else
                        {
                            nutrient.ModifiedDate = reader.GetDateTime(6);
                        }

                        nutrients.Add(nutrient);
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

            return nutrients;
        }
    }
}
