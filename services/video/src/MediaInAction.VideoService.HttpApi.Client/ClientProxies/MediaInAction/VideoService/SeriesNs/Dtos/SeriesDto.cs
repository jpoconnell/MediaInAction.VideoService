// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using MediaInAction.Shared.Domain.Enums;
using System;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Application.Dtos;

// ReSharper disable once CheckNamespace
namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class SeriesDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public int FirstAiredYear { get; set; }

    public MediaType Type { get; set; }

    public bool IsActive { get; set; }

    public string ImageName { get; set; }

    public SeriesAliasDto[] SeriesAliasDtos { get; set; }
}
