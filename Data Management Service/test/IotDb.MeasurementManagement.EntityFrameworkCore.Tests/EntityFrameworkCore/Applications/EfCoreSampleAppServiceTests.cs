using IotDb.MeasurementManagement.Samples;
using Xunit;

namespace IotDb.MeasurementManagement.EntityFrameworkCore.Applications;

[Collection(MeasurementManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MeasurementManagementEntityFrameworkCoreTestModule>
{

}
