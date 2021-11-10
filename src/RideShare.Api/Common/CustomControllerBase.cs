using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Api.Common
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected CustomControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
