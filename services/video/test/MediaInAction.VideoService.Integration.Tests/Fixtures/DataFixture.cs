using Bogus;
using MediaInAction.VideoService.SeriesNs;

namespace MediaInAction.VideoService.Integration.Test.Fixtures
{
    internal class DataFixture
    {
        public static List<Series> GetSeries(int count, bool useNewSeed = false)
        {
            return GetSeriesFaker(useNewSeed).Generate(count);
        }
        
        private static Faker<Series> GetSeriesFaker(bool useNewSeed)
        {
            var seed = 0;
            if (useNewSeed)
            {
                seed = Random.Shared.Next(10, int.MaxValue);
            }
            return new Faker<Series>()
            
                .RuleFor(t => t.Name, (faker, t) => faker.Name.JobArea())
                .UseSeed(seed);
        }

    }
}
