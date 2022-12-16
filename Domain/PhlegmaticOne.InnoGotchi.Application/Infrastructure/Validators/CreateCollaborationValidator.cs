using FluentValidation;
using PhlegmaticOne.InnoGotchi.Application.Commands.Collaborations;
using PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators.Base;
using PhlegmaticOne.InnoGotchi.Domain.Models;
using PhlegmaticOne.InnoGotchi.Shared.ErrorMessages;
using PhlegmaticOne.UnitOfWork.Interfaces;

namespace PhlegmaticOne.InnoGotchi.Application.Infrastructure.Validators;

public class CreateCollaborationValidator : AbstractValidator<CreateCollaborationCommand>
{
    public CreateCollaborationValidator(IUnitOfWork unitOfWork)
    {
        var collaborationsRepository = unitOfWork.GetRepository<Collaboration>();
        var profilesRepository = unitOfWork.GetRepository<UserProfile>();

        RuleFor(x => x.ProfileId)
            .ProfileMustHaveFarm(unitOfWork, AppErrorMessages.HaveNoFarmForCollaborationMessage);

        RuleFor(x => x.ToProfileId)
            .MustAsync((id, ct) => profilesRepository.ExistsAsync(x => x.Id == id, ct))
            .WithMessage(AppErrorMessages.ProfileDoesNotExistMessage);

        RuleFor(x => x)
            .MustAsync((model, ct) => collaborationsRepository
                .AllAsync(x => x.Farm.OwnerId != model.ProfileId && x.UserProfileId != model.ToProfileId, ct))
            .WithMessage(AppErrorMessages.SuchCollaborationExistsMessage);
    }
}