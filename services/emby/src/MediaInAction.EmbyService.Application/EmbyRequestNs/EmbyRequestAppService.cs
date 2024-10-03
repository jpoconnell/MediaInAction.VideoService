using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMethodNs;
using MediaInAction.EmbyService.EmbyRequestsNs;
using MediaInAction.EmbyService.EmbyRequestsNs.Dtos;

namespace MediaInAction.EmbyService.EmbyRequestNs;

public class EmbyRequestAppService : EmbyServiceAppService, IEmbyRequestAppService
{
    protected IEmbyRequestRepository _embyRequestRepository { get; }
    private readonly EmbyMethodResolver _embyMethodResolver;
    
    public EmbyRequestAppService(
        IEmbyRequestRepository embyRequestRepository,
        EmbyMethodResolver embyMethodResolver)
    {
        _embyRequestRepository = embyRequestRepository;
        _embyMethodResolver = embyMethodResolver;
    }


    public virtual async Task<EmbyRequestDto> CreateAsync(EmbyRequestCreateDto input)
    {
    
        /*
        foreach (var embyRequestItem in input.Embys
                     .Select(s => new EmbyRequestEmby(
                         id: GuidGenerator.Create(),
                         //embyRequestId: embyRequest.Id,
                         server: s.Server,
                         embyname: s.EmbyName,
                         directory: s.Directory)))
        {
            embyRequest.Embys.Add(embyRequestItem);
        }
        */
       // await _embyRequestRepository.InsertAsync(embyRequest);
        //return ObjectMapper.Map<EmbyRequest, EmbyRequestDto>(embyRequest);
        return null;
    }
    
    public virtual async Task<EmbyRequestStartResultDto> StartAsync(string embyType, EmbyRequestStartDto input)
    {
        EmbyRequest2 embyRequest =
            await _embyRequestRepository.GetAsync(input.EmbyRequestId, includeDetails: true);

        var embyService = _embyMethodResolver.Resolve(embyType);
        return await embyService.StartAsync(embyRequest, input);
    }
    
    public virtual async Task<EmbyRequestDto> CompleteAsync(string embyType, EmbyRequestCompleteInputDto input)
    {
        var embyService = _embyMethodResolver.Resolve(embyType);

        var embyRequest = await embyService.CompleteAsync(_embyRequestRepository, input.Token);
        return ObjectMapper.Map<EmbyRequest2, EmbyRequestDto>(embyRequest);
    }

    public virtual async Task<bool> HandleWebhookAsync(string traktType, string payload)
    {
        var embyService = _embyMethodResolver.Resolve(traktType);

        await embyService.HandleWebhookAsync(payload);
        
        return true;
    }
}
