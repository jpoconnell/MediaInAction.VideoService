using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
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

/*
 *         string comment,
   bool isSeed,
   string hash,
   bool paused ,
   double ratio ,
   string message, 
   string name,
   string label, 
   long added,
   double completeTime, 
   string location,
   FileStatus status,
   MediaType type,
 */
    [Fact]
    public async Task Should_CreateTorrentAsync()
    {
        var createdTorrent = await _torrentManager.CreateAsync(
            "no comment",
            true,
            "hash",
            false,
            0.0,
            "message",
            "name",
            "label",
            2000,
            0.0,
            "location",
            FileStatus.New,
            MediaType.Torrent,
            Guid.Empty,
            Guid.Empty,
            false
        );
        
        createdTorrent.ShouldNotBeNull();
    }
}
