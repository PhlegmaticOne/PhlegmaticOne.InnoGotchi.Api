﻿using PhlegmaticOne.InnoGotchi.Application.Commands.Synchronization.Base;
using PhlegmaticOne.InnoGotchi.Domain.Models;
using PhlegmaticOne.InnoGotchi.Domain.Providers.Writable;
using PhlegmaticOne.OperationResults.Mediatr;
using PhlegmaticOne.UnitOfWork.Interfaces;

namespace PhlegmaticOne.InnoGotchi.Application.Commands.Synchronization;

public class SynchronizeInnoGotchiesInProfileFarmCommand : IdentityOperationResultCommand
{
    public SynchronizeInnoGotchiesInProfileFarmCommand(Guid profileId) : base(profileId)
    {
    }
}

public class SynchronizeInnoGotchiesInProfileFarmCommandHandler :
    SynchronizeFarmCommandHandlerBase<SynchronizeInnoGotchiesInProfileFarmCommand>
{
    public SynchronizeInnoGotchiesInProfileFarmCommandHandler(IUnitOfWork unitOfWork,
        IInnoGotchiesSynchronizationProvider innoGotchiesSynchronizationProvider) :
        base(unitOfWork, innoGotchiesSynchronizationProvider)
    {
    }

    protected override Task<Guid> GetFarmId(SynchronizeInnoGotchiesInProfileFarmCommand request,
        CancellationToken cancellationToken)
    {
        return UnitOfWork.GetRepository<Farm>()
            .GetFirstOrDefaultAsync(
                predicate: x => x.OwnerId == request.ProfileId,
                selector: x => x.Id,
                cancellationToken: cancellationToken);
    }
}