using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowAliasNs;
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
                var traktShowId = GuidGenerator.Create();
                var newTraktShowAliases = new List<TraktShowAlias>();
                foreach (var trakShowCreateAlias in traktShowCreateDto.TraktShowCreateAliases)
                {
                    var newAlias = new TraktShowAlias
                    {
                        IdValue = trakShowCreateAlias.IdValue,
                        IdType = trakShowCreateAlias.IdType
                    };
                    newTraktShowAliases.Add(newAlias);
                }
                
                var newTraktShow = new TraktShow(
                    traktShowId,
                    traktShowCreateDto.Name,
                    traktShowCreateDto.FirstAiredYear,
                    newTraktShowAliases,
                    traktShowCreateDto.Slug
                );
                var returnValue = await _showRepository.InsertAsync(newTraktShow, true);
                return returnValue;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TraktShowManager.CreateAsync");
                return null;
            }
        }

        private async Task<TraktShowDto> TranslateToTraktShowDto(TraktShow existingShow, TraktShowCreateDto traktShowCreateDto)
        {
            var traktShowDto = new TraktShowDto
            {
                TraktStatus = existingShow.TraktStatus,
                Id = existingShow.Id
            };
            var slug = "";
            if (existingShow.Slug.IsNullOrEmpty())
            {
                slug = existingShow.Slug;
                if (slug.IsNullOrEmpty())
                {
                    foreach (var existingShowTraktShowAlias in existingShow.TraktShowAliases)
                    {
                        if (existingShowTraktShowAlias.IdType == "slug")
                        {
                            slug = existingShowTraktShowAlias.IdValue;
                        }
                    }
                }

                if (slug.IsNullOrEmpty())
                {
                    if (!traktShowCreateDto.Slug.IsNullOrEmpty())
                    {
                        slug = traktShowCreateDto.Slug;
                    }
                }
                if (slug.IsNullOrEmpty())
                {
                    foreach (var traktShowCreatedAlias in traktShowCreateDto.TraktShowCreateAliases)
                    {
                        if (traktShowCreatedAlias.IdType == "slug")
                        {
                            slug = traktShowCreatedAlias.IdValue;
                        }
                    }
                }

                traktShowDto.Slug = slug;
            }

            traktShowDto.TraktShowAliasDtos = new List<TraktShowAliasDto>();
            foreach (var existingShowTraktShowAlias in existingShow.TraktShowAliases)
            {
                var myShowAlias = new TraktShowAliasDto();
                myShowAlias.IdValue = existingShowTraktShowAlias.IdValue;
                myShowAlias.IdType = existingShowTraktShowAlias.IdType;
                traktShowDto.TraktShowAliasDtos.Add(myShowAlias);
            }

            foreach (var traktShowCreatedAlias in traktShowCreateDto.TraktShowCreateAliases)
            {
                var found = false;
                foreach (var traktShowAliasDto in traktShowDto.TraktShowAliasDtos )
                {
                    if ((traktShowAliasDto.IdType == traktShowCreatedAlias.IdType) &&
                        (traktShowAliasDto.IdValue == traktShowCreatedAlias.IdValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    var newAlias = new TraktShowAliasDto();
                    newAlias.IdType = traktShowCreatedAlias.IdType;
                    newAlias.IdValue = traktShowCreatedAlias.IdValue;
                    traktShowDto.TraktShowAliasDtos.Add(newAlias);
                }
            }
            return traktShowDto;
        }

        private async Task UpdateAsync(TraktShowDto traktShowDto)
        {
            var existingShow = await _showRepository.GetAsync(traktShowDto.Id);
            if (existingShow != null)
            {
                foreach (var showAliasDto in traktShowDto.TraktShowAliasDtos)
                {
                    var found = false;
                    foreach (var existingAlias in existingShow.TraktShowAliases)
                    {
                        if (showAliasDto.IdType == existingAlias.IdType)
                        {
                            if (showAliasDto.IdValue != existingAlias.IdValue)
                            {
                                found = true;
                            }
                        }
                    }

                    if (found == false)
                    {
                        var traktShowAlias = new TraktShowAlias();
                        /*
                        existingShow.TraktShowAliases.Add(traktShowAlias);
                        var updatedShowDto = new TraktShowDto();
                        updatedShowDto.Id = existingShow.Id;
                        updatedShowDto.Name = existingShow.Name;
                        updatedShowDto.FirstAiredYear = existingShow.FirstAiredYear;
                        updatedShowDto.Slug = existingShow.Slug;
                        foreach (var existShowAlias in existingShow.TraktShowAliases)
                        {
                            var newAlias = new TraktShowAliasDto();
                            newAlias.IdType = existShowAlias.idType;
                            newAlias.IdValue = existShowAlias.idValue;
                            updatedShowDto.TraktShowAliasDtos.Add(newAlias);
                        }
                     
                        await UpdateTraktShowAsync(updatedShowDto);
                        */
                    }
                }
            }
        }
    }
}
