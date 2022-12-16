﻿using AutoMapper;
using PhlegmaticOne.InnoGotchi.Domain.Models;
using PhlegmaticOne.InnoGotchi.Shared.Components;
using PhlegmaticOne.OperationResults;
using PhlegmaticOne.OperationResults.Mediatr;
using PhlegmaticOne.UnitOfWork.Interfaces;

namespace PhlegmaticOne.InnoGotchi.Application.Queries.InnoGotchiComponents;

public class GetAllComponentsQuery : IOperationResultQuery<IList<InnoGotchiComponentDto>>
{
}

public class GetAllComponentsQueryHandler :
    IOperationResultQueryHandler<GetAllComponentsQuery, IList<InnoGotchiComponentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllComponentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResult<IList<InnoGotchiComponentDto>>> Handle(GetAllComponentsQuery request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<InnoGotchiComponent>();
        var all = await repository.GetAllAsync(cancellationToken: cancellationToken);
        var mapped = _mapper.Map<IList<InnoGotchiComponentDto>>(all);
        return OperationResult.Successful(mapped);
    }
}