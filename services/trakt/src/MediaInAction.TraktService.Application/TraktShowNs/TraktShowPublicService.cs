using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using Microsoft.Extensions.Logging;
using Seriesgrpc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowPublicService : ITraktShowPublicService, ITransientDependency
{
    private readonly ILogger<TraktShowPublicService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly SeriesGrpcService.SeriesGrpcServiceClient _seriesGrpcServiceClient;
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly TraktShowManager _traktShowManager;
    
    public TraktShowPublicService(
        ILogger<TraktShowPublicService> logger,
        ITraktShowRepository traktShowRepository,
        TraktShowManager traktShowManager,
        IObjectMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _traktShowRepository = traktShowRepository;
        _traktShowManager = traktShowManager;
        _seriesGrpcServiceClient = new SeriesGrpcService.SeriesGrpcServiceClient(Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:8181"));
    }
    
    public Task CreateShow(TraktShowDto traktShow)
    {
        try
        {
            //var seriesModel = _mapper.Map<TraktShowDto, SeriesModel>(traktShow);
            var seriesModel = MapToModel(traktShow);
            var response = _seriesGrpcServiceClient.CreateUpdateSeries(seriesModel);
            _logger.LogInformation("=== GRPC response {@response}", response);
            return Task.FromResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating series");
            return null;
        }
    }

    public async Task<TraktShowDto> UpdateAsync(Guid id, UpdateTraktShowDto input)
    {
        var traktShow = await _traktShowRepository.GetAsync(id);
        traktShow.FirstAiredYear = input.FirstAiredYear;
        traktShow.ExternalId = input.ExternalId;
        var updatedShow =  await _traktShowRepository.UpdateAsync(traktShow, true);
        return _mapper.Map<TraktShow, TraktShowDto>(updatedShow);
    }
    
    private SeriesModel MapToModel(TraktShowDto traktShow)
    {
        var seriesModel = _mapper.Map<TraktShowDto, SeriesModel>(traktShow);
        seriesModel.Year = traktShow.FirstAiredYear;
        foreach (var traktAlias in traktShow.TraktShowAliasDtos)
        {
            var alias = new SeriesAliasModel();
            alias.IdType = traktAlias.IdType;
            alias.IdValue = traktAlias.IdValue;
            seriesModel.SeriesAliases.Add(alias);
        }

        return seriesModel;
    }

    public async Task<TraktShowDto> GetUniqueShow(TraktShowCreateDto traktShowCreateDto)
    {
        var traktShow = await _traktShowRepository.FindByTraktShowNameYearAsync(traktShowCreateDto.Name,traktShowCreateDto.FirstAiredYear);
        return _mapper.Map<TraktShow, TraktShowDto>(traktShow);
    }

    public async Task<TraktShowDto> CreateAsync(TraktShowCreateDto traktShowCreateDto)
    {
        var show = await _traktShowManager.CreateAsync(traktShowCreateDto);
        if (show != null)
        {
            var traktShowDto = MapToDto(show);
            // var traktShowDto = ObjectMapper.Map<TraktShow, TraktShowDto>(show);
            return traktShowDto;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<TraktShowDto>> GetListAsync(GetTraktShowListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(TraktShow.Slug);
        }

        var shows = await _traktShowRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );
        
        return new List<TraktShowDto>(_mapper.Map<List<TraktShow>, List<TraktShowDto>>(shows));
    }
    
    private TraktShowDto MapToDto(TraktShow show)
    {
        if (show.TraktShowAliases == null)
        {
            show.TraktShowAliases = new List<TraktShowAlias>();
            return null;
        }
        else
        {
            var returnTraktShowAliasDtoList = new List<TraktShowAliasDto>();
            foreach (var alias in show.TraktShowAliases)
            {
                var returnTraktShowAliasDto = new TraktShowAliasDto
                {
                    IdType = alias.IdType,
                    IdValue = alias.IdValue
                };
                returnTraktShowAliasDtoList.Add(returnTraktShowAliasDto);
            }
            var returnTraktShowDto = new TraktShowDto
            {
                Id = show.Id,
                Name = show.Name,
                FirstAiredYear = show.FirstAiredYear,
                Slug = show.Slug,
                TraktShowAliasDtos = returnTraktShowAliasDtoList
            };
            return returnTraktShowDto;
        }
    }

}