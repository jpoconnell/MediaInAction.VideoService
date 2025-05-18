using System;
using System.Collections.Generic;

namespace MediaInAction.TraktService.TraktShowNs.Dtos
{
    public class TraktShowDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public string Slug { get; set; }
        public string ExternalId { get; set; }
        public List<TraktShowAliasDto> TraktShowAliasDtos { get; set; }
        
        public TraktShowStatus Status { get; set; }
        public TraktShowDto Create(string name, int firstAiredYear)
        {
            this.Name = name;
            this.FirstAiredYear = firstAiredYear;
            this.TraktShowAliasDtos = new List<TraktShowAliasDto>();
            return this;
        }
    }
}