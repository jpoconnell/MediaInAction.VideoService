using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.TorrentNs.Dtos;
using MediaInAction.VideoService.TorrentsNs;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.TorrentNs;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IdentityUserManager here).
 * Only test your own domain services.
 */
public abstract class TorrentManagerUnitTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ITorrentRepository _torrentRepository;
    private readonly TorrentManager _torrentManager;

    public TorrentManagerUnitTests()
    {
        _torrentRepository = GetRequiredService<ITorrentRepository>();
        _torrentManager = GetRequiredService<TorrentManager>();
    }

    [Fact]
    public async Task Should_CreateTorrentAsync()
    {
        var input = new TorrentCreateDto
        {
            Comment = "no comment",
            IsSeed = true,
            Hash = "hash",
            Paused = false,
            Ratio = 0.0,
            Message = "message",
            Name = "name",
            Label = "label",
            Added = 2000,
            CompleteTime = 0.0,
            DownloadLocation = "location",
            Status = FileStatus.New.ToString()
        };
        
        await _torrentManager.CreateAsync(input);
        
        var fromDb = await _torrentRepository.FindByHash(input.Hash);
        
        fromDb.ShouldNotBeNull();
    }
}
