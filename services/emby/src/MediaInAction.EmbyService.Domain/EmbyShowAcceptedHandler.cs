using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace MediaInAction.EmbyService;

public class EmbyShowAcceptedHandler : IDistributedEventHandler<EmbyShowAcceptedEto>, ITransientDependency
{
    private readonly IEmbyShowRepository _embyShowRepository;

    public EmbyShowAcceptedHandler(IEmbyShowRepository embyShowRepository)
    {
        _embyShowRepository = embyShowRepository;
    }
    
    [UnitOfWork]
    public virtual async Task HandleEventAsync(EmbyShowAcceptedEto eventData)
    {
    }
}