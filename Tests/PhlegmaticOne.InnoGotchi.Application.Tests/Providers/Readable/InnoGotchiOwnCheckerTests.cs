using FluentAssertions;
using PhlegmaticOne.InnoGotchi.Application.Providers.Readable;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Providers.Readable;

public class InnoGotchiOwnCheckerTests
{
    private readonly UnitOfWorkMock _data;
    public InnoGotchiOwnCheckerTests() => _data = UnitOfWorkMock.Create();

    [Fact]
    public async Task IsBelongAsync_ShouldReturnTrueBecausePetBelongsToProfile_Test()
    {
        var profileId = _data.AlivePet.Farm.OwnerId;
        var petId = _data.AlivePet.Id;
        var sut = new InnoGotchiOwnChecker(_data.UnitOfWork);

        var isBelong = await sut.IsBelongAsync(profileId, petId);

        isBelong.Should().BeTrue();
    }

    [Fact]
    public async Task IsBelongAsync_ShouldReturnTrueBecausePetIsFromCollaboratedFarm_Test()
    {
        var profileId = _data.CreatedCollaboration.UserProfileId;
        var petId = _data.AlivePet.Id;
        var sut = new InnoGotchiOwnChecker(_data.UnitOfWork);

        var isBelong = await sut.IsBelongAsync(profileId, petId);

        isBelong.Should().BeTrue();
    }

    [Fact]
    public async Task IsBelongAsync_ShouldReturnFalseBecausePetIsNotBelongToProfile_Test()
    {
        var profileId = Guid.Empty;
        var petId = _data.AlivePet.Id;
        var sut = new InnoGotchiOwnChecker(_data.UnitOfWork);

        var isBelong = await sut.IsBelongAsync(profileId, petId);

        isBelong.Should().BeFalse();
    }
}