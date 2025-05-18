using System.Threading.Tasks;
using Grpc.Core;
using Mappergrpc;
using MediaInAction.VideoService.ToBeMappedNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.Grpc
{
    /// <summary>
    /// Implementation of series RPC service
    /// </summary>
    public class PublicMapperGrpService : MapperGrpcService.MapperGrpcServiceBase
    {
        private readonly ILogger<PublicMapperGrpService> _logger;
        private readonly IToBeMappedRepository _toBeMappedRepository;
        private readonly ToBeMappedManager _toBeMappedManager;
        
        public PublicMapperGrpService(
            ILogger<PublicMapperGrpService> logger, 
            IToBeMappedRepository toBeMappedRepository,
            ToBeMappedManager toBeMappedManager)
        {
            _logger = logger;
            _toBeMappedRepository = toBeMappedRepository;
            _toBeMappedManager = toBeMappedManager;
        }

        public override async Task<MapperModel> CreateUpdateMapper(MapperModel request, ServerCallContext context)
        {
            var mapperCreateDto = MapToDto(request);
            var response = await _toBeMappedManager.CreateAsync(mapperCreateDto);
            if (response != null)
            {
                var mapperModel = MapToModel(response);
                
                //_objectMapper.Map<ToBeMapped, MapperModel>(response);
                return mapperModel;
            }
            else
            {
                return null;
            }
        }

        private MapperModel MapToModel(ToBeMapped response)
        {
            throw new System.NotImplementedException();
        }

        private ToBeMappedCreateDto MapToDto(MapperModel request)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<MapperModel> SearchOneMapped(SearchRequest request, ServerCallContext context)
        {
            var dbMapper = await _toBeMappedRepository.FindByAlias(request.Alias);
            if (dbMapper != null)
            {
                var seriesModel = MapToModel(dbMapper);
                return seriesModel; 
            }
            return null;
        }
    }
}
