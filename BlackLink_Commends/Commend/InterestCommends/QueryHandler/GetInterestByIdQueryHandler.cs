using BlackLink_Commends.Commend.InterestCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommends.QueryHandler;

public class GetInterestByIdQueryHandler : IRequestHandler<GetInterestByIdQuery, Interest>
{
    private readonly BlackLinkDbContext Context;
    public GetInterestByIdQueryHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Interest> Handle(GetInterestByIdQuery request, CancellationToken cancellationToken)
    {
        Interest? interest = await Context.Interests.FindAsync(request.Id);
        return interest == null ? throw new NotFoundException("Interest Not Found") : interest;
    }
}
