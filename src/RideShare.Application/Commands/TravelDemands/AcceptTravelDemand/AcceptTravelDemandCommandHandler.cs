using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace RideShare.Application.Commands.TravelDemands.AcceptTravelDemand
{
    public class AcceptTravelDemandCommandHandler : IRequestHandler<AcceptTravelDemandRequest, AcceptTravelDemandResponse>
    {
        private readonly IRideShareDbContext _context;

        public AcceptTravelDemandCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<AcceptTravelDemandResponse> Handle(AcceptTravelDemandRequest request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers
                .Include(x => x.TravelPlans)
                .ThenInclude(x => x.Demands)
                .Where(x => x.Id == request.DriverId)
                .FirstOrDefaultAsync();

            var plan = driver.ActiveTravelPlans
                .Where(x => x.Id == request.TravelPlanId)
                .FirstOrDefault();

            var demand = plan.AwaitingDemands
                .Where(x => x.Id == request.TravelDemandId)
                .FirstOrDefault();

            driver.AcceptDemand(plan, demand);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return new AcceptTravelDemandResponse
            {
                Result = result
            };
        }
    }

    public class AcceptTravelDemandRequest : IRequest<AcceptTravelDemandResponse>
    {
        public int DriverId { get; set; }
        public int TravelPlanId { get; set; }
        public int TravelDemandId { get; set; }
    }

    public class AcceptTravelDemandResponse
    {
        public int Result { get; set; }
    }
}