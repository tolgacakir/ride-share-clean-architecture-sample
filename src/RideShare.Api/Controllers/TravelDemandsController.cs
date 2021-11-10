using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideShare.Api.Common;
using RideShare.Application.Commands.TravelDemands.AcceptTravelDemand;
using RideShare.Application.Commands.TravelDemands.CancelTravelDemand;
using RideShare.Application.Commands.TravelDemands.CreateTravelDemand;
using RideShare.Application.Commands.TravelDemands.RejectTravelDemand;
using RideShare.Application.Queries.TravelDemands.GetAwaitingTravelDemandsByTravelPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideShare.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelDemandsController : CustomControllerBase
    {
        public TravelDemandsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<List<GetAwaitingTravelDemandsByTravelPlanResponse>> GetAwaitingListByTravelPlan([FromQuery] GetAwaitingTravelDemandsByTravelPlanRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<CreateTravelDemandResponse> Create([FromBody] CreateTravelDemandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<AcceptTravelDemandResponse> Accept([FromBody] AcceptTravelDemandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<RejectTravelDemandResponse> Reject([FromBody] RejectTravelDemandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<CancelTravelDemandResponse> Cancel([FromBody] CancelTravelDemandRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
