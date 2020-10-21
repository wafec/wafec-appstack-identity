using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public interface IUserService
    {
        User CreateUser(string name, string password);
        bool ExistsUser(string name);
        bool ExistsUser(string name, string password);
        UserRole AddUserRole(long userId, long roleId);
        User FindUser(long id);
        User ChangePassword(long id, string currentPassword, string newPassword);
        void DeleteUser(long id);
        User FindUser(string name);
        User FindUser(string name, string password);
        void RemoveUserRole(long userId, long roleId);
        UserRole FindUserRole(long userId, long roleId);
    }
}
