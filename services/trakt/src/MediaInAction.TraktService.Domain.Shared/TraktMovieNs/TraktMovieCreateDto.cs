using System.Collections.Generic;
using JetBrains.Annotations;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieCreateDto 
    {
        public string Slug  { get; set; }
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        public TraktMovieStatus Status { get; set; }
        public List<TraktMovieAliasCreateDto> TraktMovieCreateAliases{ get; set; }
        [CanBeNull] public string ImageName { get; set; }
    }
}