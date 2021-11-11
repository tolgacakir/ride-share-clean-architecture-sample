using RideShare.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace RideShare.Application.Queries.TravelDemands.GetAwaitingTravelDemandsByTravelPlan
{
    public class GetAwaitingTravelDemandsByTravelPlanQueryHandlerQueryHandler : IRequestHandler<GetAwaitingTravelDemandsByTravelPlanRequest, List<GetAwaitingTravelDemandsByTravelPlanResponse>>
    {
        private readonly IRideShareDbContext _context;
        private readonly IMapper _mapper;

        public GetAwaitingTravelDemandsByTravelPlanQueryHandlerQueryHandler(IRideShareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAwaitingTravelDemandsByTravelPlanResponse>> Handle(GetAwaitingTravelDemandsByTravelPlanRequest request, CancellationToken cancellationToken)
        {
            var travelDemands = await _context.TravelDemands
                .Where(x => x.Status == DemandStatuses.Awaiting 
                    && x.Plan.Id == request.TravelPlanId 
                    && (x.Passenger.User.Id == request.UserId || x.Plan.Driver.User.Id == request.UserId))
                .Include(x=>x.Passenger)
                .ThenInclude(x=>x.User)
                .ToListAsync();

            return _mapper.Map<List<GetAwaitingTravelDemandsByTravelPlanResponse>>(travelDemands);
        }
    }

    public class GetAwaitingTravelDemandsByTravelPlanRequest : IRequest<List<GetAwaitingTravelDemandsByTravelPlanResponse>>
    {
        public Guid UserId { get; set; }
        public int TravelPlanId { get; set; }
    }

    public class GetAwaitingTravelDemandsByTravelPlanResponse
    {
        public int TravelDemandId { get; set; }
        public string PassengerName { get; set; }

    }
}