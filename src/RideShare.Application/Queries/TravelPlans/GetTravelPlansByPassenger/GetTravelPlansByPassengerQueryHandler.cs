using RideShare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using System;

namespace RideShare.Application.Queries.TravelPlans.GetTravelPlansByPassenger
{
    public class GetTravelPlansByPassengerQueryHandler : IRequestHandler<GetTravelPlansByPassengerRequest, List<GetTravelPlansByPassengerResponse>>
    {
        private readonly IRideShareDbContext _context;
        private readonly IMapper _mapper;

        public GetTravelPlansByPassengerQueryHandler(IRideShareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTravelPlansByPassengerResponse>> Handle(GetTravelPlansByPassengerRequest request, CancellationToken cancellationToken)
        {
            var travelPlans = await _context.TravelPlans
                .Include(x => x.Demands)
                .ThenInclude(x => x.Passenger)
                .ThenInclude(x => x.User)
                .Where(x => x.Demands.Any(x => x.Passenger.User.Id == request.UserId && x.Status == DemandStatuses.Accepted))
                .OrderBy(x => x.Status)
                .ThenBy(x => x.StartAt)
                .ToListAsync();

            return _mapper.Map<List<GetTravelPlansByPassengerResponse>>(travelPlans);
        }
    }

    public class GetTravelPlansByPassengerRequest : IRequest<List<GetTravelPlansByPassengerResponse>>
    {
        public Guid UserId { get; set; }
    }

    public class GetTravelPlansByPassengerResponse
    {
        public string Caption { get; set; }
        public byte EmptySeat { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime StartAt { get; set; }
        public string DriverName { get; set; }
        public IReadOnlyCollection<string> PassengerNames { get; set; }
        public string Status { get; set; }

    }
}