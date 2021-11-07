using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace RideShare.Application.Commands.TravelDemands.CancelTravelDemand
{
    public class CancelTravelDemandCommandHandler : IRequestHandler<CancelTravelDemandRequest, CancelTravelDemandResponse>
    {
        private readonly IRideShareDbContext _context;

        public CancelTravelDemandCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<CancelTravelDemandResponse> Handle(CancelTravelDemandRequest request, CancellationToken cancellationToken)
        {
            var passenger = await _context.Passengers
                .Include(x => x.Demands)
                .Where(x => x.Id == request.PassengerId)
                .FirstOrDefaultAsync();

            var demand = passenger.Demands
                .Where(x => x.Id == request.TravelDemandId)
                .FirstOrDefault();

            passenger.CancelDemand(demand);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return new CancelTravelDemandResponse
            {
                Result = result
            };
        }
    }

    public class CancelTravelDemandRequest : IRequest<CancelTravelDemandResponse>
    {
        public int PassengerId { get; set; }
        public int TravelPlanId { get; set; }
        public int TravelDemandId { get; set; }
    }

    public class CancelTravelDemandResponse
    {
        public int Result { get; set; }
    }
}