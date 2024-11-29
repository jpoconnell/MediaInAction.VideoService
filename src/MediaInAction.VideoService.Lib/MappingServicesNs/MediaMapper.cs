using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.MappingServicesNs;

public class MediaMapper : IMediaMapper
{
    private readonly ILogger<MediaMapper> _logger;
   // private readonly IParserService _parserService;
   // private readonly IEpisodeService _episodeService;
    private readonly IMovieService _movieService;
    private readonly IToBeMappedService _toBeMappedService;

    public MediaMapper(
        ILogger<MediaMapper> logger,
      //  IParserService parserService,
      //  IEpisodeService episodeService,
        IToBeMappedService toBeMappedService,
        IMovieService movieService
        )
    {
        _logger = logger;
      //  _parserService = parserService;
      //  _episodeService = episodeService;
        _toBeMappedService = toBeMappedService;
        _movieService = movieService;
    }

    public async Task Map()
    {
        _logger.LogInformation("Running Mapping Service");
        var returnList = new List<ToBeMappedDto>();
       
        var mappingDtoList = await _toBeMappedService.GetUnMapped(10);
        var cnt = 0;
        if (mappingDtoList == null)
        {
             cnt = 0;
        }

        var startCnt = cnt;
      
        _logger.LogInformation("Total Entries:" + startCnt.ToString());
        
        _logger.LogInformation("FileEntries Not Mapped:" + startCnt.ToString());
        _logger.LogInformation("FileEntries To Be Updated:" + returnList.Count.ToString());
    }
    
    private async Task<bool> MapToEpisodes(ToBeMappedDto toBeMappedDto, ParserDto parser)
    {
        return true;
    }

    private async Task<bool> MapToMovies(ToBeMappedDto fileEntry, ParserDto parser)
    {
        var result = false;
        return result;
    }
}
