using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestsNs;

[Serializable]
public class EmbyRequestCreateDto
{
    public string ReferenceId { get; set; }

    [Required]
    public string Server { get; set; }

    [Required]
    public string EmbyName { get; set; }
    
    [Required]
    public int Directory { get; set; }
}
