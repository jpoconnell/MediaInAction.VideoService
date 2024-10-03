using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Users;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesApplication_Tests : VideoServiceApplicationTestBase
{
    private readonly ISeriesAppService _seriesAppService;
    private readonly TestData _testData;
    private ICurrentUser _currentUser;

    public SeriesApplication_Tests()
    {
        _testData = GetRequiredService<TestData>();
        _currentUser = GetRequiredService<ICurrentUser>();
        _seriesAppService = GetRequiredService<ISeriesAppService>();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task Create_ShouldCreateSeries()
    {
        // Arrange Act
        var seriesDto = await CreateSeries();

        // Assert
        seriesDto.ShouldNotBeNull();
        seriesDto.SeriesAliasDtos.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task ShouldGetSeriesListAsync()
    {
        // Arrange
        var input = new GetSeriesListInput();
        input.Filter = "a:";   //all active
        input.SkipCount = 0;
        input.MaxResultCount = 100;
        input.Sorting = "name";
        
        // Act
        var seriesPagedList = await _seriesAppService.GetSeriesListAsync(input);

        // Assert
        seriesPagedList.ShouldNotBeNull();
        seriesPagedList.Count.ShouldBeGreaterThan(1);
    }

    
    [Fact]
    public async Task ShouldGetByIdValueAsync()
    {
        // Arrange
        var input = "fbi";
        
        // Act
        var seriesDto = await _seriesAppService.GetByIdValue(input);

        // Assert
        seriesDto.ShouldNotBeNull();
    
    }
    
    [Fact]
    public async Task UpdateSeries_ShouldError_WhenSeriesNotExists()
    {
        // Arrange
        var newSeriesDto = await CreateSeries();
        
        // Act
        var updatedSeriesDto = await _seriesAppService.UpdateAsync(new SeriesDto
        {
            Id = newSeriesDto.Id,
            Name = "paypal3",
            FirstAiredYear = 2004,
            SeriesAliasDtos =
            [
                new SeriesAliasDto()
                {
                    IdType = "name",
                    IdValue = "paypal"
                }
            ]
        });

        // Assert
        var filter = new GetSeriesInput
        {
            Filter = "n:paypal3"
        };

       var seriesDto = await _seriesAppService.GetSeriesAsync(filter);
       seriesDto.ShouldNotBeNull();
    }

    private async Task<SeriesDto> CreateSeries()
    {
        var newSeries = new SeriesCreateDto
        {
            Name = "FBI: International",
            FirstAiredYear = 2022,
            SeriesAliasCreateDtos = 
            [
                new SeriesAliasCreateDto()
                {
                    IdType = "folder",
                    IdValue = "FBI International"
                }
            ]
        };
        
        var mySeries = await _seriesAppService.CreateAsync(newSeries);
        return mySeries;
    }
}