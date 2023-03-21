using BlackLink_Commends.Commend.BlogCommentCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommentCommends.QueryHandler;

public class GetBlogCommentByIdQueryHandler : IRequestHandler<GetBlogCommentByIdQuery, BlogComment>
{
    private readonly BlackLinkDbContext Context;
    public GetBlogCommentByIdQueryHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<BlogComment> Handle(GetBlogCommentByIdQuery request, CancellationToken cancellationToken)
    {
        BlogComment? blogComment = await Context.BlogComments.FindAsync(request.Id)!;
        return blogComment is not null ? blogComment : throw new NotFoundException("Comment Not Found");
    }
}
