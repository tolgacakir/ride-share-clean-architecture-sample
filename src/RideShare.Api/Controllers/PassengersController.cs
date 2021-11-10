using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideShare.Api.Common;
using RideShare.Application.Commands.Passengers.CreatePassenger;
using RideShare.Application.Queries.Passengers.GetPassengersByTravelPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideShare.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PassengersController : CustomControllerBase
    {
        public PassengersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<List<GetPassengersByTravelPlanResponse>> GetListByTravelPlan([FromQuery] GetPassengersByTravelPlanRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<CreatePassengerResponse> Create([FromBody] CreatePassengerRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
