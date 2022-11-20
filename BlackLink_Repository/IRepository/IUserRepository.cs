using BlackLink_DTO.User;
using BlackLink_Models.Models;

namespace BlackLink_Repository.IRepository
{
    public interface IUserRepository
    {
        public Task<UserInfoDto> GetUser(Guid Id);
        public Task<IEnumerable<UserDto>> GetAllUsers();
        public Task<User> GetCurrentUser();
    }
}
