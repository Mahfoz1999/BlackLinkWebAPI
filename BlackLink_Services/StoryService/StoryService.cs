using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_Commends.Commend.StoryCommends.Query;
using BlackLink_DTO.Story;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Services.StoryService;

public class StoryService : IStoryService
{
    private readonly IMediator _mediator;
    public StoryService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<AddStoryCommend> AddStory(AddStoryCommend commend)
    {
        await _mediator.Send(commend);
        return commend;
    }
    public async Task UpdateStory(UpdateStoryCommend commend)
    {
        await _mediator.Send(commend);
    }
    public async Task<IEnumerable<StoryDto>> GetAllStories()
    {
        IEnumerable<Story> stories = await _mediator.Send(new GetAllStoriesQuery());
        IEnumerable<StoryDto> storyDtos = stories.Select(e => new StoryDto()
        {
            Id = e.Id,
            Content = e.Content,
            FileUrl = e.FileUrl,
            UserNiceName = e.User.NickName
        }).ToList();
        return storyDtos;
    }

    public async Task<StoryDto> GetStoryById(Guid Id)
    {
        Story story = await _mediator.Send(new GetStoryByIdQuery(Id));
        StoryDto storyDto = new()
        {
            Id = story.Id,
            Content = story.Content,
            FileUrl = story.FileUrl,
            UserNiceName = story.User.NickName
        };
        return storyDto;
    }

    public async Task RemoveStory(Guid Id)
    {
        await _mediator.Send(new RemoveStoryCommend(Id));
    }


}
