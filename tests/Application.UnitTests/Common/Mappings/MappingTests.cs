using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using VideoService2.Application.Common.Interfaces;
using VideoService2.Application.Common.Models;
using VideoService2.Application.Series.Queries.GetSeriesListWithPagination;
using VideoService2.Domain.Entities;
using NUnit.Framework;
using VideoService2.Application.Series.Queries.GetSeriesList;

namespace VideoService2.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Series), typeof(TodoListDto))]
    [TestCase(typeof(Series), typeof(TodoItemDto))]
    [TestCase(typeof(Series), typeof(LookupDto))]
    [TestCase(typeof(Series), typeof(LookupDto))]
    [TestCase(typeof(Series), typeof(SeriesBriefDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
