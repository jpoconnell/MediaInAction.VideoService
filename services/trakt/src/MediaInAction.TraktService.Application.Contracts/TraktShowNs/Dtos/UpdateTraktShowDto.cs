using System;
using System.ComponentModel.DataAnnotations;


namespace MediaInAction.TraktService.TraktShowNs.Dtos;

public class UpdateTraktShowDto
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(TraktShowConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    public int FirstAiredYear { get; set; }
    public string ExternalId { get; set; }
}
