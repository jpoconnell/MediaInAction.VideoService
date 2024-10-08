// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

// ReSharper disable once CheckNamespace
namespace MediaInAction.VideoService.EpisodeNs.Dtos;

public class EpisodeDto : EntityDto<Guid>
{
    public Guid SeriesId { get; set; }

    public string SeriesName { get; set; }

    public int SeasonNum { get; set; }

    public int EpisodeNum { get; set; }

    public MediaStatus EpisodeStatus { get; set; }

    public DateTime AiredDate { get; set; }

    public string EpisodeName { get; set; }

    public string AltEpisodeId { get; set; }

    public string SeasonEpisode { get; set; }

    public EpisodeAliasDto[] EpisodeAliasDtos { get; set; }
}
