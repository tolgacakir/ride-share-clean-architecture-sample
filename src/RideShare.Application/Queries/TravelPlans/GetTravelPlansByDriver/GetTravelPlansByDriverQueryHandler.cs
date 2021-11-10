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

namespace RideShare.Application.Queries.TravelPlans.GetTravelPlansByDriver
{
    public class GetTravelPlansByDriverQueryHandler : IRequestHandler<GetTravelPlansByDriverRequest, List<GetTravelPlansByDriverResponse>>
    {
        private readonly IRideShareDbContext _context;
        private readonly IMapper _mapper;

        public GetTravelPlansByDriverQueryHandler(IRideShareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTravelPlansByDriverResponse>> Handle(GetTravelPlansByDriverRequest request, CancellationToken cancellationToken)
        {
            var travelPlans = await _context.TravelPlans
                .Include(x => x.Driver)
                .ThenInclude(x => x.User)
                .Include(x=>x.Demands)
                .ThenInclude(x=>x.Passenger)
                .Where(x => x.Driver.User.Id == request.UserId)
                .OrderBy(x => x.Status)
                .ThenBy(x => x.StartAt)
                .ToListAsync();

            return _mapper.Map<List<GetTravelPlansByDriverResponse>>(travelPlans);
        }
    }

    public class GetTravelPlansByDriverRequest : IRequest<List<GetTravelPlansByDriverResponse>>
    {
        public Guid UserId { get; set; }
    }

    public class GetTravelPlansByDriverResponse
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