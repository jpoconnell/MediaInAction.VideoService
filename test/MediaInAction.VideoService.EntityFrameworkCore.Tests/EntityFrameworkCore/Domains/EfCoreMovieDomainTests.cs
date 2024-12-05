using MediaInAction.VideoService.MovieNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreMovieDomainTests : MovieDomainTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
