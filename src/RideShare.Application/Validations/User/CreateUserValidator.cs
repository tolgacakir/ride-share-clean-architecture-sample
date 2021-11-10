using FluentValidation;
using RideShare.Application.Commands.Users.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().NotEmpty()
                .WithMessage("Username cannot be null or empty")
                .Length(3,10)
                .WithMessage("Username length must be between 3 and 10");
            RuleFor(x => x.Password)
                .NotNull().NotEmpty()
                .WithMessage("Password cannot be null or empty")
                .Length(4, 10)
                .WithMessage("Password length must be between 4 and 10");

        }
    }
}
