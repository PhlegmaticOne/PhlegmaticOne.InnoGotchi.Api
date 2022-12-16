using FluentAssertions;
using PhlegmaticOne.InnoGotchi.Application.Providers.Readable;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Providers.Readable;

public class ReadableInnoGotchiProviderTests
{
    private readonly UnitOfWorkMock _data;
    public ReadableInnoGotchiProviderTests() => _data = UnitOfWorkMock.Create();

    [Fact]
    public async Task GetDetailedAsync_ShouldReturnPet_Test()
    {
        var petId = _data.AlivePet.Id;
        var sut = new ReadableInnoGotchiProvider(_data.UnitOfWork);

        var pet = await sut.GetDetailedAsync(petId);

        pet.Should().NotBeNull();
    }

    [Fact]
    public async Task GetDetailedAsync_ShouldReturnNull_Test()
    {
        var petId = Guid.Empty;
        var sut = new ReadableInnoGotchiProvider(_data.UnitOfWork);

        var pet = await sut.GetDetailedAsync(petId);

        pet.Should().BeNull();
    }
}