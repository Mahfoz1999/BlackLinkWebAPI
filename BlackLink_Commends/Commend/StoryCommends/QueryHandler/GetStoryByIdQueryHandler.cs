using BlackLink_Commends.Commend.StoryCommends.Query;
using BlackLink_Commends.Exceptions;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.StoryCommends.QueryHandler;

public class GetStoryByIdQueryHandler : IRequestHandler<GetStoryByIdQuery, Story>
{
    private readonly BlackLinkDbContext Context;
    public GetStoryByIdQueryHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<Story> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
    {
        Story? story = await Context.Stories.FindAsync(request.Id, cancellationToken);
        return story is not null ? story : throw new NotFoundException("story Not Found");
    }
}
