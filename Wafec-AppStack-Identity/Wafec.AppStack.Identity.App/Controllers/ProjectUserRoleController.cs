using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.ProjectUserRoleController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class ProjectUserRoleController : ApiController
    {
        public IProjectService ProjectService { get; private set; }
        public IRepository Repository { get; private set; }

        public ProjectUserRoleController(IProjectService projectService, IRepository repository)
        {
            ProjectService = projectService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateProjectUserRoleModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var projectUserRole = ProjectService.AddUserRole(model.ProjectUserId, model.ProjectRoleId);
                t.Commit();
                return Json(projectUserRole);
            }
        }
    }
}
