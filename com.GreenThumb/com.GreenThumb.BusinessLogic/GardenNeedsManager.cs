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
    public class GardenNeedsManager
    {
        private int userId;
        private int gardenId;
        private GardenNeedsAccessor gardenNeedsAccessor;

        protected GardenNeedsAccessor Accessor
        {
            get
            {
                gardenNeedsAccessor = gardenNeedsAccessor ?? new GardenNeedsAccessor(userId, gardenId);

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
        /// <param name="gardenId"></param>
        public GardenNeedsManager(int userId, int gardenId)
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
            IEnumerable<GardenNeed> needs = null;

            try
            {
                needs = Accessor.RetrieveActiveNeeds();

                UserManager userManager = new UserManager();

                // TODO: Refactor to pull user information on first stored proc
                foreach (GardenNeed gardenNeed in needs)
                {
                    gardenNeed.CreatedBy = userManager.GetUser(gardenNeed.CreatedBy.UserID);
                }
            }
            catch (Exception) { }

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GardenNeed> RetrieveCompletedNeeds()
        {
            IEnumerable<GardenNeed> needs = null;

            try
            {
                needs = Accessor.RetrieveCompletedNeeds();
            }
            catch (Exception) { }

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrievePendingContributions()
        {
            IEnumerable<NeedContribution> contributions = null;

            try
            {
                contributions = Accessor.RetrievePendingContributions();
            }
            catch (Exception) { }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveApprovedContributions()
        {
            IEnumerable<NeedContribution> contributions = null;

            try
            {
                contributions = Accessor.RetrieveApprovedContributions();
            }
            catch (Exception) { }

            return contributions;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedContribution> RetrieveDeclinedContributions()
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
        /// <param name="need"></param>
        /// <returns></returns>
        public bool AddNeed(GardenNeed need)
        {
            bool flag = false;

            need.CreatedBy = new User()
            {
                UserID = this.userId
            };

            need.Garden = new Garden()
            {
                GardenID = this.gardenId
            };

            need.DateCreated = DateTime.Now;

            try
            {
                flag = 1 == Accessor.AddNeed(need);
            }
            catch (Exception) { }

            return flag;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="needId"></param>
        /// <returns></returns>
        public bool RemoveNeed(int needId)
        {
            bool flag = false;

            try
            {
                flag = 1 == Accessor.RemoveNeed(needId);
            }
            catch (Exception) { }

            return flag;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/14/16
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        public bool EditNeed(GardenNeed need)
        {
            bool flag = false;

            try
            {
                flag = 1 == Accessor.EditNeed(need);
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
        public bool ApproveContribution(int contributionId)
        {
            bool flag = false;

            try
            {
                flag = 2 == Accessor.UpdateContribution(contributionId, contributed: true);
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
        public bool DeclineContribution(int contributionId)
        {
            bool flag = false;

            try
            {
                flag = 1 == Accessor.UpdateContribution(contributionId, contributed: false);
            }
            catch (Exception) { }

            return flag;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static IEnumerable<GardenNeed> RetrieveGroupNeeds(int groupId)
        {
            IEnumerable<GardenNeed> needs = null;

            try
            {
                needs = UserNeedsAccessor.RetrieveGroupActiveNeeds(groupId);
            }
            catch (Exception) { }

            return needs;
        }

        /// <summary>
        /// 
        /// Created By: Trent Cullinan 04/28/16
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public static IEnumerable<GardenNeed> RetrieveGardenNeeds(int gardenId)
        {
            IEnumerable<GardenNeed> needs = null;

            try
            {
                needs = GardenNeedsAccessor.RetrieveActiveGardenNeeds(gardenId);
            }
            catch (Exception) { }

            return needs;
        }
    }
}
