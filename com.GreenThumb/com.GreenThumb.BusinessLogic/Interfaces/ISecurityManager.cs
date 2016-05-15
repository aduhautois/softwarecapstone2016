/// <summary>
/// Ryan Taylor 
/// Created: 2016/03/10
/// </summary> 

using System;
namespace com.GreenThumb.BusinessLogic.Interfaces
{
    public interface ISecurityManager
    {
        com.GreenThumb.BusinessObjects.AccessToken ValidateExistingUser(string username, string password);
        com.GreenThumb.BusinessObjects.AccessToken ValidateNewUser(string username, string newPassword);
    }
}
