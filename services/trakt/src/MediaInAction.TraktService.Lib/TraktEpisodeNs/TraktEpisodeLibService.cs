using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Episodegrpc;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using Microsoft.Extensions.Logging;

using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.Lib.TraktEpisodeNs;

public class TraktEpisodeLibService : ITraktEpisodeLibService
{
    private readonly ILogger<TraktEpisodeLibService> _logger;
    private readonly ITraktEpisodePublicService _traktEpisodePublicService;
    private readonly IObjectMapper _mapper;
    
    public TraktEpisodeLibService(
        ILogger<TraktEpisodeLibService> logger,
        ITraktEpisodePublicService traktEpisodePublicService,
        IObjectMapper mapper)
    {
        _traktEpisodePublicService = traktEpisodePublicService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task UpdateAddFromDto(TraktEpisodeCreateDto episodeDto)
    {
        _logger.LogInformation("TraktEpisodeLibService:UpdateAddFromDto:" + episodeDto.Slug + ":" +
                               episodeDto.EpisodeNum);
        try
        {
            await CreateUpdateEpisode(episodeDto);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:UpdateAddFromDto:" + ex.Message);
        }
    }

    public async Task CreateUpdateEpisode(TraktEpisodeCreateDto traktEpisodeCreateDto)
    {
        var dbEpisode = await _traktEpisodePublicService.FindByTraktShowSlugSeasonEpisodeAsync(
            traktEpisodeCreateDto.Slug,
            traktEpisodeCreateDto.SeasonNum,
            traktEpisodeCreateDto.EpisodeNum);
        if (dbEpisode == null)
        {
            var createdEpisode = await _traktEpisodePublicService.CreateAsync(traktEpisodeCreateDto);
            await CreateGrpcEpisode(traktEpisodeCreateDto);
        }
        else
        {
            if (dbEpisode.ExternalId == null)
            {
                // resent create episode
                var rtn = await SearchGrpcEpisode(traktEpisodeCreateDto);
                if (rtn != null)
                {
                    _logger.LogInformation("TraktEpisodeLibService:CreateUpdateEpisode:" + rtn);
                }
                else
                {
                    _logger.LogError("SearchGrpcEpisode returned null" );
                }
            }
            else
            {
                // search episode 
                var episodeModel = await SearchGrpcEpisode(traktEpisodeCreateDto);
                _logger.LogInformation("TraktEpisodeLibService:SearchGrpcEpisode:" + episodeModel.Slug);
            }
        }
    }

    public async Task<List<TraktEpisodeDto>> GetListAsync()
    {
        var traktEpisodeDtos = await _traktEpisodePublicService.GetListAsync();
     //   var traktEpisodeDtos = MapToDtos(traktEpisodes);
        return traktEpisodeDtos;
    }

    public async Task<List<TraktEpisodeDto>> GetEpisodeByShow(string slug)
    {
        try
        {
            var traktEpisodeDtos = await _traktEpisodePublicService.GetEpisodesByShow(slug);
            if ((traktEpisodeDtos != null) && (traktEpisodeDtos.Count > 0))
            {
                //var traktEpisodeDtos = MapToDtos(traktEpisodes);
                return traktEpisodeDtos;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:GetEpisodeByShow" + ex.Message);
            return null;
        }
    }
    
    private TraktEpisodeCreateDto ConvertToCrateEpisodeDto(CollectionEpisodeDto episodeDto)
    {
        var createEpisodeAliasList = new List<TraktEpisodeAliasCreateDto>();
        var traktId = "";
        foreach (var episodeAlias in episodeDto.CollectionEpisodeAliasDtos)
        {
            createEpisodeAliasList.Add(new TraktEpisodeAliasCreateDto
            {
                IdType = episodeAlias.IdType,
                IdValue = episodeAlias.IdValue
            });
            if (episodeAlias.IdType == "trakt")
            {
                traktId = episodeAlias.IdValue;
            }
        }

        var createEpisode = new TraktEpisodeCreateDto
        {
            Slug = episodeDto.ShowSlug,
            SeasonNum = episodeDto.SeasonNum,
            EpisodeNum = episodeDto.EpisodeNum,
            EpisodeName = episodeDto.EpisodeName,
            AiredDate = episodeDto.AiredDate,
     
            TraktEpisodeCreateAliases = createEpisodeAliasList
        };

        return createEpisode;
    }

    private async Task<Guid> UpdateTrakEpisode(TraktEpisodeDto dbEpisodeDto,
        CollectionEpisodeDto episodeDto)
    {
        var updatedShow = dbEpisodeDto;
        updatedShow.Slug = episodeDto.ShowSlug;
        updatedShow.SeasonNum = episodeDto.SeasonNum;
        updatedShow.EpisodeNum = episodeDto.EpisodeNum;
        updatedShow.AiredDate = episodeDto.AiredDate;
        updatedShow.EpisodeName = episodeDto.EpisodeName;

        foreach (var alias in episodeDto.CollectionEpisodeAliasDtos)
        {
            var found = false;
            foreach (var dbAlias in dbEpisodeDto.TraktEpisodeAliasDtos)
            {
                if ((dbAlias.IdType == alias.IdType) && (dbAlias.IdValue == alias.IdValue))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                var newTraktEpisodeAliasCreateDto = new TraktEpisodeAliasCreateDto();
               // dbEpisode.TraktEpisodeAliases.Add(newTraktEpisodeAliasCreateDto);
                _logger.LogInformation("Alias Added:" + alias.IdType + ":" + alias.IdValue);
            }
        }

        var updatedEpisode = await _traktEpisodePublicService.UpdateAsync(updatedShow);
        await SendEpisodeUpdateEventAsync(updatedEpisode);
        return updatedEpisode.Id;
    }

    private async Task SendEpisodeUpdateEventAsync(TraktEpisodeDto updatedShow)
    {
        _logger.LogInformation("Sending TraktEpisodeUpdated Event: " +
                               updatedShow.Slug + ":" + updatedShow.EpisodeNum);

    }
    
    private List<TraktEpisodeDto> MapToDtos(List<TraktEpisode> traktEpisodes)
    {
        var traktEpisodeDtos = new List<TraktEpisodeDto>();
        foreach (var traktEpisode in traktEpisodes)
        {
            traktEpisodeDtos.Add(MapToDto(traktEpisode));
        }

        return traktEpisodeDtos;
    }

    public async Task ResendUnAcceptedEpisodesList()
    {
        var episodeDtos = await _traktEpisodePublicService.GetListAsync();
        foreach (var episode in episodeDtos)
        {
           // var episodeDto = MapToDto(episode);
        }
    }
    
    private TraktEpisodeDto MapToDto(TraktEpisode traktEpisode)
    {
        try
        {
            var traktEpisodeDto = new TraktEpisodeDto
            {
               // Id = traktEpisode.Id,
                SeriesId = traktEpisode.SeriesId,
                SeasonNum = traktEpisode.SeasonNum,
                EpisodeNum = traktEpisode.EpisodeNum,
                AiredDate = traktEpisode.AiredDate,
                EpisodeName = traktEpisode.EpisodeName,
            };
            if (traktEpisodeDto.TraktEpisodeAliasDtos == null)
            {
                traktEpisodeDto.TraktEpisodeAliasDtos = new List<TraktEpisodeAliasDto>();
            }

            foreach (var episodeAlias in traktEpisode.TraktEpisodeAliases)
            {
                traktEpisodeDto.TraktEpisodeAliasDtos.Add(new TraktEpisodeAliasDto
                {
                    IdType = episodeAlias.IdType,
                    IdValue = episodeAlias.IdValue
                });
            }

            return traktEpisodeDto;
        }

        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:MapToDto" + ex.Message);
            return null;
        }
    }
    
    private async Task<TraktEpisodeDto> CreateGrpcEpisode(TraktEpisodeCreateDto traktEpisodeCreateDto)
    {
        try
        {
            var episodeModel = MapToEpisodeModel(traktEpisodeCreateDto);
            var serverUrl = "https://localhost:44356";
            using var channel = GrpcChannel.ForAddress(serverUrl);
            var client = new EpisodeGrpcService.EpisodeGrpcServiceClient(channel);
            
            var reply = await client.CreateUpdateEpisodeAsync(episodeModel);
            _logger.LogInformation("EpisodeLibService.CreateGrpcEpisode:" + episodeModel.Slug +" "+ reply.Season.ToString() +" "+ reply.Episode.ToString());
            var rtnTraktEpisode = _mapper.Map<EpisodeModel, TraktEpisodeDto>(reply);
            return rtnTraktEpisode;
        }
        catch
        {
            return null;
        }
    }
    
    private async Task<EpisodeModel> SearchGrpcEpisode(TraktEpisodeCreateDto traktEpisodeCreateDto)
    {
        var episodeAliasModels = new RepeatedField<EpisodeAliasModel>();
        var episodeSearchRequest = new SearchRequest();
        episodeSearchRequest.Id = "";
        episodeSearchRequest.Slug = traktEpisodeCreateDto.Slug;
        episodeSearchRequest.Season = traktEpisodeCreateDto.SeasonNum;
        episodeSearchRequest.Episode = traktEpisodeCreateDto.EpisodeNum;
        
        var serverUrl = "https://localhost:44356";
        using var channel = GrpcChannel.ForAddress(serverUrl);
        var client = new EpisodeGrpcService.EpisodeGrpcServiceClient(channel);
        var reply = await client.SearchOneEpisodeAsync(episodeSearchRequest );

        if (reply.Externalid != null)
        {
            var traktEpisodeDto = await _traktEpisodePublicService.FindByTraktShowSlugSeasonEpisodeAsync(
                traktEpisodeCreateDto.Slug, traktEpisodeCreateDto.SeasonNum, traktEpisodeCreateDto.EpisodeNum);
            traktEpisodeDto.ExternalId = reply.Externalid;
            var updatedTraktEpisode = await _traktEpisodePublicService.UpdateAsync(traktEpisodeDto);
        }

        return reply;
    }


    private EpisodeModel MapToEpisodeModel(TraktEpisodeCreateDto traktEpisodeCreateDto)
    {
        var episodeAliasModels = new RepeatedField<EpisodeAliasModel>();
        var episodeModel = new EpisodeModel();
        episodeModel.Slug = traktEpisodeCreateDto.Slug;
        episodeModel.Season = traktEpisodeCreateDto.SeasonNum;
        episodeModel.Episode = traktEpisodeCreateDto.EpisodeNum;
        episodeModel.Slug = traktEpisodeCreateDto.Slug;

        if (traktEpisodeCreateDto.AiredDate != null)
        {
            var timestamp = Timestamp.FromDateTime((DateTime)traktEpisodeCreateDto.AiredDate);
            episodeModel.AiredDate = timestamp;
        }
        
        foreach (var traktEpisodeAlias in traktEpisodeCreateDto.TraktEpisodeCreateAliases)
        {
            if (!traktEpisodeAlias.IdType.IsNullOrEmpty())
            {
                var myAlias = new EpisodeAliasModel
                {
                    IdType = traktEpisodeAlias.IdType,
                    IdValue = traktEpisodeAlias.IdValue
                };
                episodeModel.EpisodeAliases.Add(myAlias); 
            }
        }
        return episodeModel;
    }
}