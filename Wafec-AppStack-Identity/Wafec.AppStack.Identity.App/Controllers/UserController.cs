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
    public class UserController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IUserService UserService { get; private set; }

        public UserController(IRepository repository, IUserService userService)
        {
            Repository = repository;
            UserService = userService;
        }

        public IHttpActionResult Post([FromBody] string name, [FromBody] string password)
        {
            using (var t = Repository.BeginTransaction())
            {
                var user = UserService.CreateUser(name, password);
                t.Commit();
                return Json(user);
            }
        }
    }
}
