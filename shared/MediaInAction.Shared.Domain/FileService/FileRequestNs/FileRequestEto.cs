using System;
using System.Collections.Generic;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.Shared.Domain.FileService.FileRequestNs;

[Serializable]
public class FileRequestEto
{
    public Guid ReferenceId { get; set; }
    public string Method { get; set; }

    public FileStatus Status { get; set; }
    public List<FileRequestFileEto> Files { get; set; }

}
