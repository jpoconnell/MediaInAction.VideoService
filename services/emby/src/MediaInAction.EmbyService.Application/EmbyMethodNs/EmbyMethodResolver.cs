using System;
using System.Collections.Generic;
using System.Linq;
using MediaInAction.EmbyService.EmbyMethodNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService.EmbyMethodNs;

public class EmbyMethodResolver : ITransientDependency
{
    private readonly IEnumerable<IEmbyMethod> _fileMethods;
    private readonly ILogger<EmbyMethodResolver> _logger;

    public EmbyMethodResolver(IEnumerable<IEmbyMethod> fileMethods, ILogger<EmbyMethodResolver> logger)
    {
        _fileMethods = fileMethods;
        _logger = logger;
    }

    public IEmbyMethod Resolve(string traktMethodName)
    {
        var traktMethod = _fileMethods.FirstOrDefault(q => q.Name.Equals(traktMethodName, StringComparison.InvariantCultureIgnoreCase));
        if (traktMethod == null)
        {
            _logger.LogError($"Couldn't find Trakt method with type:{traktMethodName}");
            throw new ArgumentException("Trakt method not found", traktMethodName);
        }

        return traktMethod;
    }
}