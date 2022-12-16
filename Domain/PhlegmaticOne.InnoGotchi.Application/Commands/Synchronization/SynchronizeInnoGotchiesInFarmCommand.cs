using PhlegmaticOne.InnoGotchi.Application.Commands.Synchronization.Base;
using PhlegmaticOne.InnoGotchi.Domain.Providers.Writable;
using PhlegmaticOne.OperationResults.Mediatr;
using PhlegmaticOne.UnitOfWork.Interfaces;

namespace PhlegmaticOne.InnoGotchi.Application.Commands.Synchronization;

public class SynchronizeInnoGotchiesInFarmCommand : IOperationResultCommand
{
    public SynchronizeInnoGotchiesInFarmCommand(Guid farmId) => FarmId = farmId;

    public Guid FarmId { get; }
}

public class SynchronizeInnoGotchiesInFarmCommandHandler :
    SynchronizeFarmCommandHandlerBase<SynchronizeInnoGotchiesInFarmCommand>
{
    public SynchronizeInnoGotchiesInFarmCommandHandler(IUnitOfWork unitOfWork,
        IInnoGotchiesSynchronizationProvider innoGotchiesSynchronizationProvider) :
        base(unitOfWork, innoGotchiesSynchronizationProvider)
    { }

    protected override Task<Guid> GetFarmId(SynchronizeInnoGotchiesInFarmCommand request,
        CancellationToken cancellationToken) => Task.FromResult(request.FarmId);
}