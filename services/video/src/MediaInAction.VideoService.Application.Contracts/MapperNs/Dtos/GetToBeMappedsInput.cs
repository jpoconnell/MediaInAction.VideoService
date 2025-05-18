using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MapperNs.Dtos;

public class GetToBeMappedsInput :  PagedAndSortedResultRequestDto
{
    public bool Filter { get; set; }
}