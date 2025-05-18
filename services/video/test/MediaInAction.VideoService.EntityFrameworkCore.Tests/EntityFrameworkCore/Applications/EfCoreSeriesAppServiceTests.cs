using MediaInAction.VideoService.Services;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSeriesAppServiceTests : EpisodeAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
