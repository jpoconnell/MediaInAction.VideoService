using Volo.Abp.Settings;

namespace MediaInAction.EmbyService.Settings
{
    public class EmbyServiceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EmbyServiceSettings.MySetting1));
        }
    }
}
