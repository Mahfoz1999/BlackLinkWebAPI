using BlackLink_Commends.Commend.GroubCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.GroubCommends.CommendHandler;

public class UpdateGroubCommendHandler : IRequestHandler<UpdateGroubCommend, Groub>
{
    private readonly BlackLinkDbContext Context;
    public UpdateGroubCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Groub> Handle(UpdateGroubCommend request, CancellationToken cancellationToken)
    {
        Groub? groub = await Context.Groubs.Where(e => e.Id == request.Id && e.Admin == request.User).SingleOrDefaultAsync();
        if (groub is null) throw new NotFoundException("Groub Not Found");
        groub.Name = request.Name;
        groub.Description = request.Description;
        await Context.SaveChangesAsync(cancellationToken);
        return groub;

    }
}
