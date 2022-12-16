using FluentValidation;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.HelpModels;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators.Base;
using PhlegmaticOne.UnitOfWork.Interfaces;

namespace PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;

public class ProfileFarmModelValidator : AbstractValidator<ProfileFarmModel>
{
    public ProfileFarmModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.ProfileId).ProfileMustHaveFarm(unitOfWork);
    }
}