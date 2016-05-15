using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/14/16
    /// </summary>
    public class UserNeedsManager
    {
        private int userId;
        private UserNeedsAccessor gardenNeedsAccessor;

        protected UserNeedsAccessor Accessor
        {
            get
            {
                gardenNeedsAccessor = gardenNeedsAccessor ?? new UserNeedsAccessor(userId);

                return gardenNeedsAccessor;
            }

            set
            {
                gardenNeedsAccessor = value;
            }
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="userId"></param>
        public UserNeedsManager(int userId)
        {
            this.userId = userId;
        }

        public UserNeedsManager()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> GetSentContributions()
        {
            IEnumerable<NeedContribution> contributions = null;

            try
            {
                contributions = Accessor.RetrieveSentContributions();
            }
            catch (Exception) { }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> GetAcceptedContributions()
        {
            IEnumerable<NeedContribution> contributions = null;

            try
            {
                contributions = Accessor.RetrieveAcceptedContributions();
            }
            catch (Exception) { }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> GetDeclinedContributions()
        {
            IEnumerable<NeedContribution> contributions = null;

            try
            {
                contributions = Accessor.RetrieveDeclinedContributions();
            }
            catch (Exception) { }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GardenNeed> GetAvailableNeeds()
        {
            IEnumerable<GardenNeed> needs = null;

            try
            {
                needs = Accessor.RetrieveAvailableNeeds();
            }
            catch (Exception) { }

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needContribution"></param>
        /// <returns></returns>
        public bool SendContribution(NeedContribution needContribution)
        {
            bool flag = false;

            try
            {
                flag = 1 == Accessor.SendContribution(needContribution);
            }
            catch (Exception) { }

            return flag;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="contributionId"></param>
        /// <returns></returns>
        public bool CancelPendingContribution(int contributionId)
        {
            bool flag = false;

            try
            {
                flag = 1 == Accessor.CancelPendingContribution(contributionId);
            }
            catch (Exception) { }

            return flag;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupNeedSummary> RetrieveGroupsOfNeed()
        {
            IEnumerable<GroupNeedSummary> summary = null;

            try
            {
                summary = UserNeedsAccessor.RetrieveGroupsOfNeed();
            }
            catch (Exception) { }

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
            IEnumerable<GardenNeedSummary> summary = null;

            try
            {
                summary = UserNeedsAccessor.RetrieveGroupGardensOfNeed(groupID);
            }
            catch (Exception) { }

            return summary;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="needID"></param>
        /// <returns></returns>
        public static GardenNeed RetrieveNeed(int needID)
        {
            GardenNeed need = null;

            try
            {
                need = GardenNeedsAccessor.RetrieveGardenNeed(needID);
            }
            catch (Exception) { }

            return need;
        }

        public IEnumerable<NeedContribution> RetrieveAllContributions()
        {
            IEnumerable<NeedContribution> contributions = null;

            try
            {
                contributions = new UserNeedsAccessor(this.userId).RetrieveAllUsersContributions();
            }
            catch (Exception) { }

            return contributions;
        }
    }
}
