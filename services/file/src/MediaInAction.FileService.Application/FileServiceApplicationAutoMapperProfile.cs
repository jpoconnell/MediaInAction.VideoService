using AutoMapper;
using MediaInAction.FileService.FileEntriesNs;

namespace MediaInAction.FileService
{
    public class FileServiceApplicationAutoMapperProfile : Profile
    {
        public FileServiceApplicationAutoMapperProfile()
        {
            CreateMap<FileEntry, FileEntryDto>();

        }
    }
}
