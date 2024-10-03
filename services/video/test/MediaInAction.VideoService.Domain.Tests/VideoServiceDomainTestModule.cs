using MediaInAction.VideoService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService
{
    [DependsOn(
        typeof(VideoServiceEntityFrameworkCoreTestModule)
        )]
    public class VideoServiceDomainTestModule : AbpModule
    {

    }
}