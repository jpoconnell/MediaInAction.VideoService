// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using MediaInAction.VideoService.SeriesNs.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

// ReSharper disable once CheckNamespace
namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class SeriesStatusDto : EntityDto
{
    public int CountOfStatusSeries { get; set; }

    public string IsActive { get; set; }
}
