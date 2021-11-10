using FluentValidation;
using RideShare.Application.Commands.Users.UpdateUserPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.User
{
    public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordRequest>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
            RuleFor(x => x.Password)
                .NotNull().NotEmpty()
                .WithMessage("Password cannot be null or empty")
                .Length(4, 10)
                .WithMessage("Password length must be between 4 and 10");
        }
    }
}
