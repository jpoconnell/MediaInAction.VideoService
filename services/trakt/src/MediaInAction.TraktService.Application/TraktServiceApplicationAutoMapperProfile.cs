using AutoMapper;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;

namespace MediaInAction.TraktService
{
    public class TraktServiceApplicationAutoMapperProfile : Profile
    {
        public TraktServiceApplicationAutoMapperProfile()
        {
            CreateMap<TraktMovie, TraktMovieDto>();
            CreateMap<TraktShow, TraktShowDto>();
            CreateMap<TraktEpisode, TraktEpisodeDto>();
        }
    }
}
