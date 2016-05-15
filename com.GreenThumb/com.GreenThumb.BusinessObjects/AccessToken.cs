/// <summary>
/// Ryan Taylor
/// Created: 2016/03/01
/// </summary>
/// <remarks>
/// Updated by Ryan Taylor 2016/03/03
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public sealed class AccessToken : User
    {
        public List<Role> Roles { get; private set; }

        public AccessToken() { }
        public AccessToken(User user, List<Role> roles)
        {
            if (user == null || roles == null || roles.Count == 0 || !user.Active)
            {
                throw new ApplicationException("Invalid User");
            }

            base.FirstName = user.FirstName;
            base.LastName = user.LastName;
            base.Zip = user.Zip;
            base.EmailAddress = user.EmailAddress;
            base.RegionId = user.RegionId;
            base.UserName = user.UserName;
            base.Active = user.Active;
            base.UserID = user.UserID;

            Roles = roles;
        }


    }
}
