using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;

namespace MediaInAction.VideoService.ParserNs;

public interface IParserService
{
    Task<MediaType> GetMediaType(ParserDto input);
    Task<ParserDto> MapProcess(ToBeMappedDto toBeMappedDto);
}