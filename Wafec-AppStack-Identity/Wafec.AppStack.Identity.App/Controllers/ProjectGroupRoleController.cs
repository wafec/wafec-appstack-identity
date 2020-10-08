using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.ProjectGroupRoleController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class ProjectGroupRoleController : ApiController
    {
        public IProjectService ProjectService { get; private set; }
        public IRepository Repository { get; private set; }

        public ProjectGroupRoleController(IProjectService projectService, IRepository repository)
        {
            ProjectService = projectService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateProjectGroupRoleModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var projectGroupRole = ProjectService.AddGroupRole(model.ProjectGroupId, model.ProjectRoleId);
                t.Commit();
                return Json(projectGroupRole);
            }
        }
    }
}
