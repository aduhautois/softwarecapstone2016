using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess
{
    public class RegionAccessor
    {
        /// <summary>
        /// Rhett Allen
        /// Created Date: 3/23/16
        /// Gets all of the regions in the database
        /// </summary>
        /// <returns>All region in the database</returns>
        /// changed method name from FetchRegions 4/21/16 Steve Hoover
        public static List<Region> RetrieveRegions()
        {
            List<Region> regions = new List<Region>();

            var conn = DBConnection.GetDBConnection();
            var query = @"Admin.spSelectRegions";
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
                        Region region = new Region()
                        {
                            RegionID = reader.GetInt32(0),
                            CreatedBy = reader.GetInt32(7),
                            CreatedDate = reader.GetDateTime(8)
                        };

                        if (reader.IsDBNull(1))
                        {
                            region.SoilType = null;
                        }
                        else
                        {
                            region.SoilType = reader.GetString(1);
                        }

                        if (reader.IsDBNull(2))
                        {
                            region.AverageTemperatureSummer = null;
                        }
                        else
                        {
                            region.AverageTemperatureSummer = reader.GetDecimal(2);
                        }

                        if (reader.IsDBNull(3))
                        {
                            region.AverageTemperatureFall = null;
                        }
                        else
                        {
                            region.AverageTemperatureFall = reader.GetDecimal(3);
                        }

                        if (reader.IsDBNull(4))
                        {
                            region.AverageTemperatureWinter = null;
                        }
                        else
                        {
                            region.AverageTemperatureWinter = reader.GetDecimal(4);
                        }

                        if (reader.IsDBNull(5))
                        {
                            region.AverageTemperatureSpring = null;
                        }
                        else
                        {
                            region.AverageTemperatureSpring = reader.GetDecimal(5);
                        }

                        if (reader.IsDBNull(6))
                        {
                            region.AverageRainfall = null;
                        }
                        else
                        {
                            region.AverageRainfall = reader.GetDecimal(6);
                        }

                        if (reader.IsDBNull(9))
                        {
                            region.ModifiedBy = null;
                        }
                        else
                        {
                            region.ModifiedBy = reader.GetInt32(9);
                        }

                        if (reader.IsDBNull(10))
                        {
                            region.ModifiedDate = null;
                        }
                        else
                        {
                            region.ModifiedDate = reader.GetDateTime(10);
                        }

                        regions.Add(region);
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

            return regions;
        }
    }
}
