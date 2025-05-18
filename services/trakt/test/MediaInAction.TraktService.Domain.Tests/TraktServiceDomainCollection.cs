using MediaInAction.TraktService.MongoDB;
using Xunit;

namespace MediaInAction.TraktService;

[CollectionDefinition(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktServiceDomainCollection : TraktServiceMongoDbCollectionFixtureBase
{

}