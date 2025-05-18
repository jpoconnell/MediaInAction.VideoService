using AutoMapper;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;

namespace MediaInAction.VideoService.Blazor;

public class VideoServiceBlazorAutoMapperProfile : Profile
{
    public VideoServiceBlazorAutoMapperProfile()
    {
        CreateMap<SeriesDto, CreateUpdateSeriesDto>();
       // CreateMap<AuthorDto, UpdateAuthorDto>();
    }
}
