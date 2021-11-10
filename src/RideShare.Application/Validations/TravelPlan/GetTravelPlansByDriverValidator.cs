using FluentValidation;
using RideShare.Application.Queries.TravelPlans.GetTravelPlansByDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelPlan
{
    public class GetTravelPlansByDriverValidator : AbstractValidator<GetTravelPlansByDriverRequest>
    {
        public GetTravelPlansByDriverValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId cannot be null or empty.");
        }
    }
}
