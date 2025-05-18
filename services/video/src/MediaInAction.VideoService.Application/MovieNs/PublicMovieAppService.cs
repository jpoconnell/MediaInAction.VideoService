using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.MovieNs
{
    public class PublicMovieAppService : ApplicationService, IPublicMovieAppService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;

        public PublicMovieAppService(IRepository<Movie, Guid> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<ListResultDto<MovieDto>> GetListAsync()
        {
            return new ListResultDto<MovieDto>(
                ObjectMapper.Map<List<Movie>, List<MovieDto>>(
                    await _movieRepository.GetListAsync()
                )
            );
        }
        
        public async Task<MovieDto> GetAsync(Guid id)
        {
            var movie = await _movieRepository.GetAsync(id);
            return ObjectMapper.Map<Movie, MovieDto>(movie);
        }
    }
}
