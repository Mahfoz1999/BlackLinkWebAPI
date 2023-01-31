using BlackLink_Commends.Commend.GroubCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.GroubCommends.QueryHandler;

public class GetAllGroubsQueryHandler : IRequestHandler<GetAllGroubsQuery, IEnumerable<Groub>>
{
    private readonly BlackLinkDbContext Context;
    public GetAllGroubsQueryHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<Groub>> Handle(GetAllGroubsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Groubs.ToListAsync(cancellationToken);
    }
}
