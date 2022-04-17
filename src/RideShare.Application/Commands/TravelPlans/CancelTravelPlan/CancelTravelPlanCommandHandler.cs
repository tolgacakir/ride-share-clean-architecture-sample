using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Application.Exception;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.TravelPlans.CancelTravelPlan
{
    public class CancelTravelPlanCommandHandler : IRequestHandler<CancelTravelPlanRequest, CancelTravelPlanResponse>
    {
        private readonly IRideShareDbContext _context;

        public CancelTravelPlanCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<CancelTravelPlanResponse> Handle(CancelTravelPlanRequest request, CancellationToken cancellationToken)
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

            driver.CancelTravelPlan(plan);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return new CancelTravelPlanResponse{
                Result = result
            };   
        }
    }

    public class CancelTravelPlanRequest : IRequest<CancelTravelPlanResponse> 
    {
        public Guid UserId { get; set; }
        public int TravelPlanId { get; set; }
    }

    public class CancelTravelPlanResponse
    {
        public int Result { get; set; }
    }
}