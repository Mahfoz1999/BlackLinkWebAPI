using BlackLink_Commends.Commend.InterestCommend.Commend;
using BlackLink_Commends.Commend.InterestCommend.Query;
using BlackLink_DTO.Interest;
using MediatR;

namespace BlackLink_Services.InterestService;

public class InterestService : IInterestService
{
    private readonly IMediator _mediator;
    public InterestService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<AddInterestCommend> AddInterest(AddInterestCommend commned)
    {
        await _mediator.Send(commned);
        return commned;
    }
    public async Task<UpdateInterestCommend> UpdateInterest(UpdateInterestCommend commned)
    {
        await _mediator.Send(commned);
        return commned;
    }
    public async Task<IEnumerable<InterestDto>> GetAllInterests()
    {
        GetAllInterestsQuery query = new();
        var response = await _mediator.Send(query);
        return response.Select(e => new InterestDto() { Id = e.Id, InterestName = e.InterestName }).ToList();
    }

    public async Task<InterestDto> GetInterest(GetInterestByIdQuery query)
    {
        var response = await _mediator.Send(query);
        InterestDto interest = new()
        {
            Id = response.Id,
            InterestName = response.InterestName,
        };
        return interest;

    }

    public async Task<RemoveInterestCommend> RemoveInterest(RemoveInterestCommend commend)
    {
        await _mediator.Send(commend);
        return commend;
    }


}
