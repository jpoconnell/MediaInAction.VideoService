using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieCreatedEto : EtoBase
    {
        public string Slug  { get; set; }
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        public FileStatus MovieStatus { get; set; }
        public bool IsActive { get; set; }
        public List<TraktMovieAliasCreateDto> TraktMovieCreateAliases{ get; set; }
    }
}