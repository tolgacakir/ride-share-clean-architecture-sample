using FluentValidation;
using RideShare.Application.Queries.Passengers.GetPassengersByTravelPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.Passenger
{
    public class GetPassengersByTravelPlanValidator : AbstractValidator<GetPassengersByTravelPlanRequest>
    {
        public GetPassengersByTravelPlanValidator()
        {
            RuleFor(x => x.TravelPlanId)
                .GreaterThan(0)
                .WithMessage("TravelPlanId must be greater than zero.");
        }
    }
}
