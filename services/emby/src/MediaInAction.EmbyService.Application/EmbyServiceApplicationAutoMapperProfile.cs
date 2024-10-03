using AutoMapper;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using MediaInAction.EmbyService.EmbyShowsNs;
using MediaInAction.EmbyService.Products;

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
