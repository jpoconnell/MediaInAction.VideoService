using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace  MediaInAction.EmbyService.EmbyMovieNs;

public class EmbyMovieManager : DomainService
{
    private readonly IEmbyMovieRepository _embyMovieRepository;
    private ILogger<EmbyMovieManager> _logger;
    
    public EmbyMovieManager(
        IEmbyMovieRepository embyMovieRepository,
        ILogger<EmbyMovieManager> logger
    )
    {
        _embyMovieRepository = embyMovieRepository;
        _logger = logger;
    }

    public async Task<EmbyMovie> CreateAsync(EmbyMovieCreateDto input)
    {
        var movie = new EmbyMovie()
        {
            Name = input.Name,
            FirstAiredYear = input.FirstAiredYear
        };

        try
        {
            var createdMediaFolder = await _embyMovieRepository.InsertAsync(movie, true);
            return createdMediaFolder;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
    }
}
