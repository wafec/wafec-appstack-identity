using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;

namespace Wafec.AppStack.Identity.Service
{
    public interface IAuthTokenService
    {
        AuthToken Create(string username, string password);
        void Delete(string token, string refresh);
        AuthToken FindAuthToken(string token, string refresh);
        AuthToken FindAuthToken(string token);
    }
}
