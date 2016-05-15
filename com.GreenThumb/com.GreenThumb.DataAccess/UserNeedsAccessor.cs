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
    public class UserNeedsAccessor
    {
        private int userId;

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="userId"></param>
        public UserNeedsAccessor(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveSentContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectUsersMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID",
                this.userId);

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
                        Need = new GardenNeed()
                        {
                            GardenNeedId = reader.GetInt32(1)
                        },
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(3),
                        DateModified
                            = reader.IsDBNull(4) ? DateTime.MinValue :
                                reader.GetDateTime(4)
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
        public IEnumerable<NeedContribution> RetrieveAcceptedContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectUsersMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
                        Need = new GardenNeed()
                        {
                            GardenNeedId = reader.GetInt32(1)
                        },
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(3),
                        DateModified
                            = reader.IsDBNull(4) ? DateTime.MinValue :
                                reader.GetDateTime(4)
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

            var cmd = new SqlCommand("Needs.spSelectUsersMetNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID",
                this.userId);

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
                        Need = new GardenNeed()
                        {
                            GardenNeedId
                                = reader.GetInt32(1)
                        },
                        Description
                            = reader.GetString(2),
                        DateCreated
                            = reader.GetDateTime(3),
                        DateModified
                            = reader.IsDBNull(4) ? DateTime.MinValue :
                                reader.GetDateTime(4)
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
        public IEnumerable<GardenNeed> RetrieveAvailableNeeds()
        {
            List<GardenNeed> needs = new List<GardenNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    needs.Add(new GardenNeed()
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

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needContribution"></param>
        /// <returns></returns>
        public int SendContribution(NeedContribution needContribution)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spInsertContributions", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NeedID",
                needContribution.Need.GardenNeedId);
            cmd.Parameters.AddWithValue("@Description",
                needContribution.Description);
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
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needContributionId"></param>
        /// <returns></returns>
        public int CancelPendingContribution(int needContributionId)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spUpdateCancelContribution", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(@"@ContributionID",
                needContributionId);

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
        /// <returns></returns>
        public static IEnumerable<GroupNeedSummary> RetrieveGroupsOfNeed()
        {
            List<GroupNeedSummary> summary = new List<GroupNeedSummary>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGroupsInNeed", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    summary.Add(new GroupNeedSummary()
                    {
                        ActiveNeeds 
                            = reader.GetInt32(0),
                        CompletedNeeds 
                            = reader.GetInt32(1),
                        GroupID 
                            = reader.GetInt32(2),
                        GroupName 
                            = reader.GetString(3),
                        GroupLeader = new User()
                        {
                            UserName 
                                = reader.GetString(4)
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

            return summary;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static IEnumerable<GardenNeedSummary> RetrieveGroupGardensOfNeed(int groupID)
        {
            List<GardenNeedSummary> summary = new List<GardenNeedSummary>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGroupsGardenNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID", groupID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    summary.Add(new GardenNeedSummary()
                    {
                         ActiveNeeds 
                            = reader.GetInt32(0),
                         CompletedNeeds 
                            = reader.GetInt32(1),
                         GardenID 
                            = reader.GetInt32(2),
                         GardenName 
                            = reader.GetString(3),
                         Description 
                            = reader.GetString(4),
                         RegionID 
                            = int.Parse(reader.GetString(5))      
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

            return summary;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static IEnumerable<GardenNeed> RetrieveGroupActiveNeeds(int groupID)
        {
            List<GardenNeed> needs = new List<GardenNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectGroupsActiveNeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GroupID",
                groupID);

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
                            = reader.GetString(2),
                        Description
                            = reader.GetString(3),
                        DateCreated
                            = reader.GetDateTime(7),
                        CreatedBy = new User()
                        {
                            UserID
                                = reader.GetInt32(8)
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

        public IEnumerable<NeedContribution> RetrieveAllUsersContributions()
        {
            List<NeedContribution> contributions = new List<NeedContribution>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("Needs.spSelectUsersContributions", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID",
                this.userId);

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
                                Garden 
                                    = new Garden()
                                    {
                                        GardenID 
                                            = reader.GetInt32(8)
                                    },
                                Title 
                                    = reader.GetString(9),
                                Description 
                                    = reader.GetString(10),
                                Completed 
                                    = reader.GetBoolean(11),
                                DateCreated     
                                    = reader.GetDateTime(12),
                                CreatedBy 
                                    = new User()
                                    {
                                        UserID 
                                            = reader.GetInt32(13)
                                    }
                            },
                        Description 
                            = reader.GetString(2),
                        Contributed 
                            = reader.IsDBNull(3) ? 
                                default(bool?) : reader.GetBoolean(3),
                        DateCreated 
                            = reader.GetDateTime(4),
                        DateModified 
                            = reader.IsDBNull(5) ? 
                                DateTime.MinValue : reader.GetDateTime(5),
                        Active 
                            = reader.GetBoolean(7),
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
    }
}
