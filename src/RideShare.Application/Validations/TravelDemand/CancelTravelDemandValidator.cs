using FluentValidation;
using RideShare.Application.Commands.TravelDemands.CancelTravelDemand;
using RideShare.Application.Commands.TravelDemands.CreateTravelDemand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelDemand
{
    public class CancelTravelDemandValidator : AbstractValidator<CancelTravelDemandRequest>
    {
        public CancelTravelDemandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
            RuleFor(x => x.TravelPlanId)
                .GreaterThan(0)
                .WithMessage("TravelPlanId must be greater than zero.");
            RuleFor(x => x.TravelDemandId)
                .GreaterThan(0)
                .WithMessage("TravelPlanId must be greater than zero.");
        }
    }
}
