using IotDb.MeasurementManagement.Samples;
using Xunit;

namespace IotDb.MeasurementManagement.EntityFrameworkCore.Domains;

[Collection(MeasurementManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MeasurementManagementEntityFrameworkCoreTestModule>
{

}
