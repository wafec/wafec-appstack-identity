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
        bool UserExists(string name);
        bool UserExists(string name, string password);
    }
}
