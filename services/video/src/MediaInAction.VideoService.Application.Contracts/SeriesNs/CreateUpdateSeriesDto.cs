using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.VideoService.SeriesNs;

[Serializable]
public class CreateUpdateSeriesDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    public int FirstAiredYear { get; set; }

    [Required]
    public SeriesStatus Status { get; set; } = SeriesStatus.Active;
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
}
