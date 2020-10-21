using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;
using Wafec.AppStack.Identity.Service;

namespace Wafec.AppStack.Identity.ServiceTests
{
    [TestClass]
    public class ProjectServiceTests
    {
        public Mock<IRepository> MockRepository { get; private set; }
        public Mock<IGroupService> MockGroupService { get; private set; }
        public Mock<IUserService> MockUserService { get; private set; }
        public Mock<IRoleService> MockRoleService { get; set; }
        public IRepository Repository => MockRepository.Object;
        public IGroupService GroupService => MockGroupService.Object;
        public IUserService UserService => MockUserService.Object;
        public IRoleService RoleService => MockRoleService.Object;
        public IProjectService ProjectService => new ProjectService(Repository, UserService, RoleService, GroupService);
        public IEnumerable<User> UserSet { get; private set; }
        public IEnumerable<Group> GroupSet { get; private set; }
        public IEnumerable<Role> RoleSet { get; private set; }
        public IEnumerable<Project> ProjectSet { get; private set; }
        public IEnumerable<ProjectUser> ProjectUserSet { get; private set; }
        public IEnumerable<ProjectRole> ProjectRoleSet { get; private set; }
        public IEnumerable<ProjectGroup> ProjectGroupSet { get; private set; }
        public IEnumerable<ProjectUserRole> ProjectUserRoleSet { get; private set; }

        private void InitializeDataSets()
        {
            var userList = new List<User>();
            userList.Add(new User() { Id = 1, Name = "User1" });
            userList.Add(new User() { Id = 2, Name = "User2" });
            var groupList = new List<Group>();
            groupList.Add(new Group() { Id = 1, Name = "Group1" });
            groupList.Add(new Group() { Id = 2, Name = "Group2" });
            var roleList = new List<Role>();
            roleList.Add(new Role() { Id = 1, Name = "Role1" });
            roleList.Add(new Role() { Id = 2, Name = "Role2" });
            var projectList = new List<Project>();
            projectList.Add(new Project() { Id = 1, Name = "Project1" });
            projectList.Add(new Project() { Id = 2, Name = "Project2" });
            var projectUserList = new List<ProjectUser>();
            projectUserList.Add(new ProjectUser() { Id = 1, User = userList.First(u => u.Id == 1), Project = projectList.First(p => p.Id == 1), UserId = 1, ProjectId = 1 });
            projectUserList.Add(new ProjectUser() { Id = 2, User = userList.First(u => u.Id == 2), Project = projectList.First(p => p.Id == 1), UserId = 2, ProjectId = 1 });
            var projectRoleList = new List<ProjectRole>();
            projectRoleList.Add(new ProjectRole() { Id = 1, Project = projectList.First(p => p.Id == 1), ProjectId = 1, Role = roleList.First(r => r.Id == 1), RoleId = 1 });
            projectRoleList.Add(new ProjectRole() { Id = 2, Project = projectList.First(p => p.Id == 1), ProjectId = 1, Role = roleList.First(r => r.Id == 2), RoleId = 2 });
            var projectGroupList = new List<ProjectGroup>();
            projectGroupList.Add(new ProjectGroup() { Id = 1, Project = projectList.First(p => p.Id == 1), ProjectId = 1, Group = groupList.First(g => g.Id == 1), GroupId = 1 });
            var projectUserRoleList = new List<ProjectUserRole>();
            projectUserRoleList.Add(new ProjectUserRole() { Id = 1, ProjectRole = projectRoleList.First(pr => pr.Id == 2), ProjectRoleId = 2, ProjectUser = projectUserList.First(pu => pu.Id == 2), ProjectUserId = 2 });

            UserSet = userList;
            GroupSet = groupList;
            RoleSet = roleList;
            ProjectSet = projectList;
            ProjectUserSet = projectUserList;
            ProjectRoleSet = projectRoleList;
            ProjectGroupSet = projectGroupList;
            ProjectUserRoleSet = projectUserRoleList;
        }

        [TestInitialize]
        public void SetUp()
        {
            InitializeDataSets();
            MockRepository = new Mock<IRepository>();
            MockGroupService = new Mock<IGroupService>();
            MockUserService = new Mock<IUserService>();
            MockRoleService = new Mock<IRoleService>();

            MockRepository.Setup(m => m.GetSet<User>()).Returns(() => UserSet);
            MockRepository.Setup(m => m.GetSet<Group>()).Returns(() => GroupSet);
            MockRepository.Setup(m => m.GetSet<Role>()).Returns(() => RoleSet);
            MockRepository.Setup(m => m.GetSet<Project>()).Returns(() => ProjectSet);
            MockRepository.Setup(m => m.GetSet<ProjectUser>()).Returns(() => ProjectUserSet);
            MockRepository.Setup(m => m.GetSet<ProjectRole>()).Returns(() => ProjectRoleSet);
            MockRepository.Setup(m => m.GetSet<ProjectGroup>()).Returns(() => ProjectGroupSet);
            MockRepository.Setup(m => m.GetSet<ProjectUserRole>()).Returns(() => ProjectUserRoleSet);

            MockUserService.Setup(m => m.FindUser(1)).Returns(() => UserSet.FirstOrDefault(u => u.Id == 1));
            MockUserService.Setup(m => m.FindUser(2)).Returns(() => UserSet.FirstOrDefault(u => u.Id == 2));
            MockUserService.Setup(m => m.FindUser(3)).Throws<NotFoundException>();

            MockRoleService.Setup(m => m.FindRole(1)).Returns(() => RoleSet.First(r => r.Id == 1));
            MockRoleService.Setup(m => m.FindRole(2)).Returns(() => RoleSet.First(r => r.Id == 2));
            MockRoleService.Setup(m => m.FindRole(3)).Throws<NotFoundException>();

            MockGroupService.Setup(m => m.FindGroup(1)).Returns(() => GroupSet.First(g => g.Id == 1));
            MockGroupService.Setup(m => m.FindGroup(2)).Returns(() => GroupSet.First(g => g.Id == 2));
            MockGroupService.Setup(m => m.FindGroup(3)).Throws<NotFoundException>();
        }

        [TestMethod]
        public void TestCreateProject()
        {
            var project = ProjectService.CreateProject("Project3", "Description3", 1);
            Assert.AreEqual("Project3", project.Name);
        }

        [TestMethod]
        public void TestCreateProjectWhenExists()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                ProjectService.CreateProject("Project2", "Description2", 1);
            });
        }

        [TestMethod]
        public void TestCreateProjectWhenUserNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() => 
            {
                ProjectService.CreateProject("Project3", "Description3", 3);
            });
        }

        [TestMethod]
        public void TestExistsProject()
        {
            var result = ProjectService.ExistsProject("Project1");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExistsProjectWhenNotFound()
        {
            var result = ProjectService.ExistsProject("Project3");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestAddProjectUser()
        {
            var projectUser = ProjectService.AddProjectUser(2, 2);
            Assert.AreEqual(2, projectUser.User.Id);
            Assert.AreEqual(2, projectUser.Project.Id);
        }

        [TestMethod]
        public void TestAddProjectUserWhenConflicting()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                ProjectService.AddProjectUser(1, 1);
            });
        }

        [TestMethod]
        public void TestAddProjectUserWhenUserIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddProjectUser(2, 3);
            });
        }

        [TestMethod]
        public void TestAddProjectUserWhenProjectIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddProjectUser(3, 1);
            });
        }

        [TestMethod]
        public void TestFindProject()
        {
            var project = ProjectService.FindProject(1);
            Assert.AreEqual(1, project.Id);
            Assert.AreEqual("Project1", project.Name);
        }

        [TestMethod]
        public void TestFindProjectWhenNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.FindProject(3);
            });
        }

        [TestMethod]
        public void TestFindProjectWithName()
        {
            var project = ProjectService.FindProject("project1");
            Assert.AreEqual(1, project.Id);
            Assert.AreEqual("Project1", project.Name);
        }

        [TestMethod]
        public void TestFindProjectWithNameWhenNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() => 
            {
                ProjectService.FindProject("Project3");
            });
        }

        [TestMethod]
        public void TestAddRole()
        {
            var projectRole = ProjectService.AddRole(2, 2);
            Assert.AreEqual(2, projectRole.Project.Id);
            Assert.AreEqual(2, projectRole.Role.Id);
        }

        [TestMethod]
        public void TestAddRoleWhenRoleIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddRole(2, 3);
            });
        }

        [TestMethod]
        public void TestAddRoleWhenProjectIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddRole(3, 1);
            });
        }

        [TestMethod]
        public void TestAddRoleWhenConflicting()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                ProjectService.AddRole(1, 1);
            });
        }

        [TestMethod]
        public void TestAddGroup()
        {
            var projectGroup = ProjectService.AddGroup(2, 2);
            Assert.AreEqual(2, projectGroup.Project.Id);
            Assert.AreEqual(2, projectGroup.Group.Id);
        }

        [TestMethod]
        public void TestAddGroupWhenGroupIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddGroup(2, 3);
            });
        }

        [TestMethod]
        public void TestAddGroupWhenProjectIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddGroup(3, 2);
            });
        }

        [TestMethod]
        public void TestAddGroupWhenConflicting()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                ProjectService.AddGroup(1, 1);
            });
        }

        [TestMethod]
        public void TestAddUserRole()
        {
            var projectUserRole = ProjectService.AddUserRole(1, 1);
            Assert.AreEqual(1, projectUserRole.ProjectUser.Id);
            Assert.AreEqual(1, projectUserRole.ProjectRole.Id);
        }

        [TestMethod]
        public void TestAddUserRoleWhenConflicting()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                ProjectService.AddUserRole(2, 2);
            });
        }

        [TestMethod]
        public void TestAddUserRoleWhenProjectUserIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddUserRole(3, 2);
            });
        }

        [TestMethod]
        public void TestAddUserRoleWhenProjectRoleIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddUserRole(2, 3);
            });
        }

        [TestMethod]
        public void TestAddProjectUserRole()
        {
            var projectUserRole = ProjectService.AddProjectUserRole(1, 1, 1);
            Assert.AreEqual(1, projectUserRole.ProjectUser.Project.Id);
            Assert.AreEqual(1, projectUserRole.ProjectRole.Project.Id);
            Assert.AreEqual(1, projectUserRole.ProjectUser.User.Id);
            Assert.AreEqual(1, projectUserRole.ProjectRole.Role.Id);
        }

        [TestMethod]
        public void TestAddProjectUserRoleWhenConflicting()
        {
            Assert.ThrowsException<ConflictException>(() =>
            {
                ProjectService.AddProjectUserRole(1, 2, 2);
            });
        }

        [TestMethod]
        public void TestAddProjectUserRoleWhenProjectIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddProjectUserRole(3, 1, 1);
            });
        }

        [TestMethod]
        public void TestAddProjectUserRoleWhenUserIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddProjectUserRole(1, 3, 1);
            });
        }

        [TestMethod]
        public void TestAddProjectUserRoleWhenRoleIsNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() =>
            {
                ProjectService.AddProjectUserRole(1, 1, 3);
            });
        }

        [TestMethod]
        public void TestExistsProjectUserRole()
        {
            var result = ProjectService.ExistsProjectUserRole(1, 2, 2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExistsProjectUserRoleWhenProjectIsNotMapped()
        {
            var result = ProjectService.ExistsProjectUserRole(2, 2, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExistsProjectUserRoleWhenUserIsNotMapped()
        {
            var result = ProjectService.ExistsProjectUserRole(1, 1, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExistsProjectUserRoleWhenRoleIsNotMapped()
        {
            var result = ProjectService.ExistsProjectUserRole(1, 2, 1);
            Assert.IsFalse(result);
        }
    }
}
