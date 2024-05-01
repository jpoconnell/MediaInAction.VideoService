using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

[Dependency(ReplaceServices = true)]
public class VideoServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "VideoService";
}
