using MediatR;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.Role;
using Naxxum.JobyHunter.Authentication.Application.Common.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxxum.JobyHunter.Authentication.Application.Commands.User.Delete
{
    public class DeleteUserCommand : IRequest<int>
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IIdentityServiceUser _identityService;

        public DeleteUserCommandHandler(IIdentityServiceUser identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteUserAsync(request.Id);

            return result ? 1 : 0;
        }
    }
}
