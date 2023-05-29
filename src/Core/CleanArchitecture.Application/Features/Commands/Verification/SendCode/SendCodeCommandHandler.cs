using CleanArchitecture.Application.Common.Constraints;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Commands.Verification.SendCode
{
    public class SendCodeCommandHandler : IRequestHandler<SendCodeCommandRequest, ResponseModel<NoContentModel>>
    {
        private readonly ICodeService _codeService;

        public SendCodeCommandHandler(ICodeService codeService)
        {
            _codeService = codeService;
        }

        public async Task<ResponseModel<NoContentModel>> Handle(SendCodeCommandRequest request, CancellationToken cancellationToken)
        {
            return await _codeService.SendAsync(request.Email, cancellationToken)
                ? await ResponseModel<NoContentModel>.SuccessAsync()
                : await ResponseModel<NoContentModel>.FailureAsync(ErrorCode.SendValidationCode, "Validation Code Error", "An error occurred while sending code.", StatusCodes.Status500InternalServerError);
        }
    }
}
