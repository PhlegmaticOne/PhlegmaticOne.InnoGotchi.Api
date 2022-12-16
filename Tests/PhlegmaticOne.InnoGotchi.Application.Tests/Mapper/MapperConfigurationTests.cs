using PhlegmaticOne.InnoGotchi.Application.Infrastructure.MapperConfigurations;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Helpers;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Mapper;

public class MapperConfigurationTests
{
    [Fact]
    public void MapperConfigurationsFromAssembly_ShouldBeValid_Test()
    {
        var mapperConfiguration = MapperConfigurationsScanner
            .ScanAssembly(typeof(ProfileMapperConfiguration).Assembly);
        mapperConfiguration.AssertConfigurationIsValid();
    }
}