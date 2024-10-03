using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktShowAliasNs;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.Lib.TraktShowNs;

public class TraktShowLibService : ITraktShowLibService
{
    private readonly ILogger<TraktShowLibService> _logger;
    private readonly TraktShowManager _traktShowManager;
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly IObjectMapper _mapper;

    
    public TraktShowLibService(
        ILogger<TraktShowLibService> logger,
        TraktShowManager traktShowManager,
        ITraktShowRepository traktShowRepository,   
        IObjectMapper mapper)
    {
        _traktShowManager = traktShowManager;
        _traktShowRepository = traktShowRepository;
        _logger = logger;
        _mapper = mapper; 
    }

    public async Task UpdateAddFromDto(CollectionShowDto show)
    {
        _logger.LogInformation("TraktShowLibService.UpdateAddFromDto:" + show.Name);
        try 
        { 
            var dbShowId = await CreateUpdateShow(show);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktShowLibService.UpdateAddFromDto:" +ex.Message);
        }
    }
    
    public async Task<List<TraktShowDto>> GetShows()
    {
        var returnList = new List<TraktShowDto>();
        var shows = await _traktShowRepository.GetListAsync();
        foreach (var show in shows)
        {
            var showDto = new TraktShowDto();
            showDto.Name = show.Name;
            showDto.FirstAiredYear = show.FirstAiredYear;
            showDto.Slug = show.Slug;
            returnList.Add(showDto);
        }
        return returnList;
    }

    public async Task<List<TraktShowDto>> GetChangedShows()
    {
        var returnList = new List<TraktShowDto>();
        var showList = await _traktShowRepository.GetChangedListAsync();
        foreach (var show in showList)
        {
            var showDto = new TraktShowDto();
            showDto.Name = show.Name;
            showDto.FirstAiredYear = show.FirstAiredYear;
            showDto.Slug = show.Slug;
            returnList.Add(showDto);
        }
        return returnList;
    }

    public async Task ResendUnAcceptedShowsList()
    {
        var shows = await _traktShowRepository.GetChangedListAsync();
   
    }

    public async Task<TraktShowDto> GetBySlug(string slug)
    {
        var traktShow = await _traktShowRepository.FindBySlug(slug);
        return MapToShowDto(traktShow);
    }
    
    private TraktShowDto MapToShowDto(TraktShow traktShow)
    {
        var showDto = new TraktShowDto
        {
            Name = traktShow.Name,
            FirstAiredYear = traktShow.FirstAiredYear
        };

        showDto.TraktShowAliasDtos = new List<TraktShowAliasDto>();

        foreach (var showAlias in traktShow.TraktShowAliases)
        {
            var showAliasDto = new TraktShowAliasDto
            {
                IdType = showAlias.IdType,
                IdValue = showAlias.IdValue
            };
            showDto.TraktShowAliasDtos.Add(showAliasDto);
        }
        return showDto;
    }

    private async Task<Guid> CreateUpdateShow(CollectionShowDto traktShowDto)
    {
        var showAliases = new List<TraktShowAliasCreateDto>();
        foreach (var alias in traktShowDto.CollectionShowAliasDtos)
        {
            if ((alias.IdType.IsNullOrEmpty()) || (alias.IdValue.IsNullOrEmpty()))
            {
                continue;
            }
            var myAliasPair = new TraktShowAliasCreateDto();
            myAliasPair.IdType = alias.IdType;
            myAliasPair.IdValue = alias.IdValue;
            showAliases.Add(myAliasPair);
        }

        var traktShowCreateDto = new TraktShowCreateDto
        {
            Name = traktShowDto.Name,
            FirstAiredYear = traktShowDto.FirstAiredYear,
            Slug = traktShowDto.Slug,
            TraktShowCreateAliases = showAliases
        };
        
        var dbShow =
            await _traktShowRepository.FindByTraktShowNameYearAsync(traktShowDto.Name,
                traktShowDto.FirstAiredYear);
        if (dbShow == null)
        {
            var returnedShow = await _traktShowManager.CreateAsync(traktShowCreateDto);
            
            if (returnedShow != null)
            {
                _logger.LogInformation("Show created:" + traktShowDto.Name + ":" +
                                       traktShowDto.FirstAiredYear.ToString());
            }
            return returnedShow.Id;
        }
        else
        {
            var returnId = Guid.Empty;
            var diff = CompareTraktShow(dbShow, traktShowDto);

            if (diff == true)
            {
                returnId = await UpdateTrakShow(dbShow, traktShowDto);
            }
            else
            {
                returnId = Guid.Empty;
            }
            return returnId;
        }
    }
    
    private async Task<Guid> UpdateTrakShow(TraktShow dbShow, 
        CollectionShowDto traktShowDto)
    {
        var updatedShow = dbShow;
        updatedShow.Slug = traktShowDto.Slug;
        updatedShow.Name = traktShowDto.Name;
        updatedShow.FirstAiredYear = traktShowDto.FirstAiredYear;
        
        foreach (var alias in traktShowDto.CollectionShowAliasDtos)
        {
            var found = false;
            foreach (var dbAlias in dbShow.TraktShowAliases)
            {
                if ((dbAlias.IdType == alias.IdType) && (dbAlias.IdValue == alias.IdValue))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                var newTraktShowAlias = new TraktShowAlias();
                newTraktShowAlias.IdType = alias.IdType;
                newTraktShowAlias.IdValue = alias.IdValue;
                updatedShow.TraktShowAliases.Add(newTraktShowAlias);
            }
        }

        await  _traktShowRepository.UpdateAsync(updatedShow, true);
        return dbShow.Id;
    }

    private bool CompareTraktShow(
        TraktShow dbShow, 
        CollectionShowDto traktShowDto)
    {
        var diff = new bool();
        diff = false;
        foreach (var alias in  traktShowDto.CollectionShowAliasDtos)
        {
            bool found = false;
            if (!alias.IdType.IsNullOrEmpty())
            {
                foreach (var dbAlias in dbShow.TraktShowAliases)
                {
                    if ((dbAlias.IdType == alias.IdType) && (dbAlias.IdValue == alias.IdValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    diff = true;
                }
            }
        }

        if (dbShow.Slug != traktShowDto.Slug)
        {
            diff = true;
        }
        
        if (dbShow.Name != traktShowDto.Name)
        {
            diff = true;
        }
        if (dbShow.FirstAiredYear != traktShowDto.FirstAiredYear)
        {
            diff = true;
        }
        
        return diff;
    }
}
