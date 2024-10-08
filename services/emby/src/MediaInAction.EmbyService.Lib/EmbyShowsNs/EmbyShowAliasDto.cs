﻿using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyShowsNs
{
    public class EmbyShowAliasDto: EntityDto<Guid>
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public EmbyShowAliasDto()
        {
        }
        
        internal EmbyShowAliasDto(
            [NotNull] string idType, 
            string idValue)
        {
            this.IdType = idType;
            this.IdValue = idValue;
        }
    }
}
