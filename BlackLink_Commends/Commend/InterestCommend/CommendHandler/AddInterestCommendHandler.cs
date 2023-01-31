using BlackLink_Commends.Commend.InterestCommend.Commend;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommend.CommendHandler;

public class AddInterestCommendHandler : IRequestHandler<AddInterestCommend, Interest>
{
    private readonly BlackLinkDbContext Context;
    public AddInterestCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task<Interest> Handle(AddInterestCommend request, CancellationToken cancellationToken)
    {
        Interest interest = new()
        {
            InterestName = request.Name,
        };
        await Context.Interests.AddAsync(interest, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return interest;
    }
}
