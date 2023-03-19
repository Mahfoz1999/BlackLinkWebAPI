using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using BlackLink_SharedKernal.Enum.File;
using MediatR;

namespace BlackLink_Commends.Commend.StoryCommends.CommendHandler;

public class UpdateStoryCommendHandler : IRequestHandler<UpdateStoryCommend, Story>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public UpdateStoryCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;

    }
    public async Task<Story> Handle(UpdateStoryCommend request, CancellationToken cancellationToken)
    {
        Story? story = await Context.Stories.FindAsync(request.Id);
        if (story != null)
        {
            story.Content = request.content;
            if (request.file is not null)
            {
                FileManagment.DeleteFile(story.FileUrl!);
                story.FileUrl = await FileManagment.SaveFile(request.file, FileType.Stories);
            }
            Context.Stories.Update(story);
            await Context.SaveChangesAsync(cancellationToken);
            return story;
        }
        else throw new NotFoundException("Story Not Found");
    }
}
