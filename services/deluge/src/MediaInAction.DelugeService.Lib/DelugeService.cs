using DelugeRPCClient.Net;
using Microsoft.Extensions.Logging;

namespace MediaInAction.DelugeService.Bg;

public class DelugeService : IDelugeService
{
    private DelugeClient _delugeClient;
    private readonly ILogger<DelugeService> _logger;
    private readonly DelugeServicesConfiguration _delugeConfig;
    
    public DelugeService(
        ILogger<DelugeService> logger,
        DelugeServicesConfiguration delugeConfig
        )
    {
        _logger = logger;
        _delugeConfig = delugeConfig;
        _delugeClient = new DelugeClient( _delugeConfig.ServerUrl, _delugeConfig.Password);
    }

    public DelugeClient GetClient()
    {
        // setup Deluge Client
        var delugeUrl = _delugeConfig.ServerUrl;
        var delugePassword = _delugeConfig.Password;
        _delugeClient = new DelugeClient( _delugeConfig.ServerUrl, _delugeConfig.Password);
        return _delugeClient;
    }
}
