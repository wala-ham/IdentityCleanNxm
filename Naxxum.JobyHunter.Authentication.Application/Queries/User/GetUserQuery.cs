using MediatR;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.Role;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.User;
using Naxxum.JobyHunter.Authentication.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxxum.JobyHunter.Authentication.Application.Queries.User
{
    public class GetUserQuery : IRequest<List<UserResponseDTO>>
    {
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserResponseDTO>>
    {
        private readonly IIdentityServiceUser _identityService;

        public GetUserQueryHandler(IIdentityServiceUser identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserResponseDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync();
            return users.Select(x => new UserResponseDTO()
            {
                Id = x.id,
                FullName = x.fullName,
                UserName = x.userName,
                Email = x.email
            }).ToList();
        }
    }
}
