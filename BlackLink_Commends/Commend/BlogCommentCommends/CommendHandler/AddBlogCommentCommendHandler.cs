using BlackLink_Commends.Commend.BlogCommends.Query;
using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommentCommends.CommendHandler;

public class AddBlogCommentCommendHandler : IRequestHandler<AddBlogCommentCommend, BlogComment>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public AddBlogCommentCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;
    }
    public async Task<BlogComment> Handle(AddBlogCommentCommend request, CancellationToken cancellationToken)
    {
        User user = await _mediator.Send(new GetCurrentUserQuery());
        Blog blog = await _mediator.Send(new GetBlogByIdQuery(request.BlogId));
        BlogComment blogComment = new() { Blog = blog, Content = request.Content, User = user };
        await Context.BlogComments.AddAsync(blogComment);
        await Context.SaveChangesAsync(cancellationToken);
        return blogComment;
    }
}
