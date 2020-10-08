using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wafec.AppStack.Identity.App.Models.ProjectGroupController;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.App.Controllers
{
    public class ProjectGroupController : ApiController
    {
        public IProjectService ProjectService { get; private set; }
        public IRepository Repository { get; private set; }

        public ProjectGroupController(IProjectService projectService, IRepository repository)
        {
            ProjectService = projectService;
            Repository = repository;
        }

        public IHttpActionResult Post([FromBody] CreateProjectGroupModel model)
        {
            using (var t = Repository.BeginTransaction())
            {
                var projectGroup = ProjectService.AddGroup(model.ProjectId, model.GroupId);
                t.Commit();
                return Json(projectGroup);
            }
        }
    }
}
