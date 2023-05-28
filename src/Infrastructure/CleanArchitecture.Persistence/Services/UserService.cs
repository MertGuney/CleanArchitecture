using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public UserService(IMailService mailService, UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _mailService = mailService;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<bool> ResetPasswordAsync()
        {
            User user = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = token.UrlEncode();

                return await _mailService.SendResetPasswordMailAsync(user.Email, user.Id, token);
            }
            throw new NotFoundException("User Not Found");
        }

        public async Task<bool> VerifyResetPasswordTokenAsync(string userId, string token)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                token = token.UrlDecode();
                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            }
            throw new NotFoundException("User Not Found");
        }
    }
}
