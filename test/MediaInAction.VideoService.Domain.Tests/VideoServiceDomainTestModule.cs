using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceDomainModule),
    typeof(VideoServiceTestBaseModule)
)]
public class VideoServiceDomainTestModule : AbpModule
{

}
