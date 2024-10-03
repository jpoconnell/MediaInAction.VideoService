using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesDomainTests<TStartupModule> : VideoServiceDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly SeriesManager _seriesManager;
    private readonly ISeriesRepository _seriesRepository;

    public SeriesDomainTests()
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
        var newSeriesAlias2 = new SeriesAliasCreateDto
        {
            IdType = "name",
            IdValue = "Code:002"
        };
        newSeriesAliasList.Add(newSeriesAlias2);
        var newSeries = new SeriesCreateDto
        {
            Name = "Series Name 1",
            FirstAiredYear = 2020,
            IsActive = true,
            SeriesAliasCreateDtos = newSeriesAliasList
        };
        await WithUnitOfWorkAsync(async () =>
        {
            var createdSeries = await _seriesManager.CreateAsync(newSeries);

            createdSeries.ShouldNotBeNull();
        });
    }
   
}