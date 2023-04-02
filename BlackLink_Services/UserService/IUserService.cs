using BlackLink_DTO.User;

namespace BlackLink_Services.UserService;

public interface IUserService
{
    public Task<IEnumerable<UserDto>> GetAllUsers();
}
