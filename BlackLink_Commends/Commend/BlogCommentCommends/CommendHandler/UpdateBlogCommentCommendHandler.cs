using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.BlogCommentCommends.CommendHandler;

public class UpdateBlogCommentCommendHandler : IRequestHandler<UpdateBlogCommentCommend>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public UpdateBlogCommentCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;
    }
    public async Task Handle(UpdateBlogCommentCommend request, CancellationToken cancellationToken)
    {
        User user = await _mediator.Send(new GetCurrentUserQuery());
        int blogComment = await Context.BlogComments.Where(e => e.User == user && e.Id == request.Id)
            .ExecuteUpdateAsync(e => e.SetProperty(x => x.Content, request.Content));
        if (blogComment is not 0)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        else throw new NotFoundException("Blog Comment Not Found");
    }
}
