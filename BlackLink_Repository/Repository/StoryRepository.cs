using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Story;
using BlackLink_Models.Models;
using BlackLink_Repository.IRepository;
using BlackLink_Repository.Util;
using BlackLink_SharedKernal.Enum.File;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Repository.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly BlackLinkDbContext Context;
        private readonly IUserRepository userRepository;
        public StoryRepository(BlackLinkDbContext context, IUserRepository userRepository)
        {
            Context = context;
            this.userRepository = userRepository;
        }

        public async Task<StoryFormDto> CreateStory(StoryFormDto formDto)
        {
            var user = await userRepository.GetCurrentUser();
            var story = new Story()
            {
                Content = formDto.Content,
                User = user
            };
            if (formDto.File is not null)
                story.FileUrl = await FileManagment.SaveFile(FileType.Stories, formDto.File);
            await Context.Stories.AddAsync(story);
            await Context.SaveChangesAsync();
            return formDto;
        }

        public async Task<bool> RemoveStory(Guid Id)
        {
            var user = await userRepository.GetCurrentUser();
            var story = await Context.Stories.Where(story => story.Id == Id && story.User == user).ExecuteDeleteAsync();
            if (story is not 0)
            {
                await Context.SaveChangesAsync();
                return true;
            }
            else throw new KeyNotFoundException("Story Not Found");
        }

        public async Task<IEnumerable<StoryDto>> GetAllStories()
        {
            var stories = await Context.Stories.Include(story => story.User).Select(story => new StoryDto()
            {
                Id = story.Id,
                Content = story.Content,
                FileUrl = story.FileUrl,
                UserNiceName = story.User.NickName
            }).ToListAsync();
            return stories;
        }

        public async Task<StoryDto> GetStory(Guid Id)
        {
            var story = await Context.Stories.Include(story => story.User).Where(story => story.Id == Id).Select(story => new StoryDto()
            {
                Id = story.Id,
                Content = story.Content,
                FileUrl = story.FileUrl,
                UserNiceName = story.User.NickName
            }).SingleOrDefaultAsync();
            return story is not null ? story : throw new KeyNotFoundException("Story Not Found");
        }


    }
}
