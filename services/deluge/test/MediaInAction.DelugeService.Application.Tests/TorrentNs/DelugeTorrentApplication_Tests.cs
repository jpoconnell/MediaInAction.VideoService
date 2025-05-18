using MediaInAction.DelugeService.DelugeTorrentNs;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp.Users;

namespace MediaInAction.DelugeService.TorrentNs;

public class DelugeTorrentApplication_Tests : DelugeServiceApplicationTestBase
{
    private readonly IDelugeTorrentAppService _embyTorrentAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public DelugeTorrentApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _embyTorrentAppService = GetRequiredService<IDelugeTorrentAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }
}