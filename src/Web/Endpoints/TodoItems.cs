using VideoService2.Application.Common.Models;
using VideoService2.Application.Series.Commands.UpdateSeriesDetail;
using VideoService2.Application.Series.Queries.GetSeriesListWithPagination;
using Microsoft.AspNetCore.Http.HttpResults;
using VideoService2.Application.Series.Commands.CreateSeries;
using VideoService2.Application.Series.Commands.DeleteSeries;
using VideoService2.Application.Series.Commands.UpdateSeries;
using VideoService2.Application.SeriesAliases.Commands.DeleteSeriesAlias;
using VideoService2.Application.SeriesAliases.Queries.GetSeriesAliasesWithPagination;
using VideoService2.Domain.Entities;

namespace VideoService2.Web.Endpoints;

public class SeriesAliases : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetSeriesListWithPagination)
            .MapPost(CreateTodoItem)
            .MapPut(UpdateTodoItem, "{id}")
            .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteTodoItem, "{id}");
    }

    public async Task<Ok<PaginatedList<SeriesAliasBriefDto>>> GetSeriesListWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateTodoItem(ISender sender, CreateSeriesCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Series)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateTodoItem(ISender sender, int id, UpdateSeriesCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, BadRequest>> UpdateTodoItemDetail(ISender sender, int id, UpdateSeriesDetailCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        
        await sender.Send(command);
        
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteTodoItem(ISender sender, int id)
    {
        await sender.Send(new DeleteSeriesCommand(id));

        return TypedResults.NoContent();
    }
}
