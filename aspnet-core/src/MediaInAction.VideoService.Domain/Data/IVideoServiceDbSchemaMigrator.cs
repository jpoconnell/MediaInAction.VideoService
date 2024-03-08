using System.Threading.Tasks;

namespace MediaInAction.VideoService.Data;

public interface IVideoServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
