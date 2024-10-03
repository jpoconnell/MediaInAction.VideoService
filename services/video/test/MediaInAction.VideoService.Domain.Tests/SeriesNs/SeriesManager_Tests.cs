using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly SeriesManager _seriesManager;
    private readonly ISeriesRepository _seriesRepository;

    public SeriesManagerUnitTests()
    {
        _seriesManager = GetRequiredService<SeriesManager>();
        _seriesRepository = GetRequiredService<ISeriesRepository>();
    }

    [Fact]
    public async Task Should_CreateSeriesAsync()
    {
        var newSeriesAlias1 = new SeriesAliasCreateDto();
        var newSeriesAliasList = new List<SeriesAliasCreateDto>();
        newSeriesAlias1.IdType = "Test product";
        newSeriesAlias1.IdValue = "Code:001";
        newSeriesAliasList.Add(newSeriesAlias1);
        var newSeriesAlias2 = new SeriesAliasCreateDto();
        newSeriesAlias2.IdType = "name";
        newSeriesAlias2.IdValue = "Code:002";
        newSeriesAliasList.Add(newSeriesAlias2);
        var newSeries = new SeriesCreateDto();
        newSeries.Name = "Series Name 1";
        newSeries.FirstAiredYear = 2020;
        newSeries.IsActive = true;
        newSeries.SeriesAliasCreateDtos = newSeriesAliasList;

        var createdSeries = await _seriesManager.CreateAsync(newSeries);
        
        createdSeries.ShouldNotBeNull();
    }
    
    
    
    //SetAsInActiveAsync
    
    //AddSeriesAliasAsync
}