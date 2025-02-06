﻿using Xunit;

namespace IotDb.MeasurementManagement.EntityFrameworkCore;

[CollectionDefinition(MeasurementManagementTestConsts.CollectionDefinitionName)]
public class MeasurementManagementEntityFrameworkCoreCollection : ICollectionFixture<MeasurementManagementEntityFrameworkCoreFixture>
{

}
