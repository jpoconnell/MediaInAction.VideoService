using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace MediaInAction.TraktService.TraktRequests;

public class TraktRequestDomainService : DomainService
{
    private readonly ITraktRequestRepository _traktRequestRepository;

    public TraktRequestDomainService(ITraktRequestRepository traktRequestRepository)
    {
        _traktRequestRepository = traktRequestRepository;
    }

    public async Task<TraktRequest> UpdateTraktRequestStateAsync(
        Guid traktRequestId,
        string orderStatus,
        string orderId)
    {
        var traktRequest = await _traktRequestRepository.GetAsync(traktRequestId);
        

       // traktRequest.ExtraProperties[PayPalConsts.OrderIdPropertyName] = orderId;
        traktRequest.ExtraProperties[nameof(orderStatus)] = orderStatus;

        await _traktRequestRepository.UpdateAsync(traktRequest);

        return traktRequest;
    }
}