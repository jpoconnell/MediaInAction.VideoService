using MediaInAction.VideoService.TorrentNs;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Domains;

[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class EfCoreTorrentDomainTests : TorrentDomainTests<VideoServiceEntityFrameworkCoreTestModule>
{

}
