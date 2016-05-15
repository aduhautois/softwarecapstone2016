using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic.FakeStuff
{
    /// <summary>
    /// Fake user manager class for unit testing by : Poonam Dubey,  Dated : 23rd March 2016
    /// </summary>
    public class FakeUserManager
    {
        public int AddNewUser(string firstName,
                                  string lastName,
                                  string zip,
                                  string emailAddress,
                                  string userName,
                                  string passWord,
                                  bool active,
                                  int? regionID)
        {
            try
            {
                if (userName == "johnDoe")
                {
                    return 2;
                }
                else
                    return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}