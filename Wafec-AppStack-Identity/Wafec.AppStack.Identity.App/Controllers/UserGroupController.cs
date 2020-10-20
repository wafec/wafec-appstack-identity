using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.UserGroupController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class UserGroupController : ApiController
    {
        public IGroupService GroupService { get; private set; }
        public IRepository Repository { get; private set; }

        public UserGroupController(IGroupService groupService, IRepository repository)
        {
            GroupService = groupService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateUserGroupModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var userGroup = GroupService.AddUserGroup(model.GroupId, model.UserId);
                t.Commit();
                return Json(userGroup);
            }
        }

        public IHttpActionResult Delete([FromUri] RemoveUserGroupModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                GroupService.RemoveUserGroup(model.GroupId, model.UserId);
                return Ok();
            }
        }
    }
}
