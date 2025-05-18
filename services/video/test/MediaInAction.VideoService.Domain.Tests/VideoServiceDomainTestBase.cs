using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

/* Inherit from this class for your domain layer tests. */
public abstract class VideoServiceDomainTestBase<TStartupModule> : VideoServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
