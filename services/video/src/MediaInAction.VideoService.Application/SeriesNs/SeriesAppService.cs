using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.SeriesNs;

[Authorize(VideoServicePermissions.Seriess.Default)]
public class SeriesAppService :
    CrudAppService<
        Series, //The Series entity
        SeriesDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSeriesDto>, //Used to create/update a book
    ISeriesAppService //implement the ISeriesAppService
{
    private readonly ISeriesAliasRepository _seriesAliasRepository;

    public SeriesAppService(
        IRepository<Series, Guid> repository,
        ISeriesAliasRepository seriesAliasRepository)
        : base(repository)
    {
        _seriesAliasRepository = seriesAliasRepository;
        GetPolicyName = VideoServicePermissions.Seriess.Default;
        GetListPolicyName = VideoServicePermissions.Seriess.Default;
        CreatePolicyName = VideoServicePermissions.Seriess.Create;
        UpdatePolicyName = VideoServicePermissions.Seriess.Update;
        DeletePolicyName = VideoServicePermissions.Seriess.Create;
    }

    public override async Task<SeriesDto> GetAsync(Guid id)
    {
        //Get the IQueryable<Series> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join books and authors
        var query = from series in queryable
                    join seriesAlias in await _seriesAliasRepository.GetQueryableAsync() on series.Id equals seriesAlias.SeriesId
                    where series.Id == id
                    select new { series, seriesAlias };

        //Execute the query and get the book with author
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Series), id);
        }

        var seriesDto = ObjectMapper.Map<Series, SeriesDto>(queryResult.series);
        seriesDto.SeriesId = queryResult.seriesAlias.SeriesId;
        return seriesDto;
    }

    public override async Task<PagedResultDto<SeriesDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        //Get the IQueryable<Series> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join books and authors
        var query = from series in queryable
            join seriesAlias in await _seriesAliasRepository.GetQueryableAsync() on series.Id equals seriesAlias
                .SeriesId
                    select new { series, seriesAlias };

        //Paging
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of SeriesDto objects
        var seriesDtos = queryResult.Select(x =>
        {
            var seriesDto = ObjectMapper.Map<Series, SeriesDto>(x.series);
            seriesDto.SeriesId = x.seriesAlias.SeriesId;
            return seriesDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<SeriesDto>(
            totalCount,
            seriesDtos
        );
        
    }

    public async Task<ListResultDto<SeriesAliasLookupDto>> GetSeriesAliasLookupAsync()
    {
        var seriesAliasList = await _seriesAliasRepository.GetListAsync();
        return null;
        /*
        return new ListResultDto<SeriesAliasLookupDto>(
            ObjectMapper.Map<List<Series>, List<SeriesAliasLookupDto>>(seriesAliasList)
        );
        */
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"series.{nameof(Series.Name)}";
        }

        if (sorting.Contains("idValue", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "authorName",
                "author.IdType",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"series.{sorting}";
    }

    public Task<object> GetDashboardAsync(DashboardInput dashboardInput)
    {
        throw new NotImplementedException();
    }

    public Task<SeriesDto> GetSeriessAsync(GetSeriessInput input)
    {
        throw new NotImplementedException();
    }
}
