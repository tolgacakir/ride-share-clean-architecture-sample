using FluentValidation;
using RideShare.Application.Queries.TravelPlans.GetTravelPlansByPassenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelPlan
{
    public class GetTravelPlansByPassengerValidator : AbstractValidator<GetTravelPlansByPassengerRequest>
    {
        public GetTravelPlansByPassengerValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
        }
    }
}
