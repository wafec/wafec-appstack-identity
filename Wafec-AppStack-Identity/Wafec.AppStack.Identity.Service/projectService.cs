using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class ProjectService : IProjectService
    {
        public IRepository Repository { get; private set; }

        public ProjectService(IRepository repository)
        {
            this.Repository = repository;
        }

        public Project CreateProject(string name, string description, User owner)
        {
            if (!ProjectExists(name))
            {
                Project project = new Project()
                {
                    Name = name,
                    Description = description,
                    Owner = owner
                };
                Repository.Add(project);
                return project;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool ProjectExists(string name)
        {
            return Repository.GetSet<Project>().Any(p => p.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
