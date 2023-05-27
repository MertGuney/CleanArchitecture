using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Commands.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<NoContentModel>>
    {
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseModel<NoContentModel>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var existUser = await _userManager.FindByEmailAsync(request.Email);
            if (existUser is not null) throw new CustomApplicationException("Exist User");

            User user = new()
            {
                Email = request.Email,
                UserName = request.UserName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new CustomApplicationException("An error occurred while user creating.");

            return ResponseModel<NoContentModel>.Success(StatusCodes.Status201Created);
        }
    }
}
