using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;

namespace MediaInAction.FileService.Lib.FileEntriesNs;

public interface IFileEntryLibService 
{
    Task CreateAsync(FileEntryCreateDto fileEntry);
}

