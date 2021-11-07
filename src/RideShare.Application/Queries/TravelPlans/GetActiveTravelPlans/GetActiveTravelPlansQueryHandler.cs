using RideShare.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RideShare.Application.Queries.TravelPlans.GetActiveTravelPlans
{
    public class GetActiveTravelPlansQueryHandler : IRequestHandler<GetActiveTravelPlansRequest, List<GetActiveTravelPlansResponse>>
    {
        private readonly IRideShareDbContext _context;
        private readonly IMapper _mapper;

        public GetActiveTravelPlansQueryHandler(IRideShareDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetActiveTravelPlansResponse>> Handle(GetActiveTravelPlansRequest request, CancellationToken cancellationToken)
        {
            var travelPlans = await _context.TravelPlans.Where(x=>x.Status == TravelPlanStatuses.Active).ToListAsync();
            return _mapper.Map<List<GetActiveTravelPlansResponse>>(travelPlans);
        }
    }

    public class GetActiveTravelPlansRequest : IRequest<List<GetActiveTravelPlansResponse>>
    {
        
    }

    public class GetActiveTravelPlansResponse
    {
        public string Caption { get; set; }
        public byte EmptySeat { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DriverName { get; set; }
        
    }
}