using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MediaInAction.VideoService;

[Dependency(ReplaceServices = true)]
public class VideoServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "VideoService";
}
