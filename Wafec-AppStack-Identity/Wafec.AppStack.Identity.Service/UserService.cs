using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafec.AppStack.Identity.Core;
using Wafec.AppStack.Identity.Core.Database;

namespace Wafec.AppStack.Identity.Service
{
    public class UserService : BaseService, IUserService
    {
        public IRepository Repository { get; private set; }
        public IPasswordService PasswordService { get; private set; }
        public IRoleService RoleService { get; set; }

        public PasswordAlgorithms CurrentPasswordAlgorithm
        {
            get
            {
                return PasswordAlgorithms.SHA256;
            }
        }

        public PasswordLevels CurrentPasswordLevel
        {
            get
            {
                return PasswordLevels.STRONG;
            }
        }

        public UserService(IRepository repository,
                           IPasswordService passwordService,
                           IRoleService roleService)
        {
            Repository = repository;
            PasswordService = passwordService;
            RoleService = roleService;
        }

        private PasswordAlgorithm CreateIfNotExistsOrGet(PasswordAlgorithms algorithm)
        {
            var name = Enum.GetName(typeof(PasswordAlgorithms), algorithm);
            var passwordAlgorithm = Repository.GetSet<PasswordAlgorithm>().FirstOrDefault(p => p.Name.ToLower().Equals(name.ToLower()));
            if (passwordAlgorithm == null)
            {
                passwordAlgorithm = new PasswordAlgorithm();
                passwordAlgorithm.Name = name;
                Repository.Add(passwordAlgorithm);
            }
            return passwordAlgorithm;
        }

        private PasswordLevel CreateIfNotExistsOrGet(PasswordLevels level)
        {
            var name = Enum.GetName(typeof(PasswordLevels), level);
            var passwordLevel = Repository.GetSet<PasswordLevel>().FirstOrDefault(p => p.Name.ToLower().Equals(name.ToLower()));
            if (passwordLevel == null)
            {
                passwordLevel = new PasswordLevel();
                passwordLevel.Name = name;
                Repository.Add(passwordLevel);
            }
            return passwordLevel;
        }

        public User CreateUser(string name, string password)
        {
            ThrowsIfEmpty(name, password);

            if (!ExistsUser(name))
            {
                if (!PasswordService.IsStrongEnough(password, CurrentPasswordLevel))
                {
                    throw new WeakPasswordException();
                }
                else
                {
                    User user = new User()
                    {
                        Name = name,
                        Password = PasswordService.GenerateHash(password, CurrentPasswordAlgorithm),
                        PasswordAlgorithm = CreateIfNotExistsOrGet(CurrentPasswordAlgorithm),
                        PasswordLevel = CreateIfNotExistsOrGet(CurrentPasswordLevel)
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

        public bool ExistsUser(string name)
        {
            return Repository.GetSet<User>().Any(u => u.Deleted == false && u.Name.ToLower().Equals(name.ToLower()));
        }

        public User FindUser(string name, string password)
        {
            ThrowsIfEmpty(name, password);
            var userView = FindUserView(name);
            PasswordAlgorithms algorithm = (PasswordAlgorithms) Enum.Parse(typeof(PasswordAlgorithms), userView.PasswordAlgorithmName);
            var passwordHash = PasswordService.GenerateHash(password, algorithm);
            if (userView.Name.ToLower().Equals(name.ToLower()) && userView.Password.Equals(passwordHash))
            {
                return userView.User;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public UserRole AddUserRole(long userId, long roleId)
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
            User user = Repository.GetSet<User>().FirstOrDefault(u => u.Deleted == false && u.Id == id);
            if (user != null)
                return user;
            else
                throw new NotFoundException();
        }

        public User ChangePassword(long id, string currentPassword, string newPassword)
        {
            ThrowsIfEmpty(currentPassword, newPassword);
            var user = FindUser(id);
            var currentAlgorithm = (PasswordAlgorithms) Enum.Parse(typeof(PasswordAlgorithms), user.PasswordAlgorithm.Name);
            if (user.Password != PasswordService.GenerateHash(currentPassword, currentAlgorithm))
                throw new InvalidDataException();
            if (PasswordService.IsStrongEnough(newPassword, CurrentPasswordLevel))
            {
                user.Password = PasswordService.GenerateHash(newPassword, CurrentPasswordAlgorithm);
                user.PasswordAlgorithm = CreateIfNotExistsOrGet(CurrentPasswordAlgorithm);
                user.PasswordLevel = CreateIfNotExistsOrGet(CurrentPasswordLevel);
                user = Repository.Update(user);
                return user;
            }
            else
            {
                throw new WeakPasswordException();
            }
        }

        public void DeleteUser(long id)
        {
            var user = FindUser(id);
            user.Deleted = true;
            user = Repository.Update(user);
        }

        public User FindUser(string name)
        {
            var user = Repository.GetSet<User>().FirstOrDefault(u => u.Deleted == false && u.Name.ToLower().Equals(name?.ToLower()));
            if (user != null)
                return user;
            else
                throw new NotFoundException();
        }

        private UserViewInternal FindUserView(string name)
        {
            ThrowsIfEmpty(name);

            var userView = Repository.GetSet<User>()
                .Where(u => u.Deleted == false && u.Name.ToLower().Equals(name?.ToLower()))
                .Join(
                    Repository.GetSet<PasswordAlgorithm>(),
                    u => u.Id,
                    p => p.Id,
                    (user, password) =>
                    {
                        return new UserViewInternal()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Password = user.Password,
                            PasswordAlgorithmName = password.Name,
                            User = user
                        };
                    }
                )
                .FirstOrDefault();
            if (userView != null)
                return userView;
            else
                throw new NotFoundException();
        }

        public bool ExistsUser(string name, string password)
        {
            return FindUser(name, password) != null;
        }

        public void RemoveUserRole(long userId, long roleId)
        {
            var userRole = FindUserRole(userId, roleId);
            userRole.Deleted = true;
            Repository.Update(userRole);
        }

        public UserRole FindUserRole(long userId, long roleId)
        {
            var userRole = Repository.GetSet<UserRole>().FirstOrDefault(ur => ur.Deleted == false && ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
                return userRole;
            else
                throw new NotFoundException();
        }

        class UserViewInternal
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string PasswordAlgorithmName { get; set; }
            public virtual User User { get; set; }
        }
    }
}
