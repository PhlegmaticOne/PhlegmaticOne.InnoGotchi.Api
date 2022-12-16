using PhlegmaticOne.InnoGotchi.Application.Commands.Farms;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Helpers;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;
using PhlegmaticOne.InnoGotchi.Shared.ErrorMessages;
using PhlegmaticOne.InnoGotchi.Shared.Farms;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Validation;

public class CreateFarmValidatorTests
{
    private readonly UnitOfWorkMock _data;
    public CreateFarmValidatorTests() => _data = UnitOfWorkMock.Create();
    [Fact]
    public async Task ShouldBeNotValid_BecauseFarmNameReserved_Test()
    {
        var reservedFarmName = _data.ReservedFarmName;
        var profileId = _data.ThatHasNoFarm.Id;
        var farmCommand = new CreateFarmCommand(profileId, new CreateFarmDto()
        {
            Name = reservedFarmName
        });
        var sut = new CreateFarmValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(farmCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.FarmNameReservedMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecauseProfileHasFarm_Test()
    {
        var farmName = Guid.NewGuid().ToString();
        var profileId = _data.ThatHasFarm.Id;
        var farmCommand = new CreateFarmCommand(profileId, new CreateFarmDto()
        {
            Name = farmName
        });
        var sut = new CreateFarmValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(farmCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.AlreadyHaveFarmMessage);
    }

    [Fact]
    public async Task ShouldBeValid_Test()
    {
        var farmName = Guid.NewGuid().ToString();
        var profileId = _data.ThatHasNoFarm.Id;
        var farmCommand = new CreateFarmCommand(profileId, new CreateFarmDto()
        {
            Name = farmName
        });
        var sut = new CreateFarmValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(farmCommand);

        ValidationResultAssertionHelper.AssertIsValid(validationResult);
    }
}