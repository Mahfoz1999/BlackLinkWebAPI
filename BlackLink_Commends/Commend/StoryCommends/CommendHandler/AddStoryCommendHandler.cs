using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using BlackLink_SharedKernal.Enum.File;
using MediatR;

namespace BlackLink_Commends.Commend.StoryCommends.CommendHandler;

public class AddStoryCommendHandler : IRequestHandler<AddStoryCommend, Story>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public AddStoryCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;
    }
    public async Task<Story> Handle(AddStoryCommend request, CancellationToken cancellationToken)
    {
        GetCurrentUserQuery getCurrentUser = new();
        User user = await _mediator.Send(getCurrentUser);
        Story story = new()
        {
            Content = request.content,
            User = user,
        };
        if (request.file is not null)
            story.FileUrl = await FileManagment.SaveFile(request.file, FileType.Stories);
        await Context.Stories.AddAsync(story, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return story;
    }
}
