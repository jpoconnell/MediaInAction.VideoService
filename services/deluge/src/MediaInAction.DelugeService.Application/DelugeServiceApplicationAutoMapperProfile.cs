using AutoMapper;
using MediaInAction.DelugeService.DelugeTorrentNs;
using MediaInAction.DelugeService.DelugeTorrentsNs;


namespace MediaInAction.DelugeService
{
    public class DelugeServiceApplicationAutoMapperProfile : Profile
    {
        public DelugeServiceApplicationAutoMapperProfile()
        {
            CreateMap<DelugeTorrent, DelugeTorrentDto>();
            //CreateMap<Torrent, ProductResponse>();
        }
    }
}
