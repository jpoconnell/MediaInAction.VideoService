using MediaInAction.VideoService.ToBeMapped;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreToBeMappedDomainTests : ToBeMappedDomainTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
