using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.ProjectRoleController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class ProjectRoleController : ApiController
    {
        public IProjectService ProjectService { get; private set; }
        public IRepository Repository { get; private set; }

        public ProjectRoleController(IProjectService projectService, IRepository repository)
        {
            ProjectService = projectService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateProjectRoleModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var projectRole = ProjectService.AddRole(model.ProjectId, model.RoleId);
                t.Commit();
                return Json(projectRole);
            }
        }
    }
}
