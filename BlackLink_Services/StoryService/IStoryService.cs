using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_DTO.Story;

namespace BlackLink_Services.StoryService;

public interface IStoryService
{
    public Task<AddStoryCommend> AddStory(AddStoryCommend commend);
    public Task<UpdateStoryCommend> UpdateStory(UpdateStoryCommend commend);
    public Task<IEnumerable<StoryDto>> GetAllStories();
    public Task<StoryDto> GetStoryById(Guid Id);
    public Task RemoveStory(Guid Id);
}
