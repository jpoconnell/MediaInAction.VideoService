using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MovieNs.Dtos
{
    public class MovieDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public MovieStatus MovieStatus { get; set; }
        public bool IsActive { get; set; }
        public List<MovieAliasDto> MovieAliasDtos { get; set; }
        
        public string ImageName { get; set; }
    }
}