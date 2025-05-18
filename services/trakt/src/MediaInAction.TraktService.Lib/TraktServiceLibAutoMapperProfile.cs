using AutoMapper;
using Episodegrpc;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using MediaInAction.TraktService.TraktShowNs;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using Moviegrpc;
using Seriesgrpc;


namespace MediaInAction.TraktService.Lib
{
    public class TraktServiceLibAutoMapperProfile : Profile
    {
        public TraktServiceLibAutoMapperProfile()
        {
            CreateMap<TraktMovie, TraktMovieDto>();
            CreateMap<TraktMovieCreateDto, MovieModel>();
            
            
            CreateMap<TraktShow, TraktShowDto>();
            CreateMap<TraktShowCreateDto, SeriesModel>();
            CreateMap<TraktShowDto, UpdateTraktShowDto>();
            
            CreateMap<TraktEpisode, TraktEpisodeDto>();
            CreateMap<TraktEpisodeCreateDto, EpisodeModel>();
            
        }
    }
}
