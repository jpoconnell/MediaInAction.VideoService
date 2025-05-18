using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using Shouldly;
using Volo.Abp.Specifications;
using Xunit;

namespace MediaInAction.VideoService.EntityFrameworkCore.Samples;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * Only test your custom repository methods.
 */
[Collection(VideoServiceTestConsts.CollectionDefinitionName)]
public class ToBeMappedRepositoryTests : VideoServiceEntityFrameworkCoreTestBase
{
    private readonly IToBeMappedRepository _toBeMappedRepository;

    public ToBeMappedRepositoryTests()
    {
        _toBeMappedRepository = GetRequiredService<IToBeMappedRepository>();
    }


    [Fact]
    public async Task ShouldGetToBeMappedList()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            //Act
            var filter = "a:";
            ISpecification<ToBeMappedNs.ToBeMapped> specification = ToBeMappedNs.Specifications.SpecificationFactory.Create(filter);
            var toBeMapped = await _toBeMappedRepository.GetToBeMappedsAsync(specification);

            //Assert
            toBeMapped.ShouldNotBeNull();
            toBeMapped.Count.ShouldBeGreaterThan(1);
        });
    }  
}
