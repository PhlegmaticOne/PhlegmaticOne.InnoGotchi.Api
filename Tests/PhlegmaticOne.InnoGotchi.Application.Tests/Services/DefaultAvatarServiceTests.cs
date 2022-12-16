using FluentAssertions;
using PhlegmaticOne.InnoGotchi.Application.Services;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Services;

public class DefaultAvatarServiceTests
{
    [Fact]
    public async Task GetDefaultAvatarDataAsync_ShouldReturnData_Test()
    {
        var sut = new DefaultAvatarService();

        var data = await sut.GetDefaultAvatarDataAsync();

        data.Should().NotBeEmpty();
    }
}