using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.Bg.DelugeTorrentNs;
using MediaInAction.DelugeService.Bg.JobArgs;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.DelugeService.Bg.Jobs;

public class SendAllJob
        : AsyncBackgroundJob<SendAllArgs>, ITransientDependency
{
    private IDelugeTorrentService _torrentService;
    
    public SendAllJob( IDelugeTorrentService torrentService)
    {
        _torrentService = torrentService;
    }
    
    public override Task ExecuteAsync(SendAllArgs args)
    {
        throw new NotImplementedException();
    }
}
