using System;
using System.Collections.Generic;

namespace MediaInAction.TraktService.TraktMovieNs.Dtos
{
    public class TraktMovieDto 
    {
        public Guid Id { get; set; }
        public string Slug { get;  set; }
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }  
        public TraktMovieStatus MovieStatus { get; set; }
       
        public string ExternalId { get; set; }
        public List<TraktMovieAliasDto> TraktMovieAliasDtos { get; set; }
        
        public TraktMovieDto Create(string name, int year)
        {
            this.Name = name;
            this.FirstAiredYear = year;
            return this;
        }
    }
}