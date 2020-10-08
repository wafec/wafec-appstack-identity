using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.ProjectController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class ProjectController : ApiController
    {
        public IRepository Repository { get; private set; }
        public IProjectService ProjectService { get; private set; }

        public ProjectController(IRepository repository, IProjectService projectService)
        {
            Repository = repository;
            ProjectService = projectService;
        }

        public IHttpActionResult Post([FromBody] CreateProjectModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var project = ProjectService.CreateProject(model.Name, model.Description, model.OwnerId);
                t.Commit();
                return Json(project);
            }
        }
    }
}
