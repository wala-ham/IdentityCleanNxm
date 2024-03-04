using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.Role
{
    public interface IIdentityService
    {


        // Role Section
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<bool> UpdateRole(string id, string roleName);



    }
}
