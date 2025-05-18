using MediaInAction.EmbyService.Localization;
using MediaInAction.Shared.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace MediaInAction.EmbyService;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(ServiceDomainSharedModule)
)]
public class EmbyServiceDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EmbyServiceDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<EmbyServiceResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/EmbyService");

            options.DefaultResourceType = typeof(EmbyServiceResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("EmbyService", typeof(EmbyServiceResource));
        });
    }
}