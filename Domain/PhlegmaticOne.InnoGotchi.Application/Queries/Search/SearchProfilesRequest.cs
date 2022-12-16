﻿using PhlegmaticOne.InnoGotchi.Domain.Models;
using PhlegmaticOne.InnoGotchi.Shared.Profiles;
using PhlegmaticOne.OperationResults;
using PhlegmaticOne.OperationResults.Mediatr;
using PhlegmaticOne.UnitOfWork.Interfaces;

namespace PhlegmaticOne.InnoGotchi.Application.Queries.Search;

public class SearchProfilesRequest : IdentityOperationResultQuery<IList<SearchProfileDto>>
{
    public SearchProfilesRequest(Guid profileId, string searchText) : base(profileId)
    {
        SearchText = searchText;
    }

    public string SearchText { get; }
}

public class SearchProfilesRequestHandler : IOperationResultQueryHandler<SearchProfilesRequest, IList<SearchProfileDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchProfilesRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IList<SearchProfileDto>>> Handle(SearchProfilesRequest request,
        CancellationToken cancellationToken)
    {
        var toSearch = request.SearchText;
        var profileId = request.ProfileId;

        var repository = _unitOfWork.GetRepository<UserProfile>();
        var result = await repository.GetFirstOrDefaultAsync(
            predicate: p => p.User.Email.ToLower() == toSearch.ToLower() &&
                            p.Farm != null &&
                            p.Id != profileId,
            selector: s => new SearchProfileDto
            {
                Email = s.User.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Id = s.Id,
                IsAlreadyCollaborated = s.Collaborations.Select(x => x.Farm.OwnerId).Contains(profileId)
            }, cancellationToken: cancellationToken);


        IList<SearchProfileDto> list = new List<SearchProfileDto>();

        if (result is not null)
        {
            list.Add(result);
        }

        return OperationResult.Successful(list);
    }
}