using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.BlogCommentCommends.CommendHandler;

public class RemoveBlogCommentCommendHandler : IRequestHandler<RemoveBlogCommentCommend>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public RemoveBlogCommentCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;
    }
    public async Task Handle(RemoveBlogCommentCommend request, CancellationToken cancellationToken)
    {
        User user = await _mediator.Send(new GetCurrentUserQuery());
        int comment = await Context.BlogComments
            .Include(e => e.Blog)
            .ThenInclude(e => e.User).Where(e => e.User == user || e.Blog.User == user).ExecuteDeleteAsync(cancellationToken);
        if (comment is not 0)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
            throw new NotFoundException("Blog Comment Not Found");

    }
}
