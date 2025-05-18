using System;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MediaInAction.TraktService.TraktShowNs;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using Microsoft.Extensions.Logging;
using Seriesgrpc;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.Lib.TraktShowNs;

public class TraktShowLibService : ITraktShowLibService
{
    private readonly ILogger<TraktShowLibService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly TraktShowPublicService _traktShowPublicService;
    
    public TraktShowLibService(
        ILogger<TraktShowLibService> logger,
        TraktShowPublicService traktShowPublicService,
        IObjectMapper mapper)
    {
        _traktShowPublicService = traktShowPublicService;
        _logger = logger;
        _mapper = mapper; 
    }

    public async Task UpdateAddFromDto(TraktShowCreateDto show)
    {
        _logger.LogInformation("TraktShowLibService.UpdateAddFromDto:" + show.Name);
        try 
        { 
            await CreateUpdateShow(show);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktShowLibService.UpdateAddFromDto:" +ex.Message);
        }
    }

    public async Task ResendUnAcceptedShowsList()
    {  
        var filter = new GetTraktShowListDto();
        filter.Sorting = "slug";
        var showList = await _traktShowPublicService.GetListAsync(filter);
        foreach (var traktShow in showList)
        {
           await _traktShowPublicService.CreateShow(traktShow);
        }
    }

    private async Task CreateUpdateShow(TraktShowCreateDto traktShowCreateDto)
    {
        // see if it exists in local db
        var dbShow = await _traktShowPublicService.GetUniqueShow(traktShowCreateDto);

        if (dbShow == null)
        {
            // save to database
            var returnedShow = await _traktShowPublicService.CreateAsync(traktShowCreateDto);
            
            if (returnedShow != null)
            {
                _logger.LogInformation("Show created:" + traktShowCreateDto.Name + ":" +
                                 traktShowCreateDto.FirstAiredYear.ToString());
            }
            // send to video service
            await CreateGrpcSeries(traktShowCreateDto);
        }
        else
        {
            if (dbShow.ExternalId == null)
            {
                // search series 
                var seriesModel = await SearchGrpcSeries(traktShowCreateDto);
                if (seriesModel != null)
                {
                    var update = new UpdateTraktShowDto
                    {
                        Name = dbShow.Name,
                        FirstAiredYear = dbShow.FirstAiredYear,
                        ExternalId = seriesModel.Externalid
                    };
                    await _traktShowPublicService.UpdateAsync(dbShow.Id, update);
                }
                else
                {
                    var seriesModel2 = await CreateGrpcSeries(traktShowCreateDto);
                }
            }
        }
    }

    private async Task<SeriesModel> SearchGrpcSeries(TraktShowCreateDto traktShowCreateDto)
    {
        try
        {
            var seriesAliasModels = new RepeatedField<SeriesAliasModel>();
            var seriesSearchRequest = new SearchRequest
            {
                Id = "",
                Slug = traktShowCreateDto.Slug,
                Name = traktShowCreateDto.Name,
                Year = traktShowCreateDto.FirstAiredYear,
                AliasValue = ""
            };

            var serverUrl = "https://localhost:44356";
            using var channel = GrpcChannel.ForAddress(serverUrl);
            var client = new SeriesGrpcService.SeriesGrpcServiceClient(channel);
            var reply = await client.SearchOneSeriesAsync(seriesSearchRequest);

            if (reply.Externalid != null)
            {
                var traktShowDto = await _traktShowPublicService.GetUniqueShow(traktShowCreateDto);
                traktShowDto.ExternalId = reply.Externalid;
                var traktShowCreateDto2 = _mapper.Map<TraktShowDto, UpdateTraktShowDto>(traktShowDto);
                await _traktShowPublicService.UpdateAsync(traktShowDto.Id, traktShowCreateDto2);
            }
            else
            {
                _logger.LogError("SearchGrpcSeries returned null");
            }

            return reply;
        }
        catch (Exception ex)
        {
            _logger.LogError("TraktShowLibService.SearchGrpcSeries:" + ex.Message);
            return null;
        }
    }

    private async Task<SeriesModel> CreateGrpcSeries(TraktShowCreateDto traktShowCreateDto)
    {
        try
        {
            var seriesAliasModels = new RepeatedField<SeriesAliasModel>();
            var seriesModel = new SeriesModel
            {
                Name = traktShowCreateDto.Name,
                Year = traktShowCreateDto.FirstAiredYear,
                Slug = traktShowCreateDto.Slug
            };
            if (traktShowCreateDto.ImageName != null)
            {
                seriesModel.ImageName = traktShowCreateDto.ImageName;
            }
            else
            {
                seriesModel.ImageName = "";
            }

            foreach (var traktShowAlias in traktShowCreateDto.TraktShowCreateAliases)
            {
                if (!traktShowAlias.IdType.IsNullOrEmpty())
                {
                    var myAlias = new SeriesAliasModel
                    {
                        IdType = traktShowAlias.IdType,
                        IdValue = traktShowAlias.IdValue
                    };
                    seriesModel.SeriesAliases.Add(myAlias);
                }
            }

            var serverUrl = "https://localhost:44356";
            using var channel = GrpcChannel.ForAddress(serverUrl);
            var client = new SeriesGrpcService.SeriesGrpcServiceClient(channel);
            _logger.LogInformation("Creating new series:" + traktShowCreateDto.Name);
            var reply = await client.CreateUpdateSeriesAsync(seriesModel);
            _logger.LogInformation("New series created:" + reply.Name + " " + reply.Year.ToString());
            return reply;
        }
        catch (Exception ex)
        {
            _logger.LogError("TraktShowLibService.SearchGrpcSeries:" + ex.Message);
            return null;
        }
    }
}
