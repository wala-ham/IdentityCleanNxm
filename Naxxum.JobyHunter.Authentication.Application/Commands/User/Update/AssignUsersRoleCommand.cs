using MediatR;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.Role;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxxum.JobyHunter.Authentication.Application.Commands.User.Update
{
    public class AssignUsersRoleCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class AssignUsersRoleCommandHandler : IRequestHandler<AssignUsersRoleCommand, int>
    {
        private readonly IIdentityServiceUser _identityService;

        public AssignUsersRoleCommandHandler(IIdentityServiceUser identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(AssignUsersRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AssignUserToRole(request.UserName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
