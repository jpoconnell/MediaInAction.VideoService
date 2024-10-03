using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktShowAliasNs;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktShowNs.Specifications;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs;

[Authorize(TraktServicePermissions.TraktShow.Default)]
public class TraktShowAppService : TraktServiceAppService, ITraktShowAppService
{
    private readonly ILogger<TraktShowAppService> _logger;
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly TraktShowManager _traktShowManager;
    
    public TraktShowAppService(
        ITraktShowRepository traktShowRepository,
        ILogger<TraktShowAppService> logger,
        TraktShowManager traktShowManager)
    {
        _traktShowRepository = traktShowRepository;
        _traktShowManager = traktShowManager;
        _logger = logger;
    }


    [Authorize(TraktServicePermissions.TraktShow.Dashboard)]
    public async Task<TraktShowDashboardDto> GetDashboardAsync(TraktShowDashboardInput input)
    {
        return new TraktShowDashboardDto()
        {
            TraktShowStatusDto = await GetCountOfTotalShowStatusAsync(input.Filter)
            /*
            TopSellings = await GetTopSellingAsync(input.Filter),

            Trakts = await GetPercentOfTotalTraktAsync(input.Filter)
            */
        };
    }

    private async Task<List<TraktShowStatusDto>> GetCountOfTotalShowStatusAsync(string filter)
    {
        ISpecification<TraktShow> specification = SpecificationFactory.Create(filter);
        var shows = await _traktShowRepository.GetDashboardAsync(specification);
        return CreateShowStatusDtoMapping(shows);
    }

    private List<TraktShowStatusDto> CreateShowStatusDtoMapping(List<TraktShow> shows)
    {
        var showStatus = shows
            .GroupBy(p => p.TraktStatus)
            .Select(p => new TraktShowStatusDto { CountOfStatusShow = p.Count(), ShowStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusShow)
            .ToList();
        

        showStatus.Add(new TraktShowStatusDto() { ShowStatus = "test", CountOfStatusShow   = 3 });

        return showStatus;
    }
    
    [AllowAnonymous]
    public async Task<TraktShowDto> CreateAsync(TraktShowCreateDto input)
    {
        var show = await _traktShowManager.CreateAsync(input);
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