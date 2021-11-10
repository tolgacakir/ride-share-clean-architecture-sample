using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideShare.Api.Common;
using RideShare.Application.Commands.Drivers.CreateDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideShare.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriversController : CustomControllerBase
    {
        public DriversController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost]
        public async Task<CreateDriverResponse> Create([FromBody] CreateDriverRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
