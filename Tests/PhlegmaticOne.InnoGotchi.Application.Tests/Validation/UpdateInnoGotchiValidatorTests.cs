﻿using Moq;
using PhlegmaticOne.InnoGotchi.Application.Commands.InnoGotchies;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Helpers;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;
using PhlegmaticOne.InnoGotchi.Domain.Providers.Readable;
using PhlegmaticOne.InnoGotchi.Shared.ErrorMessages;
using PhlegmaticOne.InnoGotchi.Shared.InnoGotchies;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Validation;

public class UpdateInnoGotchiValidatorTests
{
    private readonly UnitOfWorkMock _data;
    public UpdateInnoGotchiValidatorTests() => _data = UnitOfWorkMock.Create();
    [Fact]
    public async Task ShouldBeNotValid_BecausePetIsDead_Test()
    {
        var profileId = _data.ThatHasFarm.Id;
        var petId = _data.DeadPet.Id;
        var updateInnoGotchiCommand = new UpdateInnoGotchiCommand(profileId, new()
        {
            PetId = petId,
            InnoGotchiOperationType = InnoGotchiOperationType.Drinking
        });
        var ownChecker = new Mock<IInnoGotchiOwnChecker>();
        ownChecker.Setup(x => x.IsBelongAsync(profileId, petId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var sut = new UpdateInnoGotchiValidator(ownChecker.Object, _data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(updateInnoGotchiCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.CannotUpdateDeadPetMessage);
    }

    [Fact]
    public async Task ShouldBeValid_Test()
    {
        var profileId = _data.AlivePet.Farm.OwnerId;
        var petId = _data.AlivePet.Id;
        var updateInnoGotchiCommand = new UpdateInnoGotchiCommand(profileId, new()
        {
            PetId = petId,
            InnoGotchiOperationType = InnoGotchiOperationType.Drinking
        });
        var ownChecker = new Mock<IInnoGotchiOwnChecker>();
        ownChecker.Setup(x => x.IsBelongAsync(profileId, petId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var sut = new UpdateInnoGotchiValidator(ownChecker.Object, _data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(updateInnoGotchiCommand);

        ValidationResultAssertionHelper.AssertIsValid(validationResult);
    }
}