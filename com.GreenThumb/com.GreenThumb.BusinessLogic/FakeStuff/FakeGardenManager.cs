using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.FakeStuff
{
    /// <summary>
    /// Fake garden manager class to test Garden creation By Poonam Dubey
    /// </summary>
    public class FakeGardenManager : IGardenManager
    {
        /// <summary>
        /// Function to test create garden by Poonam Dubey
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        public bool AddNewGarden(Garden garden)
        {
            if (!string.IsNullOrEmpty(garden.GardenDescription) && garden.GroupID >= 1000 && garden.UserID >= 1000)
            {
                return true;
            }
            else
                return false;
        }
    }
}
