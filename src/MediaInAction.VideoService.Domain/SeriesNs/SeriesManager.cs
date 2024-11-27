using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesManager(
    ISeriesRepository seriesRepository,
    ISeriesAliasRepository seriesAliasRepository,
    ILogger<SeriesManager> logger)
    : DomainService
{
    
    public async Task<Series> CreateAsync(SeriesCreateDto seriesCreateDto)
    {
        try
        {
            if (seriesCreateDto.FirstAiredYear < 1950)
            {
                seriesCreateDto.FirstAiredYear = 1950;
            }

            var isActive = true;

            var seriesId = GuidGenerator.Create();
            // Create new series
            var newSeries = new Series(
                id: seriesId,
                name: seriesCreateDto.Name,
                firstAiredYear: seriesCreateDto.FirstAiredYear,
                seriesType: MediaType.Episode,
                isActive: isActive,
                imageName: seriesCreateDto.ImageName
            );
            
            foreach (var seriesAlias in seriesCreateDto.SeriesAliasCreateDtos)
            {
                newSeries.AddSeriesAlias(
                    id: GuidGenerator.Create(),
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
                    id: GuidGenerator.Create(),
                    seriesId: seriesId,
                    idType: "name",
                    idValue: seriesCreateDto.Name
                );
            }
            
            if (folderFound == false)
            {
                newSeries.AddSeriesAlias(
                    id: GuidGenerator.Create(),
                    seriesId: seriesId,
                    idType: "folder",
                    idValue: seriesCreateDto.Name
                );
            }
            var filter = "n:" + newSeries.Name;

            ISpecification<Series> specification = Specifications.SpecificationFactory.Create(filter);
            var dbSeriesList = await seriesRepository.GetSeriesBySpec(specification, true);
            
            if (dbSeriesList.Count == 1)
            {
                return dbSeriesList[0];
            }
            else
            {
                var createSeries = await seriesRepository.InsertAsync(newSeries, true);
                //await PublishCreateSeriesEvent(createSeries);
                return createSeries;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CreateAsync");
            return null;
        }
    }
    
    public async Task<Series> CreateUpdateSeriesAsync(SeriesCreateDto seriesCreateDto)
    {
        if (seriesCreateDto.Name != null)
        {
            if(seriesCreateDto.FirstAiredYear < 1950)
            {
                seriesCreateDto.FirstAiredYear = 1950;
            }
            var dbSeries = await seriesRepository.FindBySeriesNameYear(seriesCreateDto.Name, seriesCreateDto.FirstAiredYear);
            if (dbSeries == null)
            {
                var createSeries = await CreateAsync(seriesCreateDto);
                return createSeries;
            }
            else
            {
                var update = 0;
                if (dbSeries.IsActive != seriesCreateDto.IsActive)
                {
                    dbSeries.IsActive = seriesCreateDto.IsActive;
                    update++;
                }

                if (dbSeries.ImageName != seriesCreateDto.ImageName)
                {
                    dbSeries.ImageName = seriesCreateDto.ImageName;
                    update++;
                }
                if (dbSeries.IsActive != seriesCreateDto.IsActive)
                {
                    dbSeries.IsActive = seriesCreateDto.IsActive;
                    update++;
                }
       
                foreach (var seriesAlias in seriesCreateDto.SeriesAliasCreateDtos)
                {
                    var found = false;
                    foreach (var dbSeriesAlias in dbSeries.SeriesAliases)
                    {
                        if ((dbSeriesAlias.IdType == seriesAlias.IdType) && (dbSeriesAlias.IdValue == seriesAlias.IdValue))
                        {
                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        update++;
                    }
                }
            
                if (update > 0)
                {
                    // TODO Map to Series here
                   var updatedSeries = await seriesRepository.UpdateAsync(dbSeries);
                   return updatedSeries;
                }
            }
        }

        return null;
    }
    
    public async Task<Series> SetAsInActiveAsync(Guid seriesId)
    {
        var series = await seriesRepository.GetAsync(seriesId);
        if (series == null)
        {
            throw new BusinessException(VideoServiceDomainErrorCodes.SeriesWithIdNotFound)
                .WithData("SeriesId", seriesId);
        }
        else
        {
            series.SetSeriesInactive();
            return await seriesRepository.UpdateAsync(series, autoSave: true);
        }
    }
}
