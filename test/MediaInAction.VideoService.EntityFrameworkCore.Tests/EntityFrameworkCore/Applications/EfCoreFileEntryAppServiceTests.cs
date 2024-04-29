using MediaInAction.VideoService.FileEntryNS;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreFileEntryAppServiceTests : FileEntryAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
