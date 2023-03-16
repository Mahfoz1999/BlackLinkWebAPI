using BlackLink_Commends.Commend.BlogCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommends.QueryHandler;

public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, Blog>
{
    private readonly BlackLinkDbContext Context;
    public GetBlogByIdQueryHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<Blog> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        Blog? blog = await Context.Blogs.FindAsync(request.Id, cancellationToken);
        return blog is not null ? blog : throw new NotFoundException("Blog Not Found");
    }
}
