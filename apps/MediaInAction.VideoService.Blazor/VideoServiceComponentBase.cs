using MediaInAction.VideoService.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MediaInAction.VideoService.Blazor;

public abstract class VideoServiceComponentBase : AbpComponentBase
{
    protected VideoServiceComponentBase()
    {
        LocalizationResource = typeof(VideoServiceResource);
    }
}
