using JetBrains.Annotations;

namespace  MediaInAction.TraktService.TraktShowNs.Dtos
{
    public class TraktShowAliasDto
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
