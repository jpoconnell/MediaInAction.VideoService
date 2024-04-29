using MediaInAction.VideoService.EpisodeNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreEpisodeAppServiceTests : EpisodeAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
