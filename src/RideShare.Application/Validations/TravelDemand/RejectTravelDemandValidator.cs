using FluentValidation;
using RideShare.Application.Commands.TravelDemands.RejectTravelDemand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelDemand
{
    public class RejectTravelDemandValidator : AbstractValidator<RejectTravelDemandRequest>
    {
        public RejectTravelDemandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
            RuleFor(x => x.TravelDemandId)
                .GreaterThan(0)
                .WithMessage("TravelDemandId must be greater than zero.");
            RuleFor(x => x.TravelPlanId)
                .GreaterThan(0)
                .WithMessage("TravelPlanId must be greater than zero.");
        }
    }
}
