using MediaInAction.VideoService.FileEntryNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreFileEntryDomainTests : FileEntryDomainTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
