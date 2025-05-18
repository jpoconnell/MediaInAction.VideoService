using Xunit;

namespace MediaInAction.TraktService.MongoDB;

[CollectionDefinition(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktServiceMongoCollection : TraktServiceMongoDbCollectionFixtureBase
{

}