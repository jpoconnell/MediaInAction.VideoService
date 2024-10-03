using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.VideoService.ToBeMappedsNs;

public class ToBeMappedApplication_Tests : VideoServiceApplicationTestBase
{
    private readonly IToBeMappedAppService _toBeMappedAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public ToBeMappedApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _toBeMappedAppService = GetRequiredService<IToBeMappedAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task Should_Create_ToBeMapped()
    {
        _currentUser.Id.Returns(_testData.CurrentUserId);
        _currentUser.Email.Returns(_testData.CurrentUserEmail);
        _currentUser.Name.Returns(_testData.TestUserName);
        // Create ToBeMapped
        
        var newToBeMapped = await _toBeMappedAppService.CreateAsync(new ToBeMappedCreateDto()
        {
            Alias = "paypal",
            Processed = false
        });

        // Get ToBeMapped by Alias
        var input = new GetToBeMappedInput();
        input.Alias = newToBeMapped.Alias;
        var myToBeMapped = await _toBeMappedAppService.GetToBeMappedAsync(input);
        myToBeMapped.ShouldNotBeNull();
        
    }
}