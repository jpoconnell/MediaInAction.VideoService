﻿using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.Lib.TraktShowNs.Dtos;

public class CollectionEpisodeAliasDto :  EntityDto<Guid>
{
    public string IdType { get;  set; }
    public string IdValue { get;  set; }

    public CollectionEpisodeAliasDto ()
    {

    }
}

