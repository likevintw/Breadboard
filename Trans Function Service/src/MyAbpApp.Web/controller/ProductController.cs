
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using MyAbpApp.Products;
using MyAbpApp.ISmsServices;
using MyAbpApp.IGyroscopeServices;
using MyAbpApp.GyroscopeInsertRequestDtos;
using MyAbpApp.CpqDtos;
using MyAbpApp.ICpqServices;

namespace MyAbpApp.Web.Controllers
{

    [Route("/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductDto>>> GetProduct(Guid id)
        {
            Console.WriteLine($"{id}");

            var product = await _productAppService.GetAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductDto>>> CreateProduct(CreateProductDto input)
        {
            Console.WriteLine($"{input.Name}");
            var result = await _productAppService.CreateAsync(input);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<ProductDto>>> UpdateProdcut(UpdateProductDto input)
        {
            var result = await _productAppService.UpdateAsync(input);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductDto>>> DeleteProdcut(Guid id)
        {
            var result = await _productAppService.DeleteAsync(id);
            return Ok(result);
        }


    }
    public class ServiceRequest
    {
        public required string Message { get; set; }
    }
    [Route("/sms")]
    [ApiController]
    public class SmsController
    {

        private readonly ISmsService _smsAppService;

        public SmsController(ISmsService smsAppService)
        {
            _smsAppService = smsAppService;
        }

        [HttpGet]
        public async Task<IActionResult> SendSmsDemo([FromQuery] ServiceRequest request)
        {
            Console.WriteLine($"got a request for sensor");

            await _smsAppService.SendAsync("phone number", "message");
            var response = new
            {
                message = "Hello, " + request.Message
            };

            return new JsonResult(response);
        }
    }
    public class InsertOneGyroscopeRequest
    {
        public required string Database { get; set; }
    }
    public class GyroscopeInsertRequest
    {
        public required string Database { get; set; }
        public required long Timestamp { get; set; }
        public required string Measurement { get; set; }
        public double Value { get; set; }
    }

    [Route("/gyroscope")]
    [ApiController]
    public class GyroscopeController : ControllerBase
    {

        private readonly IGyroscopeService _gyroscopeService;

        public GyroscopeController(IGyroscopeService gyroscopeAppService)
        {
            _gyroscopeService = gyroscopeAppService;
        }


        // [HttpPost]
        // public async Task<IActionResult> Insert([FromBody] GyroscopeInsertRequest request)
        // {
        //     string database = request.Database ?? "root.default";
        //     long timestamp = 0;
        //     string measure = "x_velocity";
        //     double value = 53.4;
        //     await _gyroscopeService.InsertDataAsync(database, timestamp, measure, value);

        //     return new JsonResult(new { Status = "ok" });
        // }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] GyroscopeInsertRequestDto request)
        {
            string database = request.Database ?? "root.default";
            long timestamp = request.Timestamp;
            string measure = request.Measurement;
            double value = request.Value;
            await _gyroscopeService.InsertDataAsync(database, timestamp, value, measure);

            return new JsonResult(new { Status = "ok" });
        }

    }


    [Route("/cpq")]
    [ApiController]
    public class CpqController : ControllerBase
    {

        private readonly ICpqService _iCpqService;

        public CpqController(ICpqService iCpqService)
        {
            _iCpqService = iCpqService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNatsMicroservice()
        {
            var microservices = await _iCpqService.GetAllNatsMicroservice();
            return Ok(microservices);
        }
        [HttpPost("honeywell_ce3245")]
        public async Task<IActionResult> CreateServiceHoneywellCe3245([FromBody] CreateMicroserviceRequest request)
        {
            string ServiceName = request.ServiceName;
            string FunctionName = request.FunctionName;
            string ServiceVersion = request.ServiceVersion;
            string ServiceDescription = request.ServiceDescription;
            var result = await _iCpqService.CreateServiceHoneywellCe3245(
                ServiceName,
                FunctionName,
                ServiceVersion,
                ServiceDescription
            );
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNatsMicroservice(string id)
        {
            Console.WriteLine($"QQQQQQQQQQQQQQ{id}");
            string ServiceId = "1";
            var result = await _iCpqService.DeleteNatsService(ServiceId);
            return Ok(result);
        }

    }

}
