using System.Collections.Generic;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

    public class SeriesCreateDto
    {
        public string Name { get; set; }
        public string Slug  { get; set; }
        public int FirstAiredYear { get; set; }
        
        public MediaType Type { get; set; }
        
        public bool IsActive { get; set; }
        
        public List<SeriesAliasCreateDto> SeriesAliases { get; set; }
        
    }

