using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Application.Exception;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.Drivers.CreateDriver
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverRequest, CreateDriverResponse>
    {
        private readonly IRideShareDbContext _context;

        public CreateDriverCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<CreateDriverResponse> Handle(CreateDriverRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x=> x.Driver)
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(User).Name));
            }
            
            if (user.Driver is not null)
            {
                throw new InvalidOperationException(ExceptionMessage.EntityIsAlreadyExist(typeof(Driver).Name));
            }

            var driver = user.CreateDriver();
            _context.Drivers.Add(driver);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return new CreateDriverResponse{
                Result = result
            };
        }
    }

    public class CreateDriverRequest : IRequest<CreateDriverResponse>
    {
        public Guid UserId { get; set; }
    }

    public class CreateDriverResponse
    {
        public int Result { get; set; }
    }
}