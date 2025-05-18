using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Episodegrpc;
using Grpc.Net.Client;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodePublicService : ITraktEpisodePublicService, ITransientDependency
{
    private readonly IDistributedCache<TraktEpisodeDto, Guid> _cache;
    private readonly ILogger<TraktEpisodePublicService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly EpisodeGrpcService.EpisodeGrpcServiceClient _episodeGrpcServiceClient;
    private readonly GrpcChannel _channel;
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly TraktEpisodeManager _traktEpisodeManager;

    public TraktEpisodePublicService(
        IDistributedCache<TraktEpisodeDto, Guid> cache,
        ILogger<TraktEpisodePublicService> logger,
        IObjectMapper mapper,
        ITraktEpisodeRepository traktEpisodeRepository,
        TraktEpisodeManager traktEpisodeManager,
        ITraktShowRepository traktShowRepository,
        EpisodeGrpcService.EpisodeGrpcServiceClient episodeGrpcServiceClient)
    {
        _cache = cache;
        _logger = logger;
        _mapper = mapper;
        _traktEpisodeRepository = traktEpisodeRepository;
        _traktEpisodeManager = traktEpisodeManager;
        _channel = GrpcChannel.ForAddress("https://localhost:44356");
        _traktShowRepository = traktShowRepository;
        _episodeGrpcServiceClient = new EpisodeGrpcService.EpisodeGrpcServiceClient(_channel);
    }

    public async Task<TraktEpisodeDto> GetAsync(Guid episodeId)
    {
        return (await _cache.GetOrAddAsync(
            episodeId,
            () => GetOneEpisodeAsync(episodeId)
        ))!;
    }

    public async Task<TraktEpisodeDto> FindByTraktShowSlugSeasonEpisodeAsync(string slug, int seasonNum, int episodeNum)
    {
        try
        {
            var traktShow = await _traktShowRepository.FindBySlugAsync(slug);
            var traktEpisode =
                await _traktEpisodeRepository.FindByTraktShowSlugSeasonEpisodeAsync(traktShow.Id.ToString(), seasonNum, episodeNum);
            if (traktEpisode != null)
            {
                return MapToDto(traktEpisode);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private TraktEpisodeDto MapToDto(TraktEpisode episode)
    {
        var traktEpisodeAliasList = new List<TraktEpisodeAliasDto>();
        foreach (var episodeAlias in episode.TraktEpisodeAliases)
        {
            var newTraktEpisodeAlias = new TraktEpisodeAliasDto();
            newTraktEpisodeAlias.IdType = episodeAlias.IdType;
            newTraktEpisodeAlias.IdValue = episodeAlias.IdValue;
            traktEpisodeAliasList.Add(newTraktEpisodeAlias);
        }
        var traktEpisodeDto = new TraktEpisodeDto();
        traktEpisodeDto.Id = episode.Id;
        traktEpisodeDto.SeriesId = episode.SeriesId;
        traktEpisodeDto.EpisodeNum = episode.EpisodeNum;
        traktEpisodeDto.AiredDate = episode.AiredDate;
        traktEpisodeDto.TraktEpisodeAliasDtos = traktEpisodeAliasList;

        return traktEpisodeDto;
    }


    public async Task<List<TraktEpisodeDto>> GetListAsync()
    {
        var traktEpisodes = await _traktEpisodeRepository.GetListAsync();
        return _mapper.Map<List<TraktEpisode>, List<TraktEpisodeDto>>(traktEpisodes);
    }

    public async Task<List<TraktEpisodeDto>> GetEpisodesByShow(string slug)
    { 
        var traktShow = await _traktShowRepository.FindBySlugAsync(slug);
        var traktEpisodes = await _traktEpisodeRepository.GetEpisodesByShow(traktShow.Id.ToString());
        return _mapper.Map<List<TraktEpisode>, List<TraktEpisodeDto>>(traktEpisodes);
    }

    public async Task<TraktEpisodeDto> UpdateAsync(TraktEpisodeDto updatedShow)
    {
        var episode = await _traktEpisodeRepository.GetAsync(updatedShow.Id);
        episode.ExternalId = updatedShow.ExternalId;

        await _traktEpisodeRepository.UpdateAsync(episode);
        return MapToDto(episode);
    }

    public async Task<TraktEpisodeDto> CreateAsync(TraktEpisodeCreateDto traktEpisodeCreateDto)
    {
        var episode = await _traktEpisodeManager.CreateAsync(traktEpisodeCreateDto);
        if (episode == null)
        {
            return null;
        }
        else
        {
            var traktEpisodeDto = MapToDto(episode);
            return traktEpisodeDto;
        }
    }


    private async Task<TraktEpisodeDto> GetOneEpisodeAsync(Guid episodeId)
    {
        var request = new SearchRequest { Id = episodeId.ToString() };
        _logger.LogInformation("=== GRPC request {@request}", request);
        var response =  await _episodeGrpcServiceClient.SearchOneEpisodeAsync(request);
        _logger.LogInformation("=== GRPC response {@response}", response);
        return _mapper.Map<EpisodeModel, TraktEpisodeDto>(response) ??
               throw new UserFriendlyException(TraktServiceDomainErrorCodes.EpisodeNotFound);
    }
}