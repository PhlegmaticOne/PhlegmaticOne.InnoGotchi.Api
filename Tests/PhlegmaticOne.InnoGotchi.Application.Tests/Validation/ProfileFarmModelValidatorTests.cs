using PhlegmaticOne.InnoGotchi.Application.Infrastructure.HelpModels;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Helpers;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;
using PhlegmaticOne.InnoGotchi.Shared.ErrorMessages;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Validation;

public class ProfileFarmModelValidatorTests
{
    private readonly UnitOfWorkMock _data;
    public ProfileFarmModelValidatorTests() => _data = UnitOfWorkMock.Create();

    [Fact]
    public async Task ShouldBeNotValid_BecauseProfileDoesNotHaveFarm_Test()
    {
        var profileId = _data.ThatHasNoFarm.Id;
        var profileFarmModel = new ProfileFarmModel(profileId);
        var sut = new ProfileFarmModelValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(profileFarmModel);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.FarmDoesNotExistMessage);
    }

    [Fact]
    public async Task ShouldBeValid_Test()
    {
        var profileId = _data.ThatHasNoFarm.Id;
        var profileFarmModel = new ProfileFarmModel(profileId);
        var sut = new ProfileFarmModelValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(profileFarmModel);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.FarmDoesNotExistMessage);
    }
}