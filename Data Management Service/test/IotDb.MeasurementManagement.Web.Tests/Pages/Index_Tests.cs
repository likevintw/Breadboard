using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace IotDb.MeasurementManagement.Pages;

[Collection(MeasurementManagementTestConsts.CollectionDefinitionName)]
public class Index_Tests : MeasurementManagementWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
