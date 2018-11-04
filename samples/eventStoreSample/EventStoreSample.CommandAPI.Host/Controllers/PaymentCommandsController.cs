using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventStoreSample.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStoreSample.CommandAPI.Host.Controllers
{
    [ApiController]
    public class PaymentCommandsController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public PaymentCommandsController(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        [Route("api/Payment/Transaction/Direct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        public Task<IActionResult> DirectPayment([FromBody] DirectPaymentCommand request) =>
            HandleOrThrow(request, async (r) => await this._mediatr.Send(r));

        [Route("api/Payment/Transaction/Amount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPut]
        public Task<IActionResult> ChangeAmount([FromBody] ChangeOrSetAmountCommand request) =>
          HandleOrThrow(request, async (r) => await this._mediatr.Send(r));



        [Route("api/Payment/Transaction/Refund")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        public Task<IActionResult> RefundPayment([FromBody] RefundPaymentCommand request) =>
            HandleOrThrow(request, async (r) => await this._mediatr.Send(r));



        private async Task<IActionResult> HandleOrThrow<T>(T request, Func<T, Task> handler)
        {
            try
            {
                await handler(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.ToString() });
            }
        }

        private async Task<IActionResult> HandleOrThrow<T>(Func<Task<T>> handler)
        {
            try
            {
                var result = await handler();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.ToString() });
            }
        }
    }
}