using MediaInAction.EmbyService.Localization;
using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
        )]
    public class EmbyServiceHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EmbyServiceResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
