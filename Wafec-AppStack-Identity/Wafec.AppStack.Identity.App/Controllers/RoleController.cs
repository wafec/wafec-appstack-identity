using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.RoleController;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class RoleController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IRoleService RoleService { get; private set; }

        public RoleController(IRepository repository, IRoleService roleService)
        {
            Repository = repository;
            RoleService = roleService;
        }

        public IHttpActionResult Post([FromBody] CreateRoleModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                Role role = RoleService.CreateRole(model.Name, model.Description);
                t.Commit();
                return Json(role);
            }
        }
    }
}
