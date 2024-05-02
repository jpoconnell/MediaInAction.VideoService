using MediaInAction.VideoService.MovieNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreMovieAppServiceTests : MovieAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
