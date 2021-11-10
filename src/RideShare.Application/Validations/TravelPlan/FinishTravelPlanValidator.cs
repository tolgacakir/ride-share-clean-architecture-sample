using FluentValidation;
using RideShare.Application.Commands.TravelPlans.FinishTravelPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelPlan
{
    public class FinishTravelPlanValidator : AbstractValidator<FinishTravelPlanRequest>
    {
        public FinishTravelPlanValidator()
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
