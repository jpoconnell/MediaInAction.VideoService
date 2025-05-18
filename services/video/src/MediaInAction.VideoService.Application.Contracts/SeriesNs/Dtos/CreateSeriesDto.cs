using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

[Serializable]
public class CreateSeriesDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int FirstAiredYear { get; set; }
    
    [Required]
    public DateTime CreateDate { get; set; }

    public SeriesStatus Status { get; set; }
}
