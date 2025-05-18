using AutoMapper;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using MediaInAction.TraktService.TraktShowNs;
using MediaInAction.TraktService.TraktShowNs.Dtos;

namespace MediaInAction.TraktService
{
    public class TraktServiceApplicationAutoMapperProfile : Profile
    {
        public TraktServiceApplicationAutoMapperProfile()
        {
          //  CreateMap<Product, ProductDto>();
           // CreateMap<TraktShowDto, SeriesModel>();
            CreateMap<TraktShow, TraktShowDto>();
            CreateMap<TraktMovie, TraktMovieDto>();
        }
    }
}
