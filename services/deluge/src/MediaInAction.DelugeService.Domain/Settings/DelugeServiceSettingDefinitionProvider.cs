using Volo.Abp.Settings;

namespace MediaInAction.DelugeService.Settings
{
    public class DelugeServiceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(DelugeServiceSettings.MySetting1));
        }
    }
}
