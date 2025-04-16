using Microsoft.Extensions.Localization;
using MediaInAction.VideoService.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MediaInAction.VideoService;

[Dependency(ReplaceServices = true)]
public class VideoServiceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<VideoServiceResource> _localizer;

    public VideoServiceBrandingProvider(IStringLocalizer<VideoServiceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
