using BlackLink_Commends.Commend.InterestCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.InterestCommends.CommendHandler;

public class RemoveInterestCommendHandler : IRequestHandler<RemoveInterestCommend>
{
    private readonly BlackLinkDbContext Context;
    public RemoveInterestCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task Handle(RemoveInterestCommend request, CancellationToken cancellationToken)
    {
        try
        {
            int interest = await Context.Interests.Where(e => e.Id == request.Id).ExecuteDeleteAsync();
            if (interest == 0) throw new NotFoundException("Interest Not Found");
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
