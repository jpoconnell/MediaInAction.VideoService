using MediaInAction.VideoService.SeriesNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCorePublicSeriesAppServiceTests : PublicSeriesAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
