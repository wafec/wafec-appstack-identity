using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.TokenController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;
using Wafec.AppStack.Identity.Service.Models.AuthTokenService;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class TokenController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IAuthTokenService AuthTokenService { get; private set; }
        public IMapper Mapper { get; private set; }

        public TokenController(IRepository repository, IAuthTokenService authTokenService, IMapper mapper)
        {
            Repository = repository;
            AuthTokenService = authTokenService;
            Mapper = mapper;
        }

        public IHttpActionResult Post([FromBody] CreateTokenModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var authToken = AuthTokenService.Create(model.Username, model.Password);
                t.Commit();
                return Json(Mapper.Map<AuthTokenCreateView>(authToken));
            }
        }
    }
}
