using Volo.Abp.Settings;

namespace MediaInAction.FileService.Settings
{
    public class FileServiceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(FileServiceSettings.MySetting1));
        }
    }
}
