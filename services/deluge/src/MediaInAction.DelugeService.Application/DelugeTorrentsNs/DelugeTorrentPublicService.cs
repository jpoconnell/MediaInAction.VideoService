using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediaInAction.DelugeService.DelugeTorrentNs;
using Microsoft.Extensions.Logging;
using VideoService.Mapper.GrpcServer;
using VideoService.Movie.GrpcServer;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using SearchRequest = VideoService.Mapper.GrpcServer.SearchRequest;

namespace MediaInAction.DelugeService.DelugeTorrentsNs;

public class DelugeTorrentPublicService : IDelugeTorrentPublicService, ITransientDependency
{
    private readonly IDistributedCache<DelugeTorrentNs.Dtos.DelugeTorrentDto, Guid> _cache;
    private readonly ILogger<DelugeTorrentPublicService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly MapperGrpcService.MapperGrpcServiceClient _mapperGrpcServiceClient;
    private readonly IDelugeTorrentRepository _traktMovieRepository;
    private readonly DelugeTorrentManager _traktMovieManager;
    
    public DelugeTorrentPublicService(
        IDistributedCache<DelugeTorrentNs.Dtos.DelugeTorrentDto, Guid> cache,
        ILogger<DelugeTorrentPublicService> logger,
        IDelugeTorrentRepository  traktMovieRepository,
        DelugeTorrentManager traktMovieManager,
        IObjectMapper mapper)
    {
        _cache = cache;
        _logger = logger;
        _mapper = mapper;
        _traktMovieRepository = traktMovieRepository;
        _traktMovieManager = traktMovieManager;
        _mapperGrpcServiceClient = new MapperGrpcService.MapperGrpcServiceClient(Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:8181"));
    }
    
}