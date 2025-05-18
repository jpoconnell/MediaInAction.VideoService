using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMovieNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.EmbyService.EmbyMoviesNs;

public class EmbyMovieLibService : IEmbyMovieLibService
{
    private readonly ILogger<EmbyMovieLibService> _logger;
    private readonly EmbyMovieManager _embyMovieManager;
    private readonly IEmbyMovieRepository _embyMovieRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public EmbyMovieLibService(
        IEmbyMovieRepository  embyMovieRepository,
        EmbyMovieManager embyMovieManager,
        IDistributedEventBus distributedEventBus,
        ILogger<EmbyMovieLibService> logger
    )
    {
        _logger = logger;
        _embyMovieRepository = embyMovieRepository;
        _embyMovieManager = embyMovieManager;
        _distributedEventBus = distributedEventBus;
    }

    public async Task UpdateAddFromDto(EmbyMovieDto episode)
    {
        try 
        { 
            await CreateUpdateFolder(episode);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }
    
    private async Task CreateUpdateFolder(EmbyMovieDto movie)
    {
        try
        {
            var sss = new List<( string idType, string idValue)>();
           // var dbMovie = await _embyMovieRepository.GetByNameAsync(movie.Name);
          //  if (dbMovie == null)
           // {
             //   var createdMovie = await _embyMovieManager.CreateAsync(
               //     movie.Name, movie., sss );
           // }
           
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }
}

