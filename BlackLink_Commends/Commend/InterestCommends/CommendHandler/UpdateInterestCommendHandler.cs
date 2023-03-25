using BlackLink_Commends.Commend.InterestCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.InterestCommends.CommendHandler;

public class UpdateInterestCommendHandler : IRequestHandler<UpdateInterestCommend>
{
    private readonly BlackLinkDbContext Context;
    public UpdateInterestCommendHandler(BlackLinkDbContext context)
    {
        Context = context;
    }

    public async Task Handle(UpdateInterestCommend request, CancellationToken cancellationToken)
    {
        int interest = await Context.Interests.Where(e => e.Id == request.Id).ExecuteUpdateAsync(e => e.SetProperty(x => x.InterestName, request.Name));
        if (interest is 0) throw new NotFoundException("Interest Not Found");
        await Context.SaveChangesAsync(cancellationToken);
    }
}
