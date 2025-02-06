

using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyAbpApp.ICpqServices
{
    public class NatsMicroservice
    {
        public required string ServiceName { get; set; }
        public required string ServiceVersion { get; set; }
        public required string ServiceDescription { get; set; }
        public required string ServiceId { get; set; }
    }
    public class NatsMicroservices
    {
        public required NatsMicroservice[] Results { get; set; }
    }
    public interface ICpqService
    {
        Task<NatsMicroservices> GetAllNatsMicroservice();
        Task<(string result, string serviceId)> CreateServiceHoneywellCe3245(string ServiceName, string FunctionName, string ServiceVersion, string ServiceDescription);
        // Task<(string result, string serviceId)> CreateServiceTasto_ks456(string ServiceName, string serviceVersion, string ServiceDescription);
        // Task<(string result, string serviceId)> CreateServicePercentage(string ServiceName, string serviceVersion, string ServiceDescription);
        Task<string> DeleteNatsService(string serviceId);
    }
}
