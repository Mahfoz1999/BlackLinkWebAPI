namespace BlackLink_Repository.IRepository
{
    public interface ISocialRepository
    {
        public Task FolllowUser(Guid userId);
        public Task UnFollowUser(Guid userId);
        public Task AddFriend(Guid userId);
        public Task RemoveFriend(Guid userId);
        public Task BlockUser(Guid userId);
        public Task UnBlockUser(Guid userId);
    }
}
