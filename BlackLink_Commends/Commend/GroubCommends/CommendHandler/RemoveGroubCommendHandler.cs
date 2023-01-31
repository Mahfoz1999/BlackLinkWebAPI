using BlackLink_Commends.Commend.GroubCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.GroubCommends.CommendHandler;

internal class RemoveGroubCommendHandler : IRequestHandler<RemoveGroubCommend, Groub>
{
    private readonly BlackLinkDbContext Context;
    public RemoveGroubCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Groub> Handle(RemoveGroubCommend request, CancellationToken cancellationToken)
    {
        Groub? groub = await Context.Groubs.Where(e => e.Id == request.Id && e.Admin == request.User).SingleOrDefaultAsync();
        if (groub == null) throw new NotFoundException("Groub Not Found");
        Context.Groubs.Remove(groub);
        await Context.SaveChangesAsync(cancellationToken);
        return groub;
    }
}
