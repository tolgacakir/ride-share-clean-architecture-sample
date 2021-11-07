using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;

namespace RideShare.Application.Commands.TravelPlans.FinishTravelPlan
{
    public class FinishTravelPlanCommandHandler : IRequestHandler<FinishTravelPlanRequest, FinishTravelPlanResponse>
    {
        private readonly IRideShareDbContext _context;
        public async Task<FinishTravelPlanResponse> Handle(FinishTravelPlanRequest request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.Include(x => x.TravelPlans)
                .Where(x=>x.Id == request.DriverId)
                .FirstOrDefaultAsync();

            var plan = driver.ActiveTravelPlans.Where(x=>x.Id == request.TravelPlanId).FirstOrDefault();
            driver.FinishTravelPlan(plan);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return new FinishTravelPlanResponse{
                Result = result
            };
        }
    }

    public class FinishTravelPlanRequest : IRequest<FinishTravelPlanResponse> 
    {
        public int TravelPlanId { get; set; }
        public int DriverId { get; set; }
    }

    public class FinishTravelPlanResponse
    {
        public int Result { get; set; }
    }
}