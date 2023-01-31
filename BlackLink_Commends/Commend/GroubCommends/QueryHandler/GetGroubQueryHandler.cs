using BlackLink_Commends.Commend.GroubCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.GroubCommends.QueryHandler;

public class GetGroubQueryHandler : IRequestHandler<GetGroubQuery, Groub>
{
    private readonly BlackLinkDbContext Context;
    public GetGroubQueryHandler(BlackLinkDbContext context)
    {
        Context = context;
    }
    public async Task<Groub> Handle(GetGroubQuery request, CancellationToken cancellationToken)
    {
        Groub? groub = await Context.Groubs.FindAsync(request.Id);
        return groub == null ? throw new NotFoundException("Groub Not Found") : groub;
    }
}
