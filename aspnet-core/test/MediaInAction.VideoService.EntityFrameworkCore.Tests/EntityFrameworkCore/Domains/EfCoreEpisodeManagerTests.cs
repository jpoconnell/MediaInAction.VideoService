using MediaInAction.VideoService.EpisodeNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreEpisodeManagerTests : EpisodeManagerUnitTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
