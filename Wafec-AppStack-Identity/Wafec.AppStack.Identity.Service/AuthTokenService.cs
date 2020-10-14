using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class AuthTokenService : BaseService, IAuthTokenService
    {
        public IUserService UserService { get; private set; }
        public IRepository Repository { get; private set; }

        public AuthTokenService(IUserService userService, IRepository repository)
        {
            UserService = userService;
            Repository = repository;
        }

        public AuthToken Create(string username, string password)
        {
            var user = UserService.FindUser(username, password);
            AuthToken authToken = new AuthToken();
            authToken.User = user;
            authToken.Created = DateTime.Now;
            authToken.Token = Guid.NewGuid().ToString();
            authToken.Refresh = Guid.NewGuid().ToString();
            authToken.Expires = authToken.Created.AddHours(4);
            Repository.Add(authToken);
            return authToken;
        }

        public void Delete(string token, string refresh)
        {
            var authToken = FindAuthToken(token, refresh);
            authToken.Deleted = true;
            Repository.Update(authToken);
        }

        private AuthToken FindAuthToken(string token, string refresh, bool ignoreRefreshWhenNull)
        {
            var authToken = Repository.GetSet<AuthToken>().FirstOrDefault(m => m.Deleted == false && m.Token.Equals(token) && ((string.IsNullOrEmpty(refresh) && ignoreRefreshWhenNull) || (m.Refresh.Equals(refresh))));
            if (authToken.Expires < DateTime.Now)
                throw new ExpiredTokenException();
            if (authToken != null)
                return authToken;
            else
                throw new NotFoundException();
        }

        public AuthToken FindAuthToken(string token, string refresh)
        {
            return FindAuthToken(token, refresh, false);
        }

        public AuthToken FindAuthToken(string token)
        {
            return FindAuthToken(token, null, true);
        }
    }
}
