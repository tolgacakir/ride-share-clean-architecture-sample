using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideShare.Application.Common;
using RideShare.Domain.Entities;

namespace RideShare.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IRideShareDbContext _context;

        public CreateUserCommandHandler(IRideShareDbContext context)
        {
            _context = context;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var isExist = await _context.Users.AnyAsync(x => x.Username == request.Username);
            if (isExist)
            {
                throw new ArgumentException();
            }

            var user = new User(request.Username, request.Password);
            _context.Users.Add(user);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserResponse{
                Result = result
            };
        }
    }

    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserResponse
    {
        public int Result { get; set; }
    }
}