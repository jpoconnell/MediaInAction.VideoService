using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreFileEntryManagerTests : FileEntryManagerUnitTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
