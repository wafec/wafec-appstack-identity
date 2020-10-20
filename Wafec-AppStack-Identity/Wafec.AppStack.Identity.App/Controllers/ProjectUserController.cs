using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.ProjectUserController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class ProjectUserController : ApiController
    {
        public IProjectService ProjectService { get; private set; }
        public IRepository Repository { get; private set; }

        public ProjectUserController(IProjectService projectService, IRepository repository)
        {
            ProjectService = projectService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateProjectUserModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var projectUser = ProjectService.AddProjectUser(model.ProjectId, model.UserId);
                t.Commit();
                return Json(projectUser);
            }
        }

        public IHttpActionResult Delete([FromUri] RemoveProjectUserModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                ProjectService.RemoveProjectUser(model.ProjectId, model.UserId);
                t.Commit();
                return Ok();
            }
        }
    }
}
