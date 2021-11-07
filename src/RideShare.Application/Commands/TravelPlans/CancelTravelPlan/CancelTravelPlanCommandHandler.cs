using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;

namespace RideShare.Application.Commands.TravelPlans.CancelTravelPlan
{
    public class CancelTravelPlanCommandHandler : IRequestHandler<CancelTravelPlanRequest, CancelTravelPlanResponse>
    {
        private readonly IRideShareDbContext _context;
        public async Task<CancelTravelPlanResponse> Handle(CancelTravelPlanRequest request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.Include(x => x.TravelPlans)
                .Where(x=>x.Id == request.DriverId)
                .FirstOrDefaultAsync();

            var plan = driver.ActiveTravelPlans.Where(x=>x.Id == request.TravelPlanId).FirstOrDefault();
            driver.CancelTravelPlan(plan);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return new CancelTravelPlanResponse{
                Result = result
            };   
        }
    }

    public class CancelTravelPlanRequest : IRequest<CancelTravelPlanResponse> 
    {
        public int TravelPlanId { get; set; }
        public int DriverId { get; set; }
    }

    public class CancelTravelPlanResponse
    {
        public int Result { get; set; }
    }
}