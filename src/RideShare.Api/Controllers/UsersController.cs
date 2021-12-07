using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RideShare.Application.Common;
using RideShare.Domain.Entities;
using System.Threading;
using RideShare.Application.Commands.Users.CreateUser;
using MediatR;
using RideShare.Api.Common;
using RideShare.Application.Commands.Users.UpdateUserPassword;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideShare.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : CustomControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<CreateUserResponse> Create([FromBody] CreateUserRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<UpdateUserPasswordResponse> UpdatePassword([FromBody] UpdateUserPasswordRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
