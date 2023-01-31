using BlackLink_Commends.Commend.InterestCommend.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommend.CommendHandler;

public class UpdateInterestCommendHandler : IRequestHandler<UpdateInterestCommend, Interest>
{
    private readonly BlackLinkDbContext Context;
    public UpdateInterestCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Interest> Handle(UpdateInterestCommend request, CancellationToken cancellationToken)
    {
        Interest? interest = await Context.Interests.FindAsync(request.Id);
        if (interest is null) throw new NotFoundException("Interest Not Found");
        interest.InterestName = request.Name;
        Context.Interests.Update(interest);
        await Context.SaveChangesAsync(cancellationToken);
        return interest;
    }
}
