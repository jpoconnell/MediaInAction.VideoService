﻿using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieCreateDto 
    {
        public string Slug  { get; set; }
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        public FileStatus MovieStatus { get; set; }
        public bool IsActive { get; set; }
        public List<TraktMovieAliasCreateDto> TraktMovieCreateAliases{ get; set; }
    }
}