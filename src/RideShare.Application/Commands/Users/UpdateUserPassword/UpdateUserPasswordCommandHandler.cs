using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Application.Exception;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.Users.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordRequest, UpdateUserPasswordResponse>
    {
        private readonly IRideShareDbContext _context;

        public UpdateUserPasswordCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateUserPasswordResponse> Handle(UpdateUserPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync().Result;

            if (user is null)
            {
                throw new NullReferenceException(ExceptionMessage.EntityNotFound(typeof(User).Name));
            }

            user.SetPassword(request.Password);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return new UpdateUserPasswordResponse
            {
                Result = result
            };
        }
    }

    public class UpdateUserPasswordRequest : IRequest<UpdateUserPasswordResponse>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserPasswordResponse
    {
        public int Result { get; set; }
    }
}