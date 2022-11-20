using BlackLink_DTO.Groub;


namespace BlackLink_Repository.IRepository
{
    public interface IGroubRepository
    {
        public Task<GroubFormDto> CreateGroub(GroubFormDto formDto);
        public Task<GroubFormDto> UpdateGroub(GroubFormDto formDto);
        public Task JoinToGroub(Guid groubId);
        public Task LeaveGroub(Guid groubId);
        public Task<IEnumerable<GroubUserDto>> GetUserGroubs(Guid Id);
        public Task<IEnumerable<GroubDto>> GetAllGroubs();
        public Task<GroubInfoDto> GetGroub(Guid Id);
        public Task<bool> RemoveGroub(Guid Id);
    }
}
