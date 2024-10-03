using AutoMapper;
using MediaInAction.DelugeService.DelugeTorrentNs;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using MediaInAction.DelugeService.TorrentsNs;

namespace MediaInAction.DelugeService
{
    public class DelugeServiceDomainAutoMapperProfile : Profile
    {
        public DelugeServiceDomainAutoMapperProfile()
        {
            CreateMap<DelugeTorrent, DelugeTorrentCreatedEto>();
        }
    }
}
