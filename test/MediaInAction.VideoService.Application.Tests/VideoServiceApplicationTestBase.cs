using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

public abstract class VideoServiceApplicationTestBase<TStartupModule> : VideoServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
