using MediaInAction.VideoService.ToBeMappedNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreToBeMappedManagerTests : ToBeMappedManagerUnitTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
