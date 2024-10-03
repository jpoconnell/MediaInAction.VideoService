using AutoMapper;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;

namespace MediaInAction.TraktService.Lib
{
    public class TraktServiceLibAutoMapperProfile : Profile
    {
        public TraktServiceLibAutoMapperProfile()
        {
            CreateMap<TraktMovie, TraktMovieDto>();
            CreateMap<TraktShow, TraktShowDto>();
            CreateMap<TraktEpisode, TraktEpisodeDto>();
        }
    }
}
