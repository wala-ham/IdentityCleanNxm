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
    public class GetUserDetailsByUserNameQuery : IRequest<UserDetailsResponseDTO>
    {
        public string UserName { get; set; }
    }

    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityServiceUser _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityServiceUser identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserDetailsResponseDTO() { Id = userId, FullName = fullName, UserName = userName, Email = email, Roles = roles };
        }
    }
}
