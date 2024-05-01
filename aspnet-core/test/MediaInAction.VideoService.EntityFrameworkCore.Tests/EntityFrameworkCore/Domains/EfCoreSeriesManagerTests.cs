using MediaInAction.VideoService.SeriesNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSeriesManagerTests : SeriesManagerUnitTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
