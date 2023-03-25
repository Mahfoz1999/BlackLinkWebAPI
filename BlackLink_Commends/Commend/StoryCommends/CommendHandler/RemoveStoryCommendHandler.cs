using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.StoryCommends.CommendHandler;

public class RemoveStoryCommendHandler : IRequestHandler<RemoveStoryCommend>
{
    private readonly BlackLinkDbContext Context;
    public RemoveStoryCommendHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task Handle(RemoveStoryCommend request, CancellationToken cancellationToken)
    {
        Story? story = await Context.Stories.FindAsync(request.Id);
        if (story != null)
        {
            FileManagment.DeleteFile(story.FileUrl!);
            Context.Stories.Remove(story);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else throw new NotFoundException("Story Not Found");
    }
}
