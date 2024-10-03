using AutoMapper;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.MovieNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.AutoMapper;

namespace MediaInAction.VideoService
{
    public class VideoServiceApplicationAutoMapperProfile : Profile
    {
        public VideoServiceApplicationAutoMapperProfile()
        {
            CreateMap<Series, SeriesDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Episode, EpisodeDto>();
        }
    }
}