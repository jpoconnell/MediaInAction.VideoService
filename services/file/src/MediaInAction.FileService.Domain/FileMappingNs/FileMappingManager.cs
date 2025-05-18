using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MediaInAction.FileService.FileMappingNs;

public class FileMappingManager : DomainService
{
    private readonly IRepository<FileMapping, Guid> _fileMappingRepository;
    private readonly ILogger<FileMappingManager> _logger;
        
    public FileMappingManager( 
        IRepository<FileMapping, Guid> fileMappingRepository, 
        IFileEntryRepository fileEntryRepository, 
        ILogger<FileMappingManager> logger)
    {
        _fileMappingRepository = fileMappingRepository;
        _logger = logger;
    }

    public async Task<FileMapping> CreateAsync(FileEntry input)
    {
        if (input != null)
        {
            var cleanName =  CleanFileName(input.Filename);
            _logger.LogInformation("cleanAlias" + cleanName);
            var existingFileMapping = await _fileMappingRepository.FirstOrDefaultAsync(
                p => p.SearchString == cleanName);
            if (existingFileMapping == null)
            {
                var newMapping = new FileMapping
                {
                    SearchString = cleanName,
                    IsSent = false
                };
                var myMapping = await _fileMappingRepository.InsertAsync(newMapping);
                _logger.LogInformation("New Mapping Created for " + myMapping.SearchString);
            }
            else
            {
                if (existingFileMapping.FileEntryIds == null)
                {
                    existingFileMapping.FileEntryIds =
                    [
                        input.Id
                    ];
                    await _fileMappingRepository.UpdateAsync(existingFileMapping, true);
                    _logger.LogInformation("Added First ID for " + existingFileMapping.SearchString);
                }
                else
                {
                    var found = false;
                    foreach (var myId in existingFileMapping.FileEntryIds)
                    {
                        if (input.Id == myId)
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        existingFileMapping.FileEntryIds.Add(input.Id);
                        await _fileMappingRepository.UpdateAsync(existingFileMapping, true);
                        _logger.LogInformation("Added Next ID for " + existingFileMapping.SearchString);
                    }
                }
            }
        }
        return null;
    }
    
    private string CleanFileName( string name)
    {
        var myName = "";
        var seseps = "";
        List<string> words = SpiltName(name);
          
        try
        {
            foreach (string word in words)
            {
                string pattern = @"\bs\d+e\d+\b";
                string wordUpper = word.ToUpper();
                Match m = Regex.Match(wordUpper, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    seseps = word.ToUpper();
                    break;
                }
                else
                {
                    myName = myName + word + " ";
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return "";
        }

        return myName.TrimEnd();
    }

    private List<string> SpiltName(string input)
    {
        List<string> outList = new List<string>();
        string[] prases = input.Split('.');
        string[] words;
        foreach (string prase in prases)
        {
            words = prase.Split(' ');
            foreach (string word in words)
            {
                outList.Add(word);
            }
        }
        return outList;
    } 
}