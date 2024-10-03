using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.BG.FileMethodNs;

public class FileMethodResolver : ITransientDependency
{
    private readonly IEnumerable<IFileMethod> _fileMethods;
    private readonly ILogger<FileMethodResolver> _logger;

    public FileMethodResolver(IEnumerable<IFileMethod> fileMethods, ILogger<FileMethodResolver> logger)
    {
        _fileMethods = fileMethods;
        _logger = logger;
    }

    public IFileMethod Resolve(string traktMethodName)
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