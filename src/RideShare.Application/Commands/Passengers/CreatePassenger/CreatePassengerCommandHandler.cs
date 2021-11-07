using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.Passengers.CreatePassenger
{
    public class CreatePassengerCommandHandler : IRequestHandler<CreatePassengerRequest, CreatePassengerResponse>
    {
        private readonly IRideShareDbContext _context;

        public CreatePassengerCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<CreatePassengerResponse> Handle(CreatePassengerRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x=> x.Passenger)
                .FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user.Passenger != null)
            {
                throw new InvalidOperationException();
            }

            var passenger = user.CreatePassenger();
            _context.Passengers.Add(passenger);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return new CreatePassengerResponse{
                Result = result
            };
        }
    }

    public class CreatePassengerRequest : IRequest<CreatePassengerResponse>
    {
        public Guid UserId { get; set; }
    }

    public class CreatePassengerResponse
    {
        public int Result { get; set; }
    }
}