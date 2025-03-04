using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore;

[CollectionDefinition(VideoServiceTestConsts.CollectionDefinitionName)]
public class VideoServiceEntityFrameworkCoreCollection : ICollectionFixture<VideoServiceEntityFrameworkCoreFixture>
{

}
