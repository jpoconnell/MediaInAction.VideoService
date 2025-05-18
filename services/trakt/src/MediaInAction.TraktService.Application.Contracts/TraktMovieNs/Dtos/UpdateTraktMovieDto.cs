using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.TraktService.TraktMovieNs.Dtos;

public class UpdateTraktMovieDto
{
    [Required]
    [StringLength(TraktMovieConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public int FirstAiredYear { get; set; }

    public string ExternalId { get; set; }
  
}
