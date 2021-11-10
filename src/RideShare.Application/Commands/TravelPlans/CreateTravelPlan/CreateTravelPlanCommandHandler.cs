using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace RideShare.Application.Commands.TravelPlans.CreateTravelPlan
{
    public class CreateTravelPlanCommandHandler : IRequestHandler<CreateTravelPlanRequest, CreateTravelPlanResponse>
    {
        private readonly IRideShareDbContext _context;

        public CreateTravelPlanCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<CreateTravelPlanResponse> Handle(CreateTravelPlanRequest request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers.Where(x=>x.User.Id == request.UserId)
                .FirstOrDefaultAsync();

            var plan = driver.CreateTravelPlan(
                request.Caption,
                request.From,
                request.To,
                request.StartAt,
                request.Capacity,
                request.AwaitingDemandCapacity
                );

            _context.TravelPlans.Add(plan);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return new CreateTravelPlanResponse{
                Result = result
            };
        }
    }

    public class CreateTravelPlanRequest : IRequest<CreateTravelPlanResponse>
    {
        public Guid UserId { get; set; }
        public string Caption { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public byte Capacity { get; set; }
        public DateTime StartAt { get; set; }
        public ushort? AwaitingDemandCapacity { get; set; }
    }

    public class CreateTravelPlanResponse
    {
        public int Result { get; set; }
    }
}