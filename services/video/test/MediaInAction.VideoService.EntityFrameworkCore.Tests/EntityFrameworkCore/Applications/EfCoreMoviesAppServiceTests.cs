using MediaInAction.VideoService.Services;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreMovieAppServiceTests : EpisodeAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
