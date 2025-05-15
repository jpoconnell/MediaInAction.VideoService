using System.Diagnostics.CodeAnalysis;

namespace VideoService2.Domain.Entities;

public class Series : BaseAuditableEntity
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }

    public MediaStatus Status { get; set; }
    public string? ImageName { get; set; }
    public List<SeriesAlias> SeriesAliases { get; private set; }

    public Series(string name,int year, List<SeriesAlias> seriesAliases)
    {
        Name = name;
        FirstAiredYear = year;
        SeriesAliases = seriesAliases;
    }

    internal Series(
        Guid id,
        [NotNull] string name,
        int firstAiredYear,
        [NotNull] MediaType seriesType, List<SeriesAlias> seriesAliases, bool isActive = true,
        string imageName = ""
    )
    {
        Name = name;
        ImageName = imageName;
        FirstAiredYear = firstAiredYear;
        SeriesAliases = new List<SeriesAlias>();
    }
    
    public Series AddSeriesAlias(Guid id, Guid seriesId, string idType, string idValue )
    {
        var existingAliasForSeries = SeriesAliases.SingleOrDefault(o => o.SeriesId == seriesId &&
            o.IdType == idType && 
            o.IdValue == idValue);

        if (existingAliasForSeries != null)
        {

        }
        else
        {
            var seriesAlias = new SeriesAlias( idType, idValue);
            SeriesAliases.Add(seriesAlias);
        }

        return this;
    }
    
}