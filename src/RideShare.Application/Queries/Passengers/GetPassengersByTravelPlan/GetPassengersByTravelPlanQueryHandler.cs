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

namespace RideShare.Application.Queries.Passengers.GetPassengersByTravelPlan
{
    public class GetPassengersByTravelPlanQueryHandler : IRequestHandler<GetPassengersByTravelPlanRequest, List<GetPassengersByTravelPlanResponse>>
    {
        private readonly IRideShareDbContext _context;
        private readonly IMapper _mapper;

        public GetPassengersByTravelPlanQueryHandler(IRideShareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetPassengersByTravelPlanResponse>> Handle(GetPassengersByTravelPlanRequest request, CancellationToken cancellationToken)
        {
            var plan = await _context.TravelPlans
                .Where(x => x.Id == request.TravelPlanId)
                .Include(x => x.Demands)
                .ThenInclude(x => x.Passenger)
                .FirstOrDefaultAsync();
            var passengers = plan.Passengers;

            return _mapper.Map<List<GetPassengersByTravelPlanResponse>>(passengers);
        }
    }

    public class GetPassengersByTravelPlanRequest : IRequest<List<GetPassengersByTravelPlanResponse>>
    {
        public int TravelPlanId { get; set; }
    }

    public class GetPassengersByTravelPlanResponse
    {
        public string PassengerName { get; set; }
    }
}