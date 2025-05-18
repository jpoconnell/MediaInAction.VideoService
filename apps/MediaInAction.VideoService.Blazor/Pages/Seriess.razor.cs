using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.Blazor.Pages;

public partial class Seriess
{
    private IReadOnlyList<SeriesDto> SeriesList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateSeries { get; set; }
    private bool CanEditSeries { get; set; }
    private bool CanDeleteSeries { get; set; }

    private CreateUpdateSeriesDto NewSeries { get; set; }

    private Guid EditingSeriesId { get; set; }
    private CreateUpdateSeriesDto EditingSeries { get; set; }

    private Modal CreateSeriesModal { get; set; }
    private Modal EditSeriesModal { get; set; }

    private Validations CreateValidationsRef;

    private Validations EditValidationsRef;

    public Seriess()
    {
        NewSeries = new CreateUpdateSeriesDto();
        EditingSeries = new CreateUpdateSeriesDto();
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetSeriesAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateSeries = await AuthorizationService
            .IsGrantedAsync(VideoServicePermissions.Seriess.Create);

        CanEditSeries = await AuthorizationService
            .IsGrantedAsync(VideoServicePermissions.Seriess.Update);

        CanDeleteSeries = await AuthorizationService
            .IsGrantedAsync(VideoServicePermissions.Seriess.Delete);
    }

    private async Task GetSeriesAsync()
    {
        var filter = new PagedAndSortedResultRequestDto();
        filter.MaxResultCount = PageSize;
        filter.SkipCount = CurrentPage * PageSize;
        filter.Sorting = CurrentSorting;

        var result = await SeriesNs.SeriesAppService.GetListAsync(filter);

        SeriesList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<SeriesDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetSeriesAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateSeriesModal()
    {
        CreateValidationsRef.ClearAll();

        NewSeries = new CreateUpdateSeriesDto();
        CreateSeriesModal.Show();
    }

    private void CloseCreateSeriesModal()
    {
        CreateSeriesModal.Hide();
    }

    private void OpenEditSeriesModal(SeriesDto series)
    {
        EditValidationsRef.ClearAll();

        EditingSeriesId = series.Id;
        EditingSeries = ObjectMapper.Map<SeriesDto, CreateUpdateSeriesDto>(series);
        EditSeriesModal.Show();
    }

    private async Task DeleteSeriesAsync(SeriesDto series)
    {
        var confirmMessage = L["SeriesDeletionConfirmationMessage", series.SeriesName];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await SeriesNs.SeriesAppService.DeleteAsync(series.Id);
        await GetSeriesAsync();
    }

    private void CloseEditSeriesModal()
    {
        EditSeriesModal.Hide();
    }

    private async Task CreateSeriesAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            await SeriesNs.SeriesAppService.CreateAsync(NewSeries);
            await GetSeriesAsync();
            CreateSeriesModal.Hide();
        }
    }

    private async Task UpdateSeriesAsync()
    {
        if (await EditValidationsRef.ValidateAll())
        {
            await SeriesNs.SeriesAppService.UpdateAsync(EditingSeriesId, EditingSeries);
            await GetSeriesAsync();
            await EditSeriesModal.Hide();
        }
    }
}
