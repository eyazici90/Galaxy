using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CustomerSample.Application.Abstractions;
using CustomerSample.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSample.API.Host.Controllers
{
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppServ;
        public BrandController(ICustomerAppService customerAppServ)
        {
            _customerAppServ = customerAppServ ?? throw new ArgumentNullException(nameof(customerAppServ));
        }

        [Route("api/Customer/Brands")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public   Task<IActionResult> GetAllBrandsAsync() =>
             ResponseOrThrow(async () => await this._customerAppServ.GetAllBrandsAsync());

        [Route("api/Customer/Brand/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public  Task<IActionResult> GetBrandByIdAsync(int id) =>
               ResponseOrThrow(async () => await this._customerAppServ.GetBrandByIdAsync(id));


        [Route("api/Customer/Brand/ChangeName")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public  Task<IActionResult> ChangeBrandName([FromBody] BrandDto request) =>
               ResponseOrThrow(request, async (r) => await this._customerAppServ.ChangeBrandName(r));

        [Route("api/Customer/ChangeMerchantVknByBrand")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public  Task<IActionResult> ChangeMerchantVknByBrand([FromBody] MerchantDto request) =>
               ResponseOrThrow(request, async (r) => await this._customerAppServ.ChangeMerchantVknByBrand(r));


        [Route("api/Customer/Brand")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public  Task<IActionResult> AddNewBrand([FromBody] BrandDto request) =>
               ResponseOrThrow(request, async (r) => await this._customerAppServ.AddNewBrand(r));


        [Route("api/Customer/Merchant")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public  Task<IActionResult> AddMerchantToBrand([FromBody] MerchantDto request) =>
              ResponseOrThrow(request, async (r) => await this._customerAppServ.AddMerchantToBrand(r));


        private async Task<IActionResult> ResponseOrThrow<T>(T request, Func<T, Task> handler)
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

        private async Task<IActionResult> ResponseOrThrow<T>(Func<Task<T>> handler)
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