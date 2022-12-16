﻿using PhlegmaticOne.InnoGotchi.Application.Commands.InnoGotchies;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Helpers;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;
using PhlegmaticOne.InnoGotchi.Shared.Components;
using PhlegmaticOne.InnoGotchi.Shared.Constructor;
using PhlegmaticOne.InnoGotchi.Shared.ErrorMessages;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Validation;

public class CreateInnoGotchiValidatorTests
{
    private readonly UnitOfWorkMock _data;
    public CreateInnoGotchiValidatorTests() => _data = UnitOfWorkMock.Create();

    [Fact]
    public async Task ShouldBeNotValid_BecauseProfileDoesNotHaveFarm_Test()
    {
        var profileId = _data.ThatHasNoFarm.Id;
        var petCommand = new CreateInnoGotchiCommand(profileId, new CreateInnoGotchiDto
        {
            Components = new List<InnoGotchiModelComponentDto>(),
        });
        var sut = new CreateInnoGotchiValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(petCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.FarmDoesNotExistMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecausePetNameReserved_Test()
    {
        var profileId = _data.ThatHasFarm.Id;
        var name = _data.AlivePet.Name;
        var petCommand = new CreateInnoGotchiCommand(profileId, new CreateInnoGotchiDto
        {
            Components = new List<InnoGotchiModelComponentDto>(),
            Name = name
        });
        var sut = new CreateInnoGotchiValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(petCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.InnoGotchiNameReservedMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecauseOfUnknownComponentCategory_Test()
    {
        var profileId = _data.ThatHasFarm.Id;
        var name = Guid.NewGuid().ToString();
        var petCommand = new CreateInnoGotchiCommand(profileId, new CreateInnoGotchiDto
        {
            Components = new List<InnoGotchiModelComponentDto>
            {
                new()
                {
                    Name = Guid.NewGuid().ToString()
                }
            },
            Name = name
        });
        var sut = new CreateInnoGotchiValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(petCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.UnknownComponentCategoryNameMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecauseOfUnknownComponentImageUrl_Test()
    {
        var profileId = _data.ThatHasFarm.Id;
        var name = Guid.NewGuid().ToString();
        var petCommand = new CreateInnoGotchiCommand(profileId, new CreateInnoGotchiDto
        {
            Components = new List<InnoGotchiModelComponentDto>
            {
                new()
                {
                    Name = "Bodies",
                    ImageUrl = Guid.NewGuid().ToString()
                }
            },
            Name = name
        });
        var sut = new CreateInnoGotchiValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(petCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.UnknownComponentImageUrlMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecausePetDoesNotHaveBody_Test()
    {
        var profileId = _data.ThatHasFarm.Id;
        var name = Guid.NewGuid().ToString();
        var petCommand = new CreateInnoGotchiCommand(profileId, new CreateInnoGotchiDto
        {
            Components = new List<InnoGotchiModelComponentDto>(),
            Name = name
        });
        var sut = new CreateInnoGotchiValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(petCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.InnoGotchiMustHaveBodyMessage);
    }

    [Fact]
    public async Task ShouldBeValid_Test()
    {
        var profileId = _data.ThatHasFarm.Id;
        var name = Guid.NewGuid().ToString();
        var bodyComponent = _data.AlivePet.Components.First().InnoGotchiComponent;
        var petCommand = new CreateInnoGotchiCommand(profileId, new CreateInnoGotchiDto
        {
            Components = new List<InnoGotchiModelComponentDto>
            {
                new()
                {
                    Name = bodyComponent.Name,
                    ImageUrl = bodyComponent.ImageUrl
                }
            },
            Name = name
        });
        var sut = new CreateInnoGotchiValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(petCommand);

        ValidationResultAssertionHelper.AssertIsValid(validationResult);
    }
}