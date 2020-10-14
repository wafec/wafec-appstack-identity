using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class TokenController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IAuthTokenService AuthTokenService { get; private set; }

        public TokenController(IRepository repository, IAuthTokenService authTokenService)
        {
            Repository = repository;
            AuthTokenService = authTokenService;
        }
    }
}
