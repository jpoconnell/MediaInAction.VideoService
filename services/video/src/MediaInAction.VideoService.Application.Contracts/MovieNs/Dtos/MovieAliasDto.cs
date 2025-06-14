﻿using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MovieNs.Dtos
{
    public class MovieAliasDto : EntityDto<Guid>
    {
        public Guid MovieId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}

