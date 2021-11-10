using FluentValidation;
using RideShare.Application.Commands.Passengers.CreatePassenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.Passenger
{
    public class CreatePassengerValidator : AbstractValidator<CreatePassengerRequest>
    {
        public CreatePassengerValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
        }
    }
}
