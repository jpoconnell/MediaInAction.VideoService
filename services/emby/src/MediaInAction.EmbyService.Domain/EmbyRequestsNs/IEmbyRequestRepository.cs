using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyRequestsNs;

public interface IEmbyRequestRepository : IRepository<EmbyRequest2, Guid>
{
    Task<EmbyRequest2> FindByServerNameAsync(string server, string filename, string directory);

    Task<List<EmbyRequest2>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );

    Task<EmbyRequest2> GetByIdentifier(Guid refId);
 //   Task UpdateEmbyRequestStatus(Guid refId, EmbyStatus accepted);
}
