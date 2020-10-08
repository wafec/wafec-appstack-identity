using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.GroupRoleController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class GroupRoleController : ApiController
    {
        public IGroupService GroupService { get; private set; }
        public IRepository Repository { get; private set; }

        public GroupRoleController(IGroupService groupService, IRepository repository)
        {
            GroupService = groupService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateGroupRoleModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var groupRole = GroupService.AddRole(model.GroupId, model.RoleId);
                t.Commit();
                return Json(groupRole);
            }
        }
    }
}
