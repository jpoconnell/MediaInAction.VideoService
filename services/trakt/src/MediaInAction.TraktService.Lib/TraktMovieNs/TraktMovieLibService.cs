using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using Microsoft.Extensions.Logging;
using Moviegrpc;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.Lib.TraktMovieNs
{
    public class TraktMovieLibService : ITraktMovieLibService
    {
        private readonly ILogger<TraktMovieLibService> _logger;
        private readonly ITraktMoviePublicService _traktMoviePublicService;
        private readonly IObjectMapper _mapper;
        
        public TraktMovieLibService(
            ITraktMoviePublicService traktMoviePublicService,
            IObjectMapper mapper,
            ILogger<TraktMovieLibService> logger)
        {
            _mapper = mapper;
            _traktMoviePublicService = traktMoviePublicService;
            _logger = logger;
        }

        public async Task UpdateAddFromDto(TraktMovieCreateDto traktMovieCreateDto)
        {
            _logger.LogInformation("MovieLibService.UpdateAddFromDto:" + traktMovieCreateDto.Name);
            try 
            { 
                await CreateUpdateMovie(traktMovieCreateDto);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("MovieLibService.UpdateAddFromDto:" + ex.Message);
            }
        }
        
        public async Task<List<TraktMovieDto>> GetListAsync()
        {
            var traktMovieListDto = new List<TraktMovieDto>();
            var traktMovieList = await _traktMoviePublicService.GetListAsync();
            foreach (var traktMovie in traktMovieList)
            {
                var traktMovieDto = new TraktMovieDto
                {
                    FirstAiredYear = traktMovie.FirstAiredYear,
                    Name = traktMovie.Name
                };
                traktMovieListDto.Add(traktMovieDto);
            }

            return traktMovieListDto;
        }

        public async Task ResendUnAcceptedMoviesList()
        {
            var movieList = await _traktMoviePublicService.GetListAsync();
            foreach (var traktMovie in movieList)
            {
            
            }
        }
        
        private async Task CreateUpdateMovie(TraktMovieCreateDto traktMovieCreateDto)
        {
            try
            {
                // see if it exists in local db
                var dbMovie = await _traktMoviePublicService.GetUniqueMovie(traktMovieCreateDto);

                if (dbMovie == null)
                {
                    // save to database
                    var returnedMovie = await _traktMoviePublicService.CreateAsync(traktMovieCreateDto);

                    if (returnedMovie != null)
                    {
                        _logger.LogInformation("Trakt Movie created:" + traktMovieCreateDto.Name + ":" +
                                               traktMovieCreateDto.FirstAiredYear.ToString());
                    }
                    // send to video service
                    await CreateGrpcMovie(traktMovieCreateDto);
                    Console.WriteLine($"Trakt Movie created:{traktMovieCreateDto.Name}");
                }
                else
                {
                     var movieModel = await CreateGrpcMovie(traktMovieCreateDto);
                     
                }
            }
            catch
            {
                return;
            }
        }
        
        private async Task<MovieModel> SearchGrpcMovie(TraktMovieCreateDto traktMovieCreateDto)
        {
            try
            {
                var movieAliasModels = new RepeatedField<MovieAliasModel>();
                var movieSearchRequest = new SearchRequest();
                movieSearchRequest.Id = "";
                movieSearchRequest.Slug = traktMovieCreateDto.Slug;
                movieSearchRequest.Name = traktMovieCreateDto.Name;
                movieSearchRequest.Year = traktMovieCreateDto.FirstAiredYear;
                movieSearchRequest.AliasValue = "";

                var serverUrl = "https://localhost:44356";
                using var channel = GrpcChannel.ForAddress(serverUrl);
                var client = new MovieGrpcService.MovieGrpcServiceClient(channel);
                var reply = await client.SearchOneMovieAsync(movieSearchRequest);

                if (reply.Externalid != null)
                {
                    var traktMovieDto = await _traktMoviePublicService.GetUniqueMovie(traktMovieCreateDto);
                    traktMovieDto.ExternalId = reply.Externalid;
                    var traktMovieCreateDto2 = _mapper.Map<TraktMovieDto, UpdateTraktMovieDto>(traktMovieDto);
                    await _traktMoviePublicService.UpdateAsync(traktMovieDto.Id, traktMovieCreateDto2);
                }

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("MovieLibService.SearchGrpcMovie:" + ex.Message);
                return null;
            }
        }

        private async Task<MovieModel> CreateGrpcMovie(TraktMovieCreateDto traktMovieCreateDto)
        {
            var movieAliasModels = new RepeatedField<MovieAliasModel>();
            var movieModel = new MovieModel();
            movieModel.Name = traktMovieCreateDto.Name;
            movieModel.Year = traktMovieCreateDto.FirstAiredYear;
            movieModel.Slug = traktMovieCreateDto.Slug;
            if (traktMovieCreateDto.ImageName != null)
            {
                movieModel.ImageName = traktMovieCreateDto.ImageName;
            }
            else
            {
                movieModel.ImageName = "";
            }
            foreach (var traktMovieAlias in traktMovieCreateDto.TraktMovieCreateAliases)
            {
                if (!traktMovieAlias.IdType.IsNullOrEmpty())
                {
                    var myAlias = new MovieAliasModel
                    {
                        IdType = traktMovieAlias.IdType,
                        IdValue = traktMovieAlias.IdValue
                    };
                    movieModel.MovieAliases.Add(myAlias); 
                }
            }

            var serverUrl = "https://localhost:44356";
            using var channel = GrpcChannel.ForAddress(serverUrl);
            _logger.LogInformation("Creating new movie:" + traktMovieCreateDto.Name);
            var client = new MovieGrpcService.MovieGrpcServiceClient(channel);
            var reply = await client.CreateUpdateMovieAsync(movieModel );
            _logger.LogInformation("MovieLibService.CreateNewMovie:" + reply.Name);
            return reply;
        }
    }
}
