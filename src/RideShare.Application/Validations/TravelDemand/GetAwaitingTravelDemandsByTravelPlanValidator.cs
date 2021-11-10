using FluentValidation;
using RideShare.Application.Queries.TravelDemands.GetAwaitingTravelDemandsByTravelPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelDemand
{
    public class GetAwaitingTravelDemandsByTravelPlanValidator : AbstractValidator<GetAwaitingTravelDemandsByTravelPlanRequest>
    {
        public GetAwaitingTravelDemandsByTravelPlanValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
            RuleFor(x => x.TravelPlanId)
                .GreaterThan(0)
                .WithMessage("TravelPlanId must be greater than zero.");
        }
    }
}
