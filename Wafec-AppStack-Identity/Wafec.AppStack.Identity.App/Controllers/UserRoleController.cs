using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.UserRoleController;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class UserRoleController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IUserService UserService { get; private set; }

        public UserRoleController(IRepository repository, IUserService userService)
        {
            Repository = repository;
            UserService = userService;
        }

        public IHttpActionResult Post([FromBody] CreateUserRoleModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                UserRole userRole = UserService.AddUserRole(model.UserId, model.RoleId);
                t.Commit();
                return Json(userRole);
            }
        }
    }
}
