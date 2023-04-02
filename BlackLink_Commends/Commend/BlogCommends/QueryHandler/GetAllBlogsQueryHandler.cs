using BlackLink_Commends.Commend.BlogCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.BlogCommends.QueryHandler;

public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQuery, IEnumerable<Blog>>
{
    private readonly BlackLinkDbContext Context;
    public GetAllBlogsQueryHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<IEnumerable<Blog>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
    {
        return await Context.Blogs.Include(e => e.User).ThenInclude(e => e.UserPhotos).ToListAsync(cancellationToken);
    }
}
