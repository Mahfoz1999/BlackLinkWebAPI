using BlackLink_Commends.Commend.InterestCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.InterestCommends.QueryHandler;

public class GetAllInterestsQueryHandler : IRequestHandler<GetAllInterestsQuery, IEnumerable<Interest>>
{
    private readonly BlackLinkDbContext Context;
    public GetAllInterestsQueryHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<Interest>> Handle(GetAllInterestsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Interests.ToListAsync(cancellationToken);
    }
}
