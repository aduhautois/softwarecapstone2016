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
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/14/16
    /// </summary>
    public class GardenNeedsAccessor
    {
        private int userId;
        private int gardenId;

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="userId"></param>
        public GardenNeedsAccessor(int userId, int gardenId)
        {
            this.userId = userId;
            this.gardenId = gardenId;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GardenNeed> RetrieveActiveNeeds()
        {
            List<GardenNeed> needs = new List<GardenNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGardenNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID",
                this.gardenId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    needs.Add(new GardenNeed()
                    {
                        GardenNeedId
                            = reader.GetInt32(0),
                        Title
                            = reader.GetString(1),
                        Description
                            = reader.GetString(2),
                        NeedType
                            = reader.GetString(3),
                        DateCreated
                            = reader.GetDateTime(4),
                        CreatedBy = new User()
                        {
                            UserID 
                                = reader.GetInt32(5)
                        },
                        DateModified 
                            = reader.IsDBNull(6) ? 
                                DateTime.MinValue : reader.GetDateTime(6),
                        ModifiedBy = new User()
                        {
                            UserID 
                                = reader.IsDBNull(7) ? 
                                    default(int) : reader.GetInt32(7)
                        }
                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GardenNeed> RetrieveCompletedNeeds()
        {
            List<GardenNeed> needs = new List<GardenNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGardenMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID",
                this.gardenId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    needs.Add(new GardenNeed()
                    {
                        GardenNeedId
                            = reader.GetInt32(0),
                        Title
                            = reader.GetString(1),
                        Description
                            = reader.GetString(2),
                        NeedType
                            = reader.GetString(3),
                        DateCreated
                            = reader.GetDateTime(4),
                        CreatedBy = new User()
                        {
                            UserID = reader.GetInt32(5)
                        },
                        DateModified
                            = reader.IsDBNull(6) ? DateTime.MinValue :
                                reader.GetDateTime(6)
                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrievePendingContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGardenPendingContributions", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID",
                this.gardenId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contributions.Add(new NeedContribution()
                    {
                        NeedContributionID
                            = reader.GetInt32(0),
                        Need 
                            = new GardenNeed()
                            {
                                GardenNeedId 
                                    = reader.GetInt32(1),
                                Title
                                    = reader.GetString(9)
                            },
                        SentBy
                            = new User()
                            {
                                UserID 
                                    = reader.GetInt32(2),
                                UserName
                                    = reader.GetString(10)
                            },
                        Description
                            = reader.GetString(3),
                        DateCreated
                            = reader.GetDateTime(5),
                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveApprovedContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID",
                    this.gardenId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contributions.Add(new NeedContribution()
                    {

                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveDeclinedContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contributions.Add(new NeedContribution()
                    {

                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        public int AddNeed(GardenNeed need)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spInsertGardenNeed", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID",
                need.Garden.GardenID);
            cmd.Parameters.AddWithValue("@UserID",
                this.userId);
            cmd.Parameters.AddWithValue("@Title",
                need.Title);
            cmd.Parameters.AddWithValue("@Description",
                need.Description);
            cmd.Parameters.AddWithValue("@DateCreated",
                need.DateCreated);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
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
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needId"></param>
        /// <returns></returns>
        public int RemoveNeed(int needId)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spUpdateRemoveNeed", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NeedID", needId);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
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
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        public int EditNeed(GardenNeed need, bool completed = false)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spUpdateNeed", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NeedID",
                need.GardenNeedId);
            cmd.Parameters.AddWithValue("@UserID",
                this.userId);
            cmd.Parameters.AddWithValue("@Title",
                need.Title);
            cmd.Parameters.AddWithValue("@Description",
                need.Description);
            cmd.Parameters.AddWithValue("@Completed",
                completed);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
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
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="contributionId"></param>
        /// <param name="contributed"></param>
        /// <returns></returns>
        public int UpdateContribution(int contributionId, bool contributed)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spUpdateContribution", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ContributionID",
                contributionId);
            cmd.Parameters.AddWithValue("@Contributed",
                contributed);
            cmd.Parameters.AddWithValue("@UserID",
                this.userId);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
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
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public static IEnumerable<GardenNeed> RetrieveActiveGardenNeeds(int gardenId)
        {
            List<GardenNeed> needs = new List<GardenNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGardenNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GardenID",
                gardenId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    needs.Add(new GardenNeed()
                    {
                        GardenNeedId
                            = reader.GetInt32(0),
                        Title
                            = reader.GetString(1),
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(4),
                        CreatedBy = new User()
                        {
                            UserID
                                = reader.GetInt32(5)
                        }
                    });
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return needs;
        }

        public static GardenNeed RetrieveGardenNeed(int needID)
        {
            GardenNeed need = null;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectNeed", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NeedID", 
                needID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    need = new GardenNeed()
                    {
                        GardenNeedId 
                            = needID,
                        Title 
                            = reader.GetString(1),
                        Description 
                            = reader.GetString(2),
                        DateCreated 
                            = reader.GetDateTime(6),
                        CreatedBy = new User()
                        {
                            UserID 
                                = reader.GetInt32(7)
                        }
                    };
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return need;
        }

    }
}
