using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideShare.Api.Common;
using RideShare.Application.Commands.TravelPlans.CancelTravelPlan;
using RideShare.Application.Commands.TravelPlans.CreateTravelPlan;
using RideShare.Application.Commands.TravelPlans.FinishTravelPlan;
using RideShare.Application.Queries.TravelPlans.GetActiveTravelPlans;
using RideShare.Application.Queries.TravelPlans.GetTravelPlansByPassenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideShare.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelPlansController : CustomControllerBase
    {
        public TravelPlansController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<List<GetActiveTravelPlansResponse>> GetActiveList([FromQuery] GetActiveTravelPlansRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<List<GetTravelPlansByDriverResponse>> GetListDriver([FromQuery] GetTravelPlansByDriverRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<List<GetTravelPlansByPassengerResponse>> GetListByPassenger([FromQuery] GetTravelPlansByPassengerRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<CreateTravelPlanResponse> Create([FromBody] CreateTravelPlanRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<FinishTravelPlanResponse> Finish([FromBody] FinishTravelPlanRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<CancelTravelPlanResponse> Cancel([FromBody] CancelTravelPlanRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
