using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediaInAction.TraktService.TraktShowNs.Dtos;


namespace MediaInAction.TraktService.TraktShowNs;

public interface ITraktShowPublicService
{
    Task CreateShow(TraktShowDto traktShow);
}