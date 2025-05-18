using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesManager(
    ISeriesRepository seriesRepository,
    ILogger<SeriesManager> logger)
    : DomainService
{
    
    public async Task<Series> CreateUpdateAsync(SeriesCreateDto seriesCreateDto)
    {
        try
        {
            if (seriesCreateDto.SeriesStatus == SeriesStatus.Unknown)
            {
                seriesCreateDto.SeriesStatus = SeriesStatus.New;
            }
            if (seriesCreateDto.FirstAiredYear < 1950)
            {
                seriesCreateDto.FirstAiredYear = 1950;
            }

            var seriesId = GuidGenerator.Create();
            // Create new series
            var newSeries = new Series(
                name: seriesCreateDto.Name,
                firstAiredYear: seriesCreateDto.FirstAiredYear,
                seriesStatus: seriesCreateDto.SeriesStatus,
                imageName: seriesCreateDto.ImageName
            );
            
            if((seriesCreateDto.SeriesAliasCreateDtos == null) || (seriesCreateDto.SeriesAliasCreateDtos?.Count == 0))
            {
                newSeries.AddSeriesAlias(
                    seriesId: seriesId,
                    idType: "name",
                    idValue: seriesCreateDto.Name);
            }
            else
            {            
                foreach (var seriesAlias in seriesCreateDto.SeriesAliasCreateDtos)
                {
                    newSeries.AddSeriesAlias(
                        seriesId: seriesId,
                        idType: seriesAlias.IdType,
                        idValue: seriesAlias.IdValue);
                }

                var nameFound = false;
                var folderFound = false;

                foreach (var seriesAlias in seriesCreateDto.SeriesAliasCreateDtos)
                {
                    if (seriesAlias.IdType == "name")
                    {
                        nameFound = true;
                    }

                    if (seriesAlias.IdType == "folder")
                    {
                        folderFound = true;
                    }
                }

                if (nameFound == false)
                {
                    newSeries.AddSeriesAlias(
                        seriesId: seriesId,
                        idType: "name",
                        idValue: seriesCreateDto.Name
                    );
                }

                if (folderFound == false)
                {
                    newSeries.AddSeriesAlias(
                        seriesId: seriesId,
                        idType: "folder",
                        idValue: seriesCreateDto.Name
                    );
                }
            }
            var createSeries = await seriesRepository.InsertAsync(newSeries, true);
            return createSeries;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CreateAsync");
            return null;
        }
    }
    

    public async Task<Series> SetAsInActiveAsync(Guid seriesId)
    {
        var series = await seriesRepository.GetAsync(seriesId);
        if (series == null)
        {
            throw new BusinessException(VideoServiceErrorCodes.SeriesWithIdNotFound)
                .WithData("SeriesId", seriesId);
        }
        else
        {
           // series.SetSeriesInactive();
            return await seriesRepository.UpdateAsync(series, autoSave: true);
        }
    }
}
