using FluentAssertions;
using PhlegmaticOne.InnoGotchi.Application.Queries.Farms;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Queries.Farms;

public class GetIsExistsFarmQueryHandlerTests
{
    private readonly UnitOfWorkMock _data;
    private readonly GetIsFarmExistsQueryHandler _sut;
    public GetIsExistsFarmQueryHandlerTests()
    {
        _data = UnitOfWorkMock.Create();
        _sut = new GetIsFarmExistsQueryHandler(_data.UnitOfWork);
    }

    [Fact]
    public async Task ShouldReturnTrue_Test()
    {
        var profile = _data.ThatHasFarm;
        var profileId = profile.Id;
        var query = new GetIsFarmExistsQuery(profileId);

        var result = await _sut.Handle(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Result.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldReturnFalse_Test()
    {
        var profile = _data.ThatHasNoFarm;
        var profileId = profile.Id;
        var query = new GetIsFarmExistsQuery(profileId);

        var result = await _sut.Handle(query, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Result.Should().BeFalse();
    }
}