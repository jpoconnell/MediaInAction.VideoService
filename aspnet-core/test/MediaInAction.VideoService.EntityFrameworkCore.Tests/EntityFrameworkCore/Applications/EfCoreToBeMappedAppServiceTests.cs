using MediaInAction.VideoService.ToBeMappedNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreToBeMappedAppServiceTests : ToBeMappedAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
