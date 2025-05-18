using System.Threading.Tasks;
using TraktNet.Objects.Get.Calendars;
using TraktNet.Objects.Get.Collections;
using TraktNet.Objects.Get.History;
using TraktNet.Objects.Get.Shows;
using TraktNet.Objects.Get.Episodes;
using TraktNet.Objects.Get.Movies;
using Microsoft.Extensions.Logging;
using TraktNet;
using System;
using System.Collections.Generic;
using System.Linq;
using MediaInAction.TraktService.Lib.Config;
using MediaInAction.TraktService.Lib.TraktEpisodeNs;
using MediaInAction.TraktService.Lib.TraktMovieNs;
using MediaInAction.TraktService.Lib.TraktShowNs;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using TraktNet.Enums;
using TraktNet.Objects.Authentication;
using TraktNet.Parameters;

namespace MediaInAction.TraktService.Lib
{
    public class TraktService : ITraktService
    {
        private TraktClient _traktClient;
        private readonly ITraktShowLibService _showService;
        private readonly ITraktMovieLibService _movieService;
        private readonly ITraktEpisodeLibService _episodeService;
        private readonly ILogger<TraktService> _logger;
        private readonly ServicesConfiguration _traktConfig;
        
        public TraktService(
            ITraktShowLibService showService,
            ITraktMovieLibService movieService,
            ITraktEpisodeLibService episodeService,
            ServicesConfiguration traktConfig,
            ILogger<TraktService> logger)
        {
            _showService = showService;
            _movieService = movieService;
            _episodeService = episodeService;
            _logger = logger;
            _traktConfig = traktConfig;
            _traktClient = new TraktClient( _traktConfig.ClientId, _traktConfig.ClientSecret);
            _traktClient.Authorization = TraktAuthorization.CreateWith(_traktConfig.AccessToken, _traktConfig.RefreshToken);
        }
        
        private TraktClient GetClient()
        {
            // setup Trakt Client
            var traktToken = _traktConfig.AccessToken;
            var traktRefreshToken = _traktConfig.RefreshToken;
            var traktClientId = _traktConfig.ClientId;
            var traktClientSecret = _traktConfig.ClientSecret;

            _traktClient = new TraktClient( traktClientId, traktClientSecret);
            _traktClient.Authorization = TraktAuthorization.CreateWith(traktToken, traktRefreshToken);
            return _traktClient;
        }

        public async Task GetWatchedShows()
        {
            _logger.LogInformation("GetWatchedShows Service Started");
            var historyItemType = new TraktSyncItemType();
            historyItemType = TraktSyncItemType.Show;
            var startAt = new DateTime();
            var endAt = new DateTime(); 
            var extendedInfo = new TraktExtendedInfo();
            extendedInfo.Full = true;
            startAt = DateTime.UtcNow.AddMonths(-2);
            endAt = DateTime.UtcNow;

            var pagedParameters = new TraktPagedParameters(1,600);
            
            var result = await _traktClient.Sync.GetWatchedHistoryAsync(historyItemType, 
                null, startAt,endAt, extendedInfo, pagedParameters);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Watched Shows Pull count from Trakt:" + result.Value.Count().ToString());
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Episode != null)
                    {
                        await AddWatchedHistoryShow(traktItem);
                    }
                }
            }
        }

        public async Task GetWatchedMovies()
        {
            var historyItemType = new TraktSyncItemType();
            historyItemType = TraktSyncItemType.Movie;
            var startAt = new DateTime();
            var endAt = new DateTime(); 
            var extendedInfo = new TraktExtendedInfo();
            extendedInfo.Full = true;
            startAt = DateTime.UtcNow.AddMonths(-3);
            endAt = DateTime.UtcNow;
            var result = await _traktClient.Sync.GetWatchedHistoryAsync(historyItemType, 
                null, startAt,endAt, extendedInfo);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Collected Watched Movies:" + result.Value.Count().ToString());
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Movie != null)
                    {
                        await AddWatchedHistoryShow(traktItem);
                    }
                }
            }
        }

        public async Task GetMovieCollection()
        {
            _logger.LogInformation("GetMovieCollection Service Start");
            var result = await _traktClient.Sync.GetCollectionMoviesAsync();   

            if (result.IsSuccess)
            { 
                _logger.LogInformation("Movies Collected:" + result.Value.ToString());
                foreach (var traktCollectionMovie in result.Value)
                {
                    await AddCollection(traktCollectionMovie);
                }
            }
            else 
            {
                _logger.LogError("Unsuccessful response from Trakt");
            }
        }

        public async Task GetShowCollection()
        {
            _logger.LogInformation("GetShowCollection started");
            try
            {
                var result = await _traktClient.Sync.GetCollectionShowsAsync();   
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Collected from trakt:" + result.Value.Count().ToString());
                    foreach (var traktShow in result.Value)
                    {
                        await AddShowCollection(traktShow);
                    }
                    _logger.LogInformation("GetShowCollection finished");
                }
                else 
                {
                    _logger.LogDebug("Unsuccessful response from Trakt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("GetShowCollection Failed:" + ex.Message);
            }
        }

        public async Task SyncCalendarAsync()
        {
            var myDate = DateTime.Now.AddDays(-7);

            try
            {
                var result = await _traktClient.Calendar.GetUserShowsAsync(myDate, 14);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Calendar Items Found:" + result.Value.Count().ToString());
                    foreach (var traktCalendarShow in result.Value)
                    {
                        await this.AddCalendar(traktCalendarShow);
                    }
                }
                else
                {
                    _logger.LogError("Unsuccessful response from Trakt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("SyncCalendarAsync Failed:" + ex.Message);
            }
        }
        
        public async Task GetWatchedList(string type)
        {
            _logger.LogInformation("GetWatchedList for "+ type + " Service Started");
            var historyItemType = new TraktSyncItemType();
            if (type == "Shows")
            {
                historyItemType = TraktSyncItemType.Show;
            }
            else
            {
                historyItemType = TraktSyncItemType.Movie;
            }
          
            var startAt = new DateTime();
            var endAt = new DateTime(); 
            var extendedInfo = new TraktExtendedInfo();
            extendedInfo.Full = true;
            startAt = DateTime.UtcNow.AddMonths(-3);
            endAt = DateTime.UtcNow;

            var pagedParameters = new TraktPagedParameters(1,500);
            
            var result = await _traktClient.Sync.GetWatchlistAsync(historyItemType,
                null, extendedInfo, pagedParameters);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Shows Collected:" + result.Value.ToString());
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Movie != null)
                    {
                        await AddWatchedListMovie(traktItem.Movie);
                    }
                    if (traktItem.Show != null)
                    {
                        await AddWatchedListShow(traktItem.Show);
                    }
                }
            }
        }
        
        public async Task GetLastActivities()
        {
            var pagedParameters = new TraktPagedParameters(1,500);

            var result = await _traktClient.Sync.GetLastActivitiesAsync();
            
            if (result.IsSuccess)
            {
                /*
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Movie != null)
                    {
                        await AddWatchedListMovie(traktItem.Movie);
                    }
                    if (traktItem.Show != null)
                    {
                        await AddWatchedListShow(traktItem.Show);
                    }
                }
                */
            }
        }

        private async Task AddShowCollection(ITraktCollectionShow traktShow)
        {
            try {
                var show = ParseCollectionShow(traktShow);
                await _showService.UpdateAddFromDto(show);
            }
            catch 
            {
                _logger.LogError("ERROR: AddShowCollection");
            }
        }

        private async Task AddCalendar(ITraktCalendarShow traktCalendarShow)
        {
            var calendarShow = ParseCalendarShow(traktCalendarShow);
            await _showService.UpdateAddFromDto(calendarShow);
        }

        private async Task AddCollection(ITraktCollectionMovie traktMovie)
        {
            try
            {
                var movie = ParseCollectionMovie(traktMovie);
                if (movie != null)
                {                
                    await _movieService.UpdateAddFromDto(movie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("AddCollection"+ex.Message);
            }
        }

      
        private async Task AddWatchedListMovie(ITraktMovie traktMovie)
        {
            try
            {
                var traktMovieCreateDto = ParseTraktMovie(traktMovie);
                await _movieService.UpdateAddFromDto(traktMovieCreateDto);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }

        private TraktMovieCreateDto ParseTraktMovie(ITraktMovie traktMovie)
        {
            var newMovie = new TraktMovieCreateDto
            {
                Name = traktMovie.Title,
                FirstAiredYear = Convert.ToInt32(traktMovie.Year)
            };

            if (newMovie.TraktMovieCreateAliases == null)
            {
                newMovie.TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>();
            }
            if (traktMovie.Ids.HasAnyId == true)
            {
                if (traktMovie.Ids.Trakt > 0)
                {
                    var movieAlias = new TraktMovieAliasCreateDto()
                    {
                        IdType = "trakt",
                        IdValue = traktMovie.Ids.Trakt.ToString()
                    };
                    newMovie.TraktMovieCreateAliases.Add(movieAlias);
                }
                if (traktMovie.Ids.Imdb.Length > 0)
                {
                    var movieAlias = new TraktMovieAliasCreateDto
                    {
                        IdType = "imdb",
                        IdValue = traktMovie.Ids.Imdb
                    };
                    newMovie.TraktMovieCreateAliases.Add(movieAlias);
                }
                if (traktMovie.Ids.Slug.Length > 0)
                {
                    var movieAlias = new TraktMovieAliasCreateDto
                    {
                        IdType = "slug",
                        IdValue = traktMovie.Ids.Slug
                    };
                    newMovie.TraktMovieCreateAliases.Add(movieAlias);
                    newMovie.Slug = movieAlias.IdValue;
                }
                if (traktMovie.Ids.Tmdb > 0)
                {
                    var movieAlias = new TraktMovieAliasCreateDto
                    {
                        IdType = "tmdb",
                        IdValue = traktMovie.Ids.Tmdb.ToString()
                    };
                    
                    newMovie.TraktMovieCreateAliases.Add(movieAlias);
                }

                return newMovie;
            }
            return null;
        }

        private async Task AddWatchedListShow(ITraktShow traktShow)
        {
            try
            {
                var show = ParseTraktShow(traktShow);
                await _showService.UpdateAddFromDto(show);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("AddWatchedListShow" + ex.Message);
            }
        }

        private CollectionShowDto AddShowAlias(CollectionShowDto show, string watchedList)
        {
            var found = false;
            foreach (var showAlias in show.CollectionShowAliasDtos)
            {
                if ((showAlias.IdType == "List") && (showAlias.IdValue == watchedList))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                var showAlias = new CollectionShowAliasDto
                {
                    IdType = "List",
                    IdValue = watchedList
                };

                show.CollectionShowAliasDtos.Add(showAlias);
            }
            return show;
        }
        
        private async Task AddWatchedHistoryShow(ITraktHistoryItem traktHistoryItem)
        {
            if (traktHistoryItem.Episode != null)
            {
                var collectionShow = ParseTraktShow(traktHistoryItem.Show);
                var episodeDto = ParseTraktEpisode(traktHistoryItem.Episode);
                episodeDto.Slug = collectionShow.Slug;
               // ParseWatchHistory(collectionShow,traktHistoryItem);
                await _showService.UpdateAddFromDto(collectionShow);
                await _episodeService.UpdateAddFromDto(episodeDto);
            }
            if (traktHistoryItem.Movie != null)
            {
                var collectionMovie = ParseTraktMovie(traktHistoryItem.Movie);
                await _movieService.UpdateAddFromDto(collectionMovie);
            }
        }

        private void ParseWatchHistory(CollectionShowDto show, ITraktHistoryItem traktHistoryItem)
        {
            //_logger.LogInformation("ParseWatchHistory");
            if (traktHistoryItem.Action.DisplayName == "Watch")
            {
                foreach (var episodeDto in show.CollectionEpisodeDtos)
                {
                    var traktEpisodeAlias = new CollectionEpisodeAliasDto();
                    traktEpisodeAlias.IdType = "watched";
                    traktEpisodeAlias.IdValue = traktHistoryItem.WatchedAt.ToString();
                    episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                }
            }
        }

        private TraktEpisodeCreateDto ParseTraktEpisode(ITraktEpisode traktEpisode)
        {
           // _logger.LogInformation("ParseTraktEpisode");
            var episodeDto = new TraktEpisodeCreateDto();
            
            if (traktEpisode.Number != null)
            {
                if (traktEpisode.SeasonNumber != null)
                {
                    episodeDto.EpisodeName = "";
                    if (!traktEpisode.Title.IsNullOrEmpty())
                    {
                        episodeDto.EpisodeName = traktEpisode.Title;
                    }
                    episodeDto.AiredDate = Convert.ToDateTime("1999-01-01");

                    if (traktEpisode.FirstAired > episodeDto.AiredDate)
                    {
                        episodeDto.AiredDate = Convert.ToDateTime(traktEpisode.FirstAired);
                    }
                    episodeDto.SeasonNum = (int) traktEpisode.SeasonNumber;
                    episodeDto.EpisodeNum = (int) traktEpisode.Number;
                    if (episodeDto.TraktEpisodeCreateAliases == null)
                    {
                        episodeDto.TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>();
                    }
                          
                    if (traktEpisode.Ids.HasAnyId == true)
                    {
                        if (traktEpisode.Ids.Trakt > 0)
                        {
                            var traktEpisodeAlias = new TraktEpisodeAliasCreateDto()
                            {
                                IdType = "trakt",
                                IdValue = traktEpisode.Ids.Trakt.ToString()
                            };
                            episodeDto.TraktEpisodeCreateAliases.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.Imdb != null)
                        {
                            var traktEpisodeAlias = new TraktEpisodeAliasCreateDto
                            {
                                IdType = "imdb",
                                IdValue = traktEpisode.Ids.Imdb.ToString()
                            };
                            episodeDto.TraktEpisodeCreateAliases.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.Tvdb > 0)
                        {
                            var traktEpisodeAlias = new TraktEpisodeAliasCreateDto
                            {
                                IdType = "tvdb",
                                IdValue = traktEpisode.Ids.Tvdb.ToString()
                            };
                            episodeDto.TraktEpisodeCreateAliases.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.Tmdb > 0)
                        {
                            var traktEpisodeAlias = new TraktEpisodeAliasCreateDto
                            {
                                IdType = "tmdb",
                                IdValue = traktEpisode.Ids.Tmdb.ToString()
                            };
                            episodeDto.TraktEpisodeCreateAliases.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.TvRage > 0)
                        {
                            var traktEpisodeAlias = new TraktEpisodeAliasCreateDto
                            {
                                IdType = "TvRage",
                                IdValue = traktEpisode.Ids.TvRage.ToString()
                            };
                            episodeDto.TraktEpisodeCreateAliases.Add(traktEpisodeAlias);
                        }
                    }

                    if (traktEpisode.UpdatedAt > DateTime.MinValue )
                    {
                        var traktEpisodeAlias = new TraktEpisodeAliasCreateDto
                        {
                            IdType = "TraktUpdateAt",
                            IdValue = traktEpisode.UpdatedAt.ToString()
                        };
                        episodeDto.TraktEpisodeCreateAliases.Add(traktEpisodeAlias);
                    }
                    return episodeDto;
                }
            }
            _logger.LogInformation("Finish ParseTraktEpisode");
            return episodeDto;
        }

        //private
        private  TraktShowCreateDto ParseTraktShow(ITraktShow traktShow)
        {
            //    _logger.LogInformation("ParseTraktShow");
            if (traktShow.Year != null)
            {
                var show = new TraktShowCreateDto()
                {
                    Name = traktShow.Title,
                    FirstAiredYear = (int)traktShow.Year
                };
            if (show.TraktShowCreateAliases == null)
            {
                show.TraktShowCreateAliases = new List<TraktShowAliasCreateDto>();
            }

            if (traktShow.Ids.HasAnyId == true)
            {
                if (traktShow.Ids.Trakt > 0)
                {
                    var seriesAlias = new TraktShowAliasCreateDto()
                    {
                        IdType = "trakt",
                        IdValue = traktShow.Ids.Trakt.ToString()
                    };
                    show.TraktShowCreateAliases.Add(seriesAlias);
                }
                if (traktShow.Ids.Imdb.Length > 0)
                {
                    var seriesAlias = new TraktShowAliasCreateDto
                    {
                        IdType = "imdb",
                        IdValue = traktShow.Ids.Imdb.ToString()
                    };
                    show.TraktShowCreateAliases.Add(seriesAlias);
                }
                if (traktShow.Ids.Slug.Length > 0)
                {
                    var seriesAlias = new TraktShowAliasCreateDto
                    {
                        IdType = "slug",
                        IdValue = traktShow.Ids.Slug
                    };
                    show.TraktShowCreateAliases.Add(seriesAlias);
                    show.Slug = seriesAlias.IdValue;
                }
                if (traktShow.Ids.Tvdb > 0)
                {
                    var seriesAlias = new TraktShowAliasCreateDto
                    {
                        IdType = "tvdb",
                        IdValue = traktShow.Ids.Tvdb.ToString()
                    };
                    show.TraktShowCreateAliases.Add(seriesAlias);
                }
                if (traktShow.Ids.Tmdb > 0)
                {
                    var seriesAlias = new TraktShowAliasCreateDto
                    {
                        IdType = "tmdb",
                        IdValue = traktShow.Ids.Tmdb.ToString()
                    };
                    show.TraktShowCreateAliases.Add(seriesAlias);
                }
                if (traktShow.Ids.TvRage > 0)
                {
                    var seriesAlias = new TraktShowAliasCreateDto
                    {
                        IdType = "TvRage",
                        IdValue = traktShow.Ids.TvRage.ToString()
                    };
                    show.TraktShowCreateAliases.Add(seriesAlias);
                }
            }
            return show;
            }

            return null;
        }
        
        private TraktShowCreateDto ParseCalendarShow(ITraktCalendarShow traktShow)
        {
            var collectedShow = new TraktShowCreateDto();
            collectedShow.Name = traktShow.Title;
            collectedShow.FirstAiredYear = (int)traktShow.Year;
            collectedShow.TraktShowCreateAliases = new List<TraktShowAliasCreateDto>();
            
            if (traktShow.Ids.HasAnyId == true)
            {
                if (traktShow.Ids.Trakt > 0)
                {
                    var showAlias = new TraktShowAliasCreateDto();
                    showAlias.IdType = "trakt";
                    showAlias.IdValue = traktShow.Ids.Trakt.ToString();
                    collectedShow.TraktShowCreateAliases.Add(showAlias);
                }
                if (traktShow.Ids.Imdb.Length > 0)
                {
                    var showAlias2 = new TraktShowAliasCreateDto();
                    showAlias2.IdType = "imdb";
                    showAlias2.IdValue = traktShow.Ids.Imdb;
                    collectedShow.TraktShowCreateAliases.Add(showAlias2);
                }
                if (traktShow.Ids.Slug.Length > 0)
                {
                    var showAlias3 = new TraktShowAliasCreateDto();
                    showAlias3.IdType = "slug";
                    showAlias3.IdValue = traktShow.Ids.Slug;
                    collectedShow.Slug = traktShow.Ids.Slug;
                    collectedShow.TraktShowCreateAliases.Add(showAlias3);
                }
                if (traktShow.Ids.Tvdb > 0)
                {
                    var showAlias4 = new TraktShowAliasCreateDto();
                    showAlias4.IdType = "tvdb";
                    showAlias4.IdValue = traktShow.Ids.Tvdb.ToString();
                    collectedShow.TraktShowCreateAliases.Add(showAlias4);
                }
                if (traktShow.Ids.Tmdb > 0)
                {
                    var showAlias5 = new TraktShowAliasCreateDto();
                    showAlias5.IdType = "tmdb";
                    showAlias5.IdValue = traktShow.Ids.Tmdb.ToString();
                    collectedShow.TraktShowCreateAliases.Add(showAlias5);
                }
                if (traktShow.Ids.TvRage > 0)
                {
                    var showAlias6 = new TraktShowAliasCreateDto();
                    showAlias6.IdType = "TvRage";
                    showAlias6.IdValue = traktShow.Ids.TvRage.ToString();
                    collectedShow.TraktShowCreateAliases.Add(showAlias6);
                }
            }

            if (traktShow.Episode.Number != null)
            {
                if (traktShow.Episode.SeasonNumber != null)
                {
                    var episodeName = "";
                    if (!traktShow.Episode.Title.IsNullOrEmpty())
                    {
                        episodeName = traktShow.Episode.Title;
                    }

                    var collectedEpisode = new TraktEpisodeCreateDto();
                    collectedEpisode.SeasonNum = (int)traktShow.Episode.SeasonNumber;
                    collectedEpisode.EpisodeNum = (int) traktShow.Episode.Number;
                    collectedEpisode.EpisodeName = episodeName;
                    collectedEpisode.AiredDate =  (DateTime)traktShow.FirstAiredInCalendar;
                    collectedEpisode.TraktEpisodeCreateAliases = new List<TraktEpisodeAliasCreateDto>();
                    
                    if (traktShow.Episode.Ids.HasAnyId == true)
                    {
                        if (traktShow.Episode.Ids.Trakt > 0)
                        {
                            var episodeAlias = new TraktEpisodeAliasCreateDto
                            {
                                IdType = "trakt",
                                IdValue = traktShow.Episode.Ids.Trakt.ToString()
                            };
                            collectedEpisode.TraktEpisodeCreateAliases.Add(episodeAlias);
                        }

                        if (traktShow.Episode.Ids.Imdb != null)
                        {
                            if (traktShow.Episode.Ids.Imdb.ToString().Length > 0)
                            {
                                var episodeAlias2 = new TraktEpisodeAliasCreateDto()
                                {
                                    IdType = "imdb",
                                    IdValue = traktShow.Episode.Ids.Imdb.ToString()
                                };
                                collectedEpisode.TraktEpisodeCreateAliases.Add(episodeAlias2);
                            }
                        }
                        if (traktShow.Episode.Ids.Tvdb > 0)
                        {
                            if (traktShow.Episode.Ids.Tvdb.ToString().Length > 0)
                            {
                                var episodeAlias3 = new TraktEpisodeAliasCreateDto
                                {
                                    IdType = "tvdb",
                                    IdValue = traktShow.Episode.Ids.Tvdb.ToString()
                                };
                                collectedEpisode.TraktEpisodeCreateAliases.Add(episodeAlias3);
                            }
                        }
                        if (traktShow.Episode.Ids.Tmdb > 0)
                        {
                            if (traktShow.Episode.Ids.Tmdb.ToString().Length > 0)
                            {
                                var episodeAlias4 = new TraktEpisodeAliasCreateDto
                                {
                                    IdType = "tmdb",
                                    IdValue = traktShow.Episode.Ids.Tmdb.ToString()
                                };
                                collectedEpisode.TraktEpisodeCreateAliases.Add(episodeAlias4);
                            }
                        }
                        if (traktShow.Episode.Ids.TvRage > 0)
                        {
                            if (traktShow.Episode.Ids.TvRage.ToString().Length > 0)
                            {
                                var episodeAlias5 = new TraktEpisodeAliasCreateDto
                                {
                                    IdType = "TvRage",
                                    IdValue = traktShow.Episode.Ids.TvRage.ToString()
                                };
                                collectedEpisode.TraktEpisodeCreateAliases.Add(episodeAlias5);
                            }
                        }
                    }
                    if (traktShow.FirstAiredInCalendar != null)
                    {
                        if (Convert.ToDateTime(collectedEpisode.AiredDate)  != traktShow.FirstAiredInCalendar)
                        {
                            collectedEpisode.AiredDate = (DateTime)traktShow.FirstAiredInCalendar;
                        }
                       
                    }
                   //collectedShow.TraktEpisodeCreateAliases.Add(collectedEpisode);
                }
            }
            return collectedShow;
        }

        private static TraktShowCreateDto ParseCollectionShow(ITraktCollectionShow traktShow)
        {
            try 
            {
                var collectionShow = new TraktShowCreateDto();
                collectionShow.Name = traktShow.Title;
                collectionShow.FirstAiredYear = (int)traktShow.Year;
                collectionShow.TraktShowCreateAliases = new List<TraktShowAliasCreateDto>();

                if (traktShow.Ids.HasAnyId == true)
                {
                    if (traktShow.Ids.Trakt > 0)
                    {
                        var seriesAlias = new TraktShowAliasCreateDto()
                        {
                            IdType = "",
                            IdValue = traktShow.Ids.Trakt.ToString()
                        };
                        collectionShow.TraktShowCreateAliases.Add(seriesAlias);
                    }
                    if (traktShow.Ids.Imdb.Length > 0)
                    {
                        var seriesAlias2 = new TraktShowAliasCreateDto
                        {
                            IdType = "imdb",
                            IdValue = traktShow.Ids.Imdb.ToString()
                        };
                        collectionShow.TraktShowCreateAliases.Add(seriesAlias2);
                    }
                    if (traktShow.Ids.Slug.Length > 0)
                    {
                        var seriesAlias3 = new TraktShowAliasCreateDto
                        {
                            IdType = "slug",
                            IdValue = traktShow.Ids.Slug
                        };
                        collectionShow.Slug = traktShow.Ids.Slug;
                        collectionShow.TraktShowCreateAliases.Add(seriesAlias3);
                    }
                    if (traktShow.Ids.Tvdb > 0)
                    {
                        var seriesAlias4 = new TraktShowAliasCreateDto
                        {
                            IdType = "tvdb",
                            IdValue = traktShow.Ids.Tvdb.ToString()
                        };
                        collectionShow.TraktShowCreateAliases.Add(seriesAlias4);
                    }
                    if (traktShow.Ids.Tmdb > 0)
                    {
                        var seriesAlias5 = new TraktShowAliasCreateDto
                        {
                            IdType = "tmdb",
                            IdValue = traktShow.Ids.Tmdb.ToString()
                        };
                        collectionShow.TraktShowCreateAliases.Add(seriesAlias5);
                    }
                    if (traktShow.Ids.TvRage > 0)
                    {
                        var seriesAlias6 = new TraktShowAliasCreateDto
                        {
                            IdType = "TvRage",
                            IdValue = traktShow.Ids.TvRage.ToString()
                        };
                        collectionShow.TraktShowCreateAliases.Add(seriesAlias6);
                    }
                }
                
                if (traktShow.CollectionSeasons.Count() > 0)
                {
                    foreach (var season in  traktShow.CollectionSeasons )
                    {
                        foreach(var episode in season.Episodes)
                        {
                            if (episode.Number != null) 
                            {
                                var newEpisode = new TraktEpisodeCreateDto();
                                newEpisode.SeasonNum = (int) season.Number;
                                newEpisode.EpisodeNum = (int)episode.Number;
                                newEpisode.AiredDate = Convert.ToDateTime("1999-01-01");
                                newEpisode.TraktEpisodeCreateAliases  = new List<TraktEpisodeAliasCreateDto>();
                                
                                var episodeAlias = new TraktEpisodeAliasCreateDto()
                                {
                                    IdType = "collected",
                                    IdValue = traktShow.LastCollectedAt.ToString()
                                };
                                newEpisode.TraktEpisodeCreateAliases.Add(episodeAlias);
                               // collectionShow.TraktShowCreateAliases.Add(newEpisode);
                            }
                        }
                    }
                }
                return collectionShow;
            }
            catch 
            {
                return null;
            }
        }

        private static TraktMovieCreateDto ParseCollectionMovie(ITraktCollectionMovie traktMovie)
        {
            var slug = "";
            if (traktMovie.Year != null)
            {
                var movie = new TraktMovieCreateDto
                {
                    Name = traktMovie.Title,
                    FirstAiredYear = (int)traktMovie.Year,
                    TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>()
                };
                if (traktMovie.Ids.HasAnyId)
                {
                    if (traktMovie.Ids.Trakt > 0)
                    {
                        var movieAlias = new TraktMovieAliasCreateDto()
                        {
                            IdType = "trakt",
                            IdValue = traktMovie.Ids.Trakt.ToString()
                        };

                        movie.TraktMovieCreateAliases.Add(movieAlias);
                    }
                    if (traktMovie.Ids.Imdb.Length > 0)
                    {
                        var movieAlias2 = new TraktMovieAliasCreateDto
                        {
                            IdType = "imdb",
                            IdValue = traktMovie.Ids.Imdb.ToString()
                        };
                        movie.TraktMovieCreateAliases.Add(movieAlias2);
                    }
                    if (traktMovie.Ids.Slug.Length > 0)
                    {
                        slug = traktMovie.Ids.Slug;
                        var movieAlias3 = new TraktMovieAliasCreateDto
                        {
                            IdType = "slug",
                            IdValue = traktMovie.Ids.Slug
                        };
                        movie.TraktMovieCreateAliases.Add(movieAlias3);
                    }
                    if (traktMovie.Ids.Tmdb > 0)
                    {
                        var movieAlias4 = new TraktMovieAliasCreateDto
                        {
                            IdType = "tmdb",
                            IdValue = traktMovie.Ids.Tmdb.ToString()
                        };
                        movie.TraktMovieCreateAliases.Add(movieAlias4);
                    }
                }
                movie.Slug = slug;
                return movie;
            }

            return null;
        }
    }
}
