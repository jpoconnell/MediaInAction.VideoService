using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Episodegrpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediaInAction.VideoService.EpisodeNs;
using Microsoft.Extensions.Logging;


namespace MediaInAction.VideoService.Grpc;

public class PublicEpisodeGrpService : EpisodeGrpcService.EpisodeGrpcServiceBase 
{
    private readonly ILogger<PublicEpisodeGrpService> _logger;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly EpisodeManager _episodeManager;
    
    public PublicEpisodeGrpService(ILogger<PublicEpisodeGrpService> logger, 
        IEpisodeRepository episodeRepository,
        EpisodeManager episodeManager)
    {
        _logger = logger;
        _episodeRepository = episodeRepository;
        _episodeManager = episodeManager;
    }

    public override async Task<EpisodeModel> CreateUpdateEpisode(EpisodeModel request, ServerCallContext context)
    {
        var episodeCreateDto = TranslateEpisodeGrpc(request);
        var response = await _episodeManager.CreateUpdateAsync(episodeCreateDto);

        if (response != null)
        {
            var episodeModel = TranslateEpisode(response);
            return episodeModel;
        }

        return null;
    }
    
    public override async Task SearchEpisode(SearchRequest request, IServerStreamWriter<EpisodeModel> responseStream, ServerCallContext context)
    {
        var episode =
            await _episodeRepository.GetBySlugSeasonEpisode(request.Slug, request.Season, request.Episode);
        if (episode != null)
        {
            var episodeModel = TranslateEpisode(episode);
            await responseStream.WriteAsync(episodeModel);
        }
    }
    
    public override async Task<EpisodeModel> SearchOneEpisode(SearchRequest request, ServerCallContext context)
    {
        var episode =
            await _episodeRepository.GetBySlugSeasonEpisode(request.Slug, request.Season, request.Episode);
        if (episode != null)
        {
            var episodeModel = TranslateEpisode(episode);
            return episodeModel;
        }
        return null;
    }

    private EpisodeModel TranslateEpisode(Episode episode)
    {
        var episodeModel = new EpisodeModel();
        var airedDate = episode.AiredDate.ToUniversalTime();
        episodeModel.Season = episode.SeasonNum;
        episodeModel.Episode = episode.EpisodeNum;
        episodeModel.EpisodeName = episode.EpisodeName;
        episodeModel.AiredDate = Timestamp.FromDateTimeOffset(airedDate.ToLocalTime());
     
        foreach (var episodeAlias in episode.EpisodeAliases)
        {
            var newEpisodeAlias = new EpisodeAliasModel();
            newEpisodeAlias.IdType = episodeAlias.IdType;
            newEpisodeAlias.IdValue = episodeAlias.IdValue;
            //episodeModel.Aliases.Add(newEpisodeAlias);
            if (episodeAlias.IdType == "Slug")
            {
                episodeModel.Slug = episodeAlias.IdValue;
            }
        }
        return episodeModel;
    }

    private EpisodeCreateDto TranslateEpisodeGrpc(EpisodeModel request)
    {
        try
        {
            var episodeCreateDto = new EpisodeCreateDto();
            if (request.AiredDate != null)
            {
                episodeCreateDto.AiredDate = request.AiredDate.ToDateTime();
            }
            else
            {
                episodeCreateDto.AiredDate = null;
            }

            episodeCreateDto.Slug = request.Slug;
            episodeCreateDto.SeasonNum = request.Season;
            episodeCreateDto.EpisodeNum = request.Episode;
            if (request.EpisodeName != null)
            {
                episodeCreateDto.EpisodeName = request.EpisodeName;
            }
            else
            {
                episodeCreateDto.EpisodeName = "";
            }
         
            episodeCreateDto.EpisodeCreateAliases = new List<EpisodeAliasCreateDto>();
            
            foreach (var episodeAlias in request.EpisodeAliases)
            {
                var createEpisodeAlias = new EpisodeAliasCreateDto
                {
                    IdType = episodeAlias.IdType,
                    IdValue = episodeAlias.IdValue
                };
                episodeCreateDto.EpisodeCreateAliases.Add(createEpisodeAlias);
            }

            return episodeCreateDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
    
}
