using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace  MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowAliasDto: EntityDto<Guid>
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktShowAliasDto()
        {
        }
        
        internal TraktShowAliasDto(
            [NotNull] string idType, 
            string idValue)
        {
          
            this.IdType = idType;
            this.IdValue = idValue;
        }
    }
}
