
using System;
using Volo.Abp.Application.Dtos;

namespace MyAbpApp.CpqDtos
{
    public class CpqGetAllMicroserviceResponse
    {
        public required MicroserviceResult[] Results { get; set; }
    }
    public class MicroserviceResult
    {
        public required string ServiceName { get; set; }
        public required string ServiceVersion { get; set; }
        public required string ServiceDescription { get; set; }
        public required string ServiceId { get; set; }
    }
    public class CreateMicroserviceRequest
    {
        public required string ServiceName { get; set; }
        public required string ServiceVersion { get; set; }
        public required string ServiceDescription { get; set; }
    }
    public class DeleteMicroserviceRequest
    {
        public required string ServiceId { get; set; }
    }
}

