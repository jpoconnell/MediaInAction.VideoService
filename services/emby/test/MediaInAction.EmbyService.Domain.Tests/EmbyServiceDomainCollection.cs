using MediaInAction.EmbyService.MongoDB;
using Xunit;

namespace MediaInAction.EmbyService;

[CollectionDefinition(EmbyServiceTestConsts.CollectionDefinitionName)]
public class EmbyServiceDomainCollection : EmbyServiceMongoDbCollectionFixtureBase
{

}