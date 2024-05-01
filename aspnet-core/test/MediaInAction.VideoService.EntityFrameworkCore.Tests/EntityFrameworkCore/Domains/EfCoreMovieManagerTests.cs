using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MovieNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreMovieManagerTests : MovieManagerUnitTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
