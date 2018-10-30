using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentSample.Domain.AggregatesModel.PaymentAggregate;

namespace PaymentSample.CommandAPI.Host.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public HealthCheckController(IRepositoryAsync<PaymentTransaction, Guid> aggregateRootRepository)
        {

        }

        [HttpGet]
        [Route("healthcheck")]
        public IActionResult HealthCheck() => Ok(new { Status = "Healty" });
    }
}