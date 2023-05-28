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
        private readonly UserManager<User> _userManager;

        public CodeService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<string> GenerateAsync(string userId, CancellationToken cancellationToken)
        {
            var userVerificationRepository = _unitOfWork.Repository<UserVerification>();

            var isExist = await userVerificationRepository.Entities.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            var uniqueCode = await GetUniqueCodeAsync(userId, userVerificationRepository, cancellationToken);

            if (isExist is null)
            {
                UserVerification verification = new()
                {
                    UserId = userId,
                    Code = uniqueCode,
                    ExpireDate = DateTime.Now.AddSeconds(200),
                };
                await userVerificationRepository.AddAsync(verification);
            }
            else
            {
                isExist.Code = uniqueCode;
                isExist.ExpireDate = DateTime.Now.AddSeconds(200);
                isExist.AddLastModifier(userId);

                await userVerificationRepository.UpdateAsync(isExist);
            }
            await _unitOfWork.SaveAsync(cancellationToken);
            return uniqueCode;
        }

        public async Task<bool> VerifyAsync(string userId, string code, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var verification = _unitOfWork.Repository<UserVerification>();
                return await verification.Entities.AnyAsync(x => x.UserId == userId && x.Code == code && x.ExpireDate <= DateTime.Now, cancellationToken);
            }
            throw new NotFoundException("User Not Found");
        }

        private static async Task<string> GetUniqueCodeAsync(string userId, IGenericRepository<UserVerification> userVerificationRepository, CancellationToken cancellationToken)
        {
            int code = 0;
            bool isUnique = false;
            Random random = new();
            while (!isUnique)
            {
                code = random.Next(100000, 999999);

                var isExist = await userVerificationRepository.Entities.AnyAsync(x => x.UserId == userId && x.Code == code.ToString(), cancellationToken);
                if (!isExist)
                {
                    isUnique = true;
                    break;
                }
            }
            return code.ToString();
        }
    }
}
