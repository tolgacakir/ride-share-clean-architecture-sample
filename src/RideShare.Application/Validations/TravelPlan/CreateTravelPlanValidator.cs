using FluentValidation;
using RideShare.Application.Commands.TravelPlans.CreateTravelPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideShare.Application.Validations.TravelPlan
{
    public class CreateTravelPlanValidator : AbstractValidator<CreateTravelPlanRequest>
    {
        public CreateTravelPlanValidator()
        {
            RuleFor(x => x.Caption)
                .NotNull().NotEmpty()
                .WithMessage("Caption cannot be null or empty")
                .Length(4, 20)
                .WithMessage("Caption length must be between 4 and 20");

            RuleFor(x => x.From)
                .NotNull().NotEmpty()
                .WithMessage("'From' cannot be null or empty")
                .Length(3, 20)
                .WithMessage("'From' length must be between 4 and 20");

            RuleFor(x => x.To)
                .NotNull().NotEmpty()
                .WithMessage("'To' cannot be null or empty")
                .Length(3, 20)
                .WithMessage("To length must be between 4 and 20");

            RuleFor(x => x.StartAt)
                .NotNull().NotEmpty()
                .WithMessage("'StartAt' cannot be null or empty")
                .GreaterThan(DateTime.Now)
                .WithMessage("'StartAt' must be greather than now");


                

        }
    }
}
