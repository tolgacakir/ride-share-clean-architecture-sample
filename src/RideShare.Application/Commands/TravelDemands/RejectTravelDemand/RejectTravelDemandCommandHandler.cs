using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using RideShare.Application.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using RideShare.Application.Exception;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.TravelDemands.RejectTravelDemand
{
    public class RejectTravelDemandCommandHandler : IRequestHandler<RejectTravelDemandRequest, RejectTravelDemandResponse>
    {
        private readonly IRideShareDbContext _context;

        public RejectTravelDemandCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<RejectTravelDemandResponse> Handle(RejectTravelDemandRequest request, CancellationToken cancellationToken)
        {
            var driver = await _context.Drivers
                .Include(x => x.TravelPlans)
                .ThenInclude(x => x.Demands)
                .Where(x => x.User.Id == request.UserId)
                .FirstOrDefaultAsync();

            if (driver is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(Driver).Name));
            }

            var plan = driver.ActiveTravelPlans
                .Where(x => x.Id == request.TravelPlanId)
                .FirstOrDefault();

            if (plan is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(TravelPlan).Name));
            }

            var demand = plan.AwaitingDemands
                .Where(x => x.Id == request.TravelDemandId)
                .FirstOrDefault();

            if (demand is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(TravelDemand).Name));
            }

            driver.RejectDemand(plan, demand);

            var result = await _context.SaveChangesAsync(cancellationToken);
            return new RejectTravelDemandResponse
            {
                Result = result
            };
        }
    }

    public class RejectTravelDemandRequest : IRequest<RejectTravelDemandResponse>
    {
        public Guid UserId { get; set; }
        public int TravelPlanId { get; set; }
        public int TravelDemandId { get; set; }
    }

    public class RejectTravelDemandResponse
    {
        public int Result { get; set; }
    }
}