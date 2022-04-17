using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Application.Exception;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.TravelPlans.FinishTravelPlan
{
    public class FinishTravelPlanCommandHandler : IRequestHandler<FinishTravelPlanRequest, FinishTravelPlanResponse>
    {
        private readonly IRideShareDbContext _context;

        public FinishTravelPlanCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<FinishTravelPlanResponse> Handle(FinishTravelPlanRequest request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.Include(x => x.TravelPlans)
                .Where(x=>x.User.Id == request.UserId)
                .FirstOrDefaultAsync();

            if (driver is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(Driver).Name));
            }

            var plan = driver.ActiveTravelPlans.Where(x=>x.Id == request.TravelPlanId).FirstOrDefault();

            if (plan is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(TravelPlan).Name));
            }

            driver.FinishTravelPlan(plan);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return new FinishTravelPlanResponse{
                Result = result
            };
        }
    }

    public class FinishTravelPlanRequest : IRequest<FinishTravelPlanResponse> 
    {
        public Guid UserId { get; set; }
        public int TravelPlanId { get; set; }
    }

    public class FinishTravelPlanResponse
    {
        public int Result { get; set; }
    }
}