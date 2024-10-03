using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.FileService.BG.FileEntryNs;

public class DashboardDto: EntityDto
{
  //  public List<IsMapped> Mappeds { get; set; }
    public List<FileStatus> FileStatus { get; set; }
}