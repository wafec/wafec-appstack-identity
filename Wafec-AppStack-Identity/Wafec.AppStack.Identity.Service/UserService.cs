using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class UserService : IUserService
    {
        public IRepository Repository { get; private set; }
        public IPasswordService PasswordService { get; private set; }
        public IRoleService RoleService { get; set; }

        public UserService(IRepository repository,
                           IPasswordService passwordService,
                           IRoleService roleService)
        {
            Repository = repository;
            PasswordService = passwordService;
            RoleService = roleService;
        }

        public User CreateUser(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException();

            if (!UserExists(name))
            {
                if (!PasswordService.IsStrongEnough(password))
                {
                    throw new WeakPasswordException();
                }
                else
                {
                    User user = new User()
                    {
                        Name = name,
                        Password = PasswordService.GenerateHash(password)
                    };
                    Repository.Add(user);
                    return user;
                }
            }
            else
            {
                throw new ConflictException();
            }
        }

        public bool UserExists(string name)
        {
            return Repository.GetSet<User>().Any(u => u.Name.ToLower().Equals(name.ToLower()));
        }

        public bool UserExists(string name, string password)
        {
            return Repository.GetSet<User>().Any(u => u.Name.ToLower().Equals(name.ToLower()) && u.Password.Equals(password));
        }

        public UserRole AddRole(long userId, long roleId)
        {
            if (!Repository.GetSet<UserRole>().Any(ur => ur.RoleId == roleId && ur.UserId == userId))
            {
                User user = FindUser(userId);
                Role role = RoleService.FindRole(roleId);
                UserRole userRole = new UserRole();
                userRole.Role = role;
                userRole.User = user;
                Repository.Add(userRole);
                return userRole;
            }
            else
            {
                throw new ConflictException();
            }
        }

        public User FindUser(long id)
        {
            User user = Repository.GetSet<User>().FirstOrDefault(u => u.Id == id);
            if (user != null)
                return user;
            else
                throw new NotFoundException();
        }
    }
}
