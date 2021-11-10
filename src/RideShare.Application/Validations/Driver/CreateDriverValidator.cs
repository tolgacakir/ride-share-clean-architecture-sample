using FluentValidation;
using RideShare.Application.Commands.Drivers.CreateDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.Driver
{
    public class CreateDriverValidator : AbstractValidator<CreateDriverRequest>
    {
        public CreateDriverValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
        }
    }
}
