using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Services
{
    public class CodeService : ICodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;

        public CodeService(IUnitOfWork unitOfWork, IMailService mailService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _userManager = userManager;
        }

        public async Task<bool> SendAsync(string email, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var code = await GenerateAsync(user.Id, cancellationToken);

                return await _mailService.SendForgotPasswordMailAsync(user.Email, user.Id, code);
            }
            throw new NotFoundException("User Not Found");
        }

        public async Task<string> GenerateAsync(string userId, CancellationToken cancellationToken)
        {
            var userVerificationRepository = _unitOfWork.Repository<VerificationCode>();

            Random random = new();
            var code = random.Next(100000, 999999);

            VerificationCode verification = new(code.ToString(), userId);

            await userVerificationRepository.AddAsync(verification);

            return await _unitOfWork.SaveAsync(cancellationToken) switch
            {
                > 0 => code.ToString(),
                _ => null
            };
        }

        public async Task<bool> VerifyAsync(string email, string code, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var verification = _unitOfWork.Repository<VerificationCode>();
                var userVerification = await verification.Entities.Where(x => x.UserId == user.Id && x.Value == code && x.ExpireDate <= DateTime.Now && !x.IsVerified).FirstOrDefaultAsync(cancellationToken);
                if (userVerification is not null)
                {
                    userVerification.IsVerified = true;
                    userVerification.AddLastModifier(user.Id);

                    await verification.UpdateAsync(userVerification);
                    return await _unitOfWork.SaveAsync(cancellationToken) switch
                    {
                        > 0 => true,
                        _ => false
                    };
                }
                return false;
            }
            throw new NotFoundException("User Not Found");
        }

        public async Task<bool> IsVerifiedAsync(string userId, string code, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var verification = _unitOfWork.Repository<VerificationCode>();
                return await verification.Entities.AnyAsync(x => x.UserId == userId && x.Value == code && x.IsVerified && x.UpdatedDate.Value.DayOfYear == DateTime.Now.DayOfYear, cancellationToken);
            }
            throw new NotFoundException("User Not Found");
        }
    }
}
