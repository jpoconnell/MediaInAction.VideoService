using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;

namespace MediaInAction.VideoService.ToBeMappedsNs;

public interface IToBeMappedService
{
    Task CreateToBeMappedASync(string alias);
    Task UpdateAsync(ToBeMappedDto toBeMapped);
    Task<List<ToBeMappedDto>> GetMapped(bool b, int limit);
}