using BlackLink_Commends.Commend.BlogCommends.Query;
using BlackLink_Commends.Commend.BlogCommentCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.BlogCommentCommends.QueryHandler;

public class GetAllBlogCommentsQueryHandler : IRequestHandler<GetAllBlogCommentsQuery, IEnumerable<BlogComment>>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public GetAllBlogCommentsQueryHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;
    }
    public async Task<IEnumerable<BlogComment>> Handle(GetAllBlogCommentsQuery request, CancellationToken cancellationToken)
    {
        Blog blog = await _mediator.Send(new GetBlogByIdQuery(request.blogId));
        IEnumerable<BlogComment> blogComments = await Context.BlogComments.Where(e => e.Blog == blog).ToListAsync(cancellationToken);
        return blogComments;
    }
}
