using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.VideoService.SeriesNs;

public class UpdateSeriesDto
{
    [Required]

    public string Name { get; set; }

    [Required]
    public int FirstAiredYear { get; set; }
    [Required]
    public SeriesStatus Status { get; set; }
    
    [Required]
    public DateTime CreateDate { get; set; }
}
