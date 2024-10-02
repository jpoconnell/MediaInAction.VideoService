using MediaInAction.VideoService.Samples;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
