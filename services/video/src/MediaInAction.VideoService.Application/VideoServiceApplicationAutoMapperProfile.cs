using AutoMapper;
using Episodegrpc;
using Mappergrpc;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.MovieNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using MediaInAction.VideoService.ToBeMappedNs;
using Moviegrpc;
using Volo.Abp.AutoMapper;

namespace MediaInAction.VideoService
{
    public class VideoServiceApplicationAutoMapperProfile : Profile
    {
        public VideoServiceApplicationAutoMapperProfile()
        {
            //movie
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieModel>();
            //episode
            CreateMap<Episode, EpisodeDto>();
            CreateMap<Episode, EpisodeModel>();
            // series
            CreateMap<Series, SeriesDto>();
            CreateMap<SeriesAlias, SeriesAliasDto>();

            CreateMap<Series, SeriesDto>()
                .Ignore(q => q.SeriesAliasDtos);
            // mapper
            CreateMap<MapperModel, ToBeMappedCreateDto>();
            CreateMap<ToBeMapped, MapperModel>();
        }
    }
}