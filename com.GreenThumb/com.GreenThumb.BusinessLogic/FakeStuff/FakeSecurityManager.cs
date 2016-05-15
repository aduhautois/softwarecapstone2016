/// <summary>
/// Ryan Taylor
/// Created: 2016/03/10
/// Fake class used for testing without database integration.
/// </summary> 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.FakeStuff
{
    public class FakeSecurityManager : com.GreenThumb.BusinessLogic.Interfaces.ISecurityManager
    {
        public BusinessObjects.AccessToken ValidateExistingUser(string username, string password)
        {
            AccessToken _token = new AccessToken();

            if (username.Equals("ryant") && password.Equals("password"))
            {
                _token.UserName = "ryant";
                return _token;
            }

            return null;
        }

        public BusinessObjects.AccessToken ValidateNewUser(string username, string newPassword)
        {
            return ValidateExistingUser(username, newPassword);
        }
    }
}
