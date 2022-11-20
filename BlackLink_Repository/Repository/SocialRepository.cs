using BlackLink_Database.SQLConnection;
using BlackLink_Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Repository.Repository
{
    public class SocialRepository : ISocialRepository
    {
        private readonly BlackLinkDbContext Context;
        private readonly IUserRepository userRepository;
        public SocialRepository(BlackLinkDbContext context, IUserRepository userRepository)
        {
            Context = context;
            this.userRepository = userRepository;
        }
        public async Task FolllowUser(Guid userId)
        {
            var user = await userRepository.GetCurrentUser();
            var userToFollow = await Context.Users.Where(flo => flo.Id.Equals(userId)).SingleOrDefaultAsync();
            if (userToFollow is not null)
            {
                user.Followers.Add(userToFollow);
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("User Not Found");

        }
        public async Task UnFollowUser(Guid userId)
        {
            var user = await userRepository.GetCurrentUser();
            var userToFollow = await Context.Users.Where(flo => flo.Id.Equals(userId)).SingleOrDefaultAsync();
            if (userToFollow is not null)
            {
                user.Followers.Remove(userToFollow);
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("User Not Found");
        }
        public async Task AddFriend(Guid userId)
        {
            var user = await userRepository.GetCurrentUser();
            var userToAdd = await Context.Users.Where(flo => flo.Id.Equals(userId)).SingleOrDefaultAsync();
            if (userToAdd is not null)
            {
                user.Friends.Add(userToAdd);
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("User Not Found");

        }
        public async Task RemoveFriend(Guid userId)
        {
            var user = await userRepository.GetCurrentUser();
            var userToAdd = await Context.Users.Where(flo => flo.Id.Equals(userId)).SingleOrDefaultAsync();
            if (userToAdd is not null)
            {
                user.Friends.Remove(userToAdd);
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("User Not Found");

        }
        public async Task BlockUser(Guid userId)
        {
            var user = await userRepository.GetCurrentUser();
            var userToBlock = await Context.Users.Where(flo => flo.Id.Equals(userId)).SingleOrDefaultAsync();
            if (userToBlock is not null)
            {
                user.BlockUsers.Add(userToBlock);
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("User Not Found");
        }
        public async Task UnBlockUser(Guid userId)
        {
            var user = await userRepository.GetCurrentUser();
            var userToBlock = await Context.Users.Where(flo => flo.Id.Equals(userId)).SingleOrDefaultAsync();
            if (userToBlock is not null)
            {
                user.BlockUsers.Remove(userToBlock);
                Context.Users.Update(user);
                await Context.SaveChangesAsync();
            }
            else throw new KeyNotFoundException("User Not Found");
        }


    }
}
