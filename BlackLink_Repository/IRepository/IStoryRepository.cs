using BlackLink_DTO.Story;

namespace BlackLink_Repository.IRepository
{
    public interface IStoryRepository
    {
        public Task<StoryFormDto> CreateStory(StoryFormDto formDto);
        public Task<IEnumerable<StoryDto>> GetAllStories();
        public Task<StoryDto> GetStory(Guid Id);
        public Task<bool> RemoveStory(Guid Id);
    }
}
