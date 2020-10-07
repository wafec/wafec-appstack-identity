using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.GroupController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class GroupController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IGroupService GroupService { get; private set; }

        public GroupController(IRepository repository, IGroupService groupService)
        {
            Repository = repository;
            GroupService = groupService;
        }

        public IHttpActionResult Post([FromBody] CreateGroupModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var group = GroupService.CreateGroup(model.Name);
                t.Commit();
                return Json(group);
            }
        }
    }
}
