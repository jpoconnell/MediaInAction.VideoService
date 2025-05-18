using MediaInAction.DelugeService.MongoDB;
using Xunit;

namespace MediaInAction.DelugeService;

[CollectionDefinition(DelugeServiceTestConsts.CollectionDefinitionName)]
public class DelugeServiceDomainCollection : DelugeServiceMongoDbCollectionFixtureBase
{

}