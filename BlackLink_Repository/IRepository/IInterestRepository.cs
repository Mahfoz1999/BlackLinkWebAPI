using BlackLink_DTO.Interest;

namespace BlackLink_Repository.IRepository
{
    public interface IInterestRepository
    {
        public Task<InterestFormDto> AddInterests(InterestFormDto formDto);
        public Task<InterestFormDto> UpdateInterests(InterestFormDto formDto);
        public Task<bool> AddUserInterests(List<Guid> InterestsIds);
        public Task<bool> UpdateUserInterests(List<Guid> InterestsIds);
        public Task<IEnumerable<InterestDto>> GetAllInterests();
        public Task<InterestDto> GetUserInterests();
        public Task<bool> RemoveInterests(Guid Id);
        public Task<bool> RemoveUserInterests(List<Guid> InterestsIds);
    }
}
