using Microsoft.AspNetCore.Builder;
using IotDb.MeasurementManagement;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("IotDb.MeasurementManagement.Web.csproj"); 
await builder.RunAbpModuleAsync<MeasurementManagementWebTestModule>(applicationName: "IotDb.MeasurementManagement.Web");

public partial class Program
{
}
