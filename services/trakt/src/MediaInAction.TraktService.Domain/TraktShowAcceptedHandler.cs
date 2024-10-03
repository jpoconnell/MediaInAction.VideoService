using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace MediaInAction.TraktService;

public class TraktShowAcceptedHandler : IDistributedEventHandler<TraktShowAcceptedEto>, ITransientDependency
{
    private readonly ITraktShowRepository _traktShowRepository;

    public TraktShowAcceptedHandler(ITraktShowRepository traktShowRepository)
    {
        _traktShowRepository = traktShowRepository;
    }
    
    [UnitOfWork]
    public virtual async Task HandleEventAsync(TraktShowAcceptedEto eventData)
    {
        var traktShow = await _traktShowRepository.FindSlugAsync(eventData.Slug);
        
        if (eventData.TraktShowAliasAcceptedEtos.Count == traktShow.TraktShowAliases.Count())
        {
            traktShow.SetAccepted();
            await _traktShowRepository.UpdateAsync(traktShow);
        } 
    }
}