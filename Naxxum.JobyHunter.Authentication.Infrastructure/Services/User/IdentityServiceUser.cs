using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Naxxum.JobyHunter.Authentication.Application.Common.Exceptions;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.Role;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.User;
using Naxxum.JobyHunter.Authentication.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxxum.JobyHunter.Authentication.Infrastructure.Services.User
{
    

    public class IdentityServiceUser : IIdentityServiceUser
    {
        private readonly UserManager<ApplicationUser> _userManager;



        public IdentityServiceUser(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;


        }

        public async Task<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }



        // Return multiple value
        public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName, List<string> roles)
        {
            var user = new ApplicationUser()
            {
                FullName = fullName,
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }

            var addUserRole = await _userManager.AddToRolesAsync(user, roles);
            if (!addUserRole.Succeeded)
            {
                throw new ValidationException(addUserRole.Errors);
            }
            return (result.Succeeded, user.Id);
        }




        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
                //throw new BadRequestException("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.FullName,
                x.UserName,
                x.Email
            }).ToListAsync();

            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email)).ToList();
        }

        public Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync()
        {
            throw new NotImplementedException();

            //var roles = await _userManager.GetRolesAsync(user);
            //return (user.Id, user.UserName, user.Email, roles);

            //var users = _userManager.Users.ToListAsync();
        }



        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }
            return await _userManager.GetUserIdAsync(user);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }
            return await _userManager.GetUserNameAsync(user);
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }



        public async Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.FullName = fullName;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }



        public async Task<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            result = await _userManager.AddToRolesAsync(user, usersRole);

            return result.Succeeded;
        }
    }
}
