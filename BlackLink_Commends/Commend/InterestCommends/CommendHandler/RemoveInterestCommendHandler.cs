using BlackLink_Commends.Commend.InterestCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommends.CommendHandler;

public class RemoveInterestCommendHandler : IRequestHandler<RemoveInterestCommend, Interest>
{
    private readonly BlackLinkDbContext Context;
    public RemoveInterestCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Interest> Handle(RemoveInterestCommend request, CancellationToken cancellationToken)
    {
        try
        {
            Interest? interest = await Context.Interests.FindAsync(request.Id);
            if (interest == null) throw new NotFoundException("Interest Not Found");
            Context.Interests.Remove(interest);
            await Context.SaveChangesAsync(cancellationToken);
            return interest;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
