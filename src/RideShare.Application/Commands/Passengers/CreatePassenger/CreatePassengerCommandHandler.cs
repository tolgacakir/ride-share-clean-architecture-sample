using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Application.Exception;
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

            if (user is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(User).Name));
            }
            
            if (user.Passenger is not null)
            {
                throw new InvalidOperationException(ExceptionMessage.EntityIsAlreadyExist(typeof(Passenger).Name));
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