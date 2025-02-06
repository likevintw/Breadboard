using Volo.Abp.Settings;

namespace IotDb.MeasurementManagement.Settings;

public class MeasurementManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MeasurementManagementSettings.MySetting1));
    }
}
