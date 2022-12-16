﻿using AutoFixture;
using PhlegmaticOne.InnoGotchi.Application.Commands.Profiles;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Helpers;
using PhlegmaticOne.InnoGotchi.Application.Tests.Infrastructure.Mocks;
using PhlegmaticOne.InnoGotchi.Shared.ErrorMessages;
using PhlegmaticOne.InnoGotchi.Shared.Profiles.Anonymous;

namespace PhlegmaticOne.InnoGotchi.Application.Tests.Validation;

public class RegisterProfileValidatorTests
{
    private readonly UnitOfWorkMock _data;
    public RegisterProfileValidatorTests() => _data = UnitOfWorkMock.Create();

    [Fact]
    public async Task ShouldBeNotValid_BecauseProfileWithEmailExists_Test()
    {
        var registerDto = new Fixture()
            .Build<RegisterProfileDto>()
            .With(x => x.Email, _data.ThatHasFarm.User.Email)
            .Create();
        var registerCommand = new RegisterProfileCommand(registerDto);
        var sut = new RegisterProfileValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(registerCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.EmailExistsMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecauseEmailIsIncorrect_Test()
    {
        var registerDto = new Fixture()
            .Build<RegisterProfileDto>()
            .With(x => x.Email, "IncorrectEmail")
            .Create();
        var registerCommand = new RegisterProfileCommand(registerDto);
        var sut = new RegisterProfileValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(registerCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.EmailIncorrectMessage);
    }

    [Fact]
    public async Task ShouldBeNotValid_BecausePasswordIsIncorrect_Test()
    {
        var registerDto = new Fixture()
            .Build<RegisterProfileDto>()
            .With(x => x.Password, "Incorrectpassword")
            .Create();
        var registerCommand = new RegisterProfileCommand(registerDto);
        var sut = new RegisterProfileValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(registerCommand);

        ValidationResultAssertionHelper.AssertNotValidWithSingleError(validationResult,
            AppErrorMessages.PasswordIncorrectMessage);
    }

    [Fact]
    public async Task ShouldBeValid_Test()
    {
        var registerDto = new Fixture()
            .Build<RegisterProfileDto>()
            .With(x => x.Password, "CorrectPassword_12345")
            .With(x => x.FirstName, "Firstname")
            .With(x => x.LastName, "Lastname")
            .With(x => x.Email, "myemail@gmail.com")
            .Create();
        var registerCommand = new RegisterProfileCommand(registerDto);
        var sut = new RegisterProfileValidator(_data.UnitOfWork);

        var validationResult = await sut.ValidateAsync(registerCommand);

        ValidationResultAssertionHelper.AssertIsValid(validationResult);
    }
}