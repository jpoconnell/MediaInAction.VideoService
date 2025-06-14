﻿using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesAliasEto : EtoBase
{
    public Guid ProductId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
}