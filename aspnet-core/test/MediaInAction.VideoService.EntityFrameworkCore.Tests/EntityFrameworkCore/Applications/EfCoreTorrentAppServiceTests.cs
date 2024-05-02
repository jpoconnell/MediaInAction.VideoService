using MediaInAction.VideoService.TorrentNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Applications;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreTorrentAppServiceTests : TorrentAppServiceTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
