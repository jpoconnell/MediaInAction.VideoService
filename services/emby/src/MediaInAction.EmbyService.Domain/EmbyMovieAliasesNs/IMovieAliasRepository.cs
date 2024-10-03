using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMovieAliasesNs.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyMovieAliasesNs;

public interface IMovieAliasRepository :  IRepository<EmbyMovieAlias, Guid>
{
    Task<EmbyMovieAlias> FindByTypeValue(string showId);

}