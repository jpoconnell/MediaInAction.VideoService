using AutoMapper;
using MediaInAction.EmbyService.EmbyShowNs;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using MediaInAction.EmbyService.EmbyShowsNs;

namespace MediaInAction.EmbyService
{
    public class EmbyServiceApplicationAutoMapperProfile : Profile
    {
        public EmbyServiceApplicationAutoMapperProfile()
        {
            CreateMap<EmbyShow, EmbyShowDto>();
         //   CreateMap<Product, ProductResponse>();
        }
    }
}
