using MediaInAction.VideoService.Samples;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
