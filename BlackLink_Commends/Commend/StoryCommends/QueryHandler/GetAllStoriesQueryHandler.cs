using BlackLink_Commends.Commend.StoryCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.StoryCommends.QueryHandler;

public class GetAllStoriesQueryHandler : IRequestHandler<GetAllStoriesQuery, IEnumerable<Story>>
{
    private readonly BlackLinkDbContext Context;
    public GetAllStoriesQueryHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<IEnumerable<Story>> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
    {
        return await Context.Stories.ToListAsync(cancellationToken);
    }
}
