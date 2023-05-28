using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICodeService _codeService;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(ICodeService codeService, IMailService mailService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _codeService = codeService;
            _mailService = mailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> ForgotPasswordAsync(string email, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var code = await _codeService.GenerateAsync(user.Id, cancellationToken);

                return await _mailService.SendForgotPasswordMailAsync(user.Email, user.Id, code);
            }
            throw new NotFoundException("User Not Found");
        }

        public async Task<bool> ForgotPasswordAsync(string email, string newPassword)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (!result.Succeeded) throw new CustomApplicationException("Reset Password Exception");

                IdentityResult securityResult = await _userManager.UpdateSecurityStampAsync(user);
                if (!securityResult.Succeeded) throw new CustomApplicationException("Security Stamp Result");

                return true;
            }
            throw new NotFoundException("User Not Found");
        }
    }
}
