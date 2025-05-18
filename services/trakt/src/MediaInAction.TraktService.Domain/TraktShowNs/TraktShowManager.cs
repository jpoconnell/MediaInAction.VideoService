using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowManager : DomainService
    {
        private readonly ITraktShowRepository _showRepository;
        private ILogger<TraktShowManager> _logger;
        
        public TraktShowManager(
            ITraktShowRepository traktShowRepository,
            ILogger<TraktShowManager> logger
        )
        {
            _showRepository = traktShowRepository;
            _logger = logger;
        }

        public async Task<TraktShow> CreateAsync(TraktShowCreateDto traktShowCreateDto)
        {
            try
            {
                var newTraktShow = new TraktShow (
                    id: GuidGenerator.Create(),
                    traktShowCreateDto.Name,
                    traktShowCreateDto.Slug,
                    traktShowCreateDto.FirstAiredYear,
                    traktShowCreateDto.Status
                );
           
                // Add new order items
                foreach (var trakShowCreateAlias in traktShowCreateDto.TraktShowCreateAliases)
                {
                    newTraktShow.AddTraktShowAlias(
                        id: GuidGenerator.Create(),
                        idType: trakShowCreateAlias.IdType,
                        idValue: trakShowCreateAlias.IdValue
                    );
                }
           
                var returnValue = await _showRepository.InsertAsync(newTraktShow, true);
                return returnValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraktShowManager.CreateAsync");
                return null;
            }
        }
    }
}
