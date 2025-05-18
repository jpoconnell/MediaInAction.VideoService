using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MediaInAction.VideoService.SeriesAliasNs;

public class SeriesAliasManager : DomainService
{
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    private readonly ILogger<SeriesAliasManager> _logger;

    public SeriesAliasManager(ISeriesAliasRepository seriesAliasRepository,
        ILogger<SeriesAliasManager> logger)
    {
        _seriesAliasRepository = seriesAliasRepository;
        _logger = logger;
    }

    public async Task<SeriesAlias> CreateAsync(Guid seriesId, SeriesAliasCreateDto seriesAliasCreateDto)
    // this assumes that there is a seriesId
    {
        Check.NotNullOrWhiteSpace(seriesAliasCreateDto.IdType, nameof(seriesAliasCreateDto.IdType));
        Check.NotNullOrWhiteSpace(seriesAliasCreateDto.IdValue, nameof(seriesAliasCreateDto.IdValue));

        if (seriesAliasCreateDto.IdType == "name")
        {
            seriesAliasCreateDto.IdValue = seriesAliasCreateDto.IdValue.ToLower();
        }
        
        var newSeriesAlias = new SeriesAlias(
            GuidGenerator.Create(),
            seriesId,
            seriesAliasCreateDto.IdType,
            seriesAliasCreateDto.IdValue
        );

        var createSeriesAlias = await _seriesAliasRepository.InsertAsync(newSeriesAlias, true);
        return createSeriesAlias;
    }

    public async Task UpdateAsync(Guid seriesId, SeriesAliasCreateDto seriesAliasCreateDto)
    {
        Check.NotNullOrWhiteSpace(seriesAliasCreateDto.IdType, nameof(seriesAliasCreateDto.IdType));
        Check.NotNullOrWhiteSpace(seriesAliasCreateDto.IdType, nameof(seriesAliasCreateDto.IdType));

        var existingSeriesAlias = await _seriesAliasRepository.FindBySeriesTypeAsync(seriesId, 
            seriesAliasCreateDto.IdType);
        
    }
}

