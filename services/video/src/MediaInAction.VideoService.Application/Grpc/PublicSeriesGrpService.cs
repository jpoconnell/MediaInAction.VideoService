using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.Extensions.Logging;
using Seriesgrpc;
using VideoService.Series.GrpcServer;


namespace MediaInAction.VideoService.Grpc
{
    /// <summary>
    /// Implementation of series RPC service
    /// </summary>
    public class PublicSeriesGrpService : SeriesGrpcService.SeriesGrpcServiceBase
    {
        private readonly ILogger<PublicSeriesGrpService> _logger;
        private readonly ISeriesRepository _seriesRepository;
        private readonly SeriesManager _seriesManager;
        
        public PublicSeriesGrpService(ILogger<PublicSeriesGrpService> logger, 
            ISeriesRepository seriesRepository,
            ISeriesAliasRepository seriesAliasRepository,
            SeriesManager seriesManager)
        {
            _logger = logger;
            _seriesRepository = seriesRepository;
            _seriesManager = seriesManager;
        }

        public override async Task<SeriesModel> CreateUpdateSeries(SeriesModel request, ServerCallContext context)
        {
            var seriesCreateDto = TranslateSeriesGrpc(request);
            var response = await _seriesManager.CreateUpdateAsync(seriesCreateDto);
            if (response != null)
            {
                var seriesModel = TranslateSeries(response);
                return seriesModel;
            }
            else
            {
                return null;
            }
        }

        public override async Task<SeriesModel> SearchOneSeries(SearchRequest request, ServerCallContext context)
        {
            var dbSeries = await _seriesRepository.FindBySeriesNameYear(request.Name, request.Year);
            if (dbSeries != null)
            {
                var seriesModel = TranslateSeries(dbSeries);
                return seriesModel; 
            }
            return null;
        }
        
        private SeriesAliasModel TranslateSeriesAlias(SeriesAlias seriesAlias)
        {
            var seriesAliasModel = new SeriesAliasModel();
            seriesAliasModel.IdType = seriesAlias.IdType;
            seriesAliasModel.IdValue = seriesAlias.IdValue;
            return seriesAliasModel;
        }
    
        private SeriesModel TranslateSeries(SeriesNs.Series series)
        {
            try
            {
                var slug = "";
                var seriesAliasModels = new RepeatedField<SeriesAliasModel>();

                foreach (var seriesAlias in series.SeriesAliases)
                {
                    var seriesAliasModel = new SeriesAliasModel
                    {
                        IdType = seriesAlias.IdType,
                        IdValue = seriesAlias.IdValue
                    };
                    if (seriesAlias.IdType == "slug")
                    {
                        slug = seriesAlias.IdValue;
                    }
                    seriesAliasModels.Add(seriesAliasModel);
                }

                var seriesModel = new SeriesModel
                {
                    Name = series.Name,
                    Year = series.FirstAiredYear,
                    Slug = slug,
                    SeriesAliases = { seriesAliasModels }
                };
                if (series.ImageName == null)
                {
                    seriesModel.ImageName = "";
                }
                else
                {
                    seriesModel.ImageName = series.ImageName;
                }
                seriesModel.Externalid  = series.Id.ToString();
                return seriesModel;
            }
            catch (Exception )
            {
                return null;
            }
        }

        private SeriesCreateDto TranslateSeriesGrpc(SeriesModel request)
        {
            var seriesCreateDto = new SeriesCreateDto
            {
                Name = request.Name,
                FirstAiredYear = request.Year
            };
            seriesCreateDto.SeriesAliasCreateDtos = new List<SeriesAliasCreateDto>();
            foreach (var seriesAlias in request.SeriesAliases)
            {
                seriesCreateDto.SeriesAliasCreateDtos.Add(new SeriesAliasCreateDto
                {
                    IdType = seriesAlias.IdType,
                    IdValue = seriesAlias.IdValue,
                });
            }
            return seriesCreateDto;
        }
    }
}
