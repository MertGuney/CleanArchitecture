using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Commands.Verification.SendCode
{
    public class SendCodeCommandValidator : AbstractValidator<SendCodeCommandRequest>
    {
        public SendCodeCommandValidator()
        {

        }
    }
}
