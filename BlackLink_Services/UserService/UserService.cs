using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Services.UserService;

public class UserService : IUserService
{
    private readonly IMediator _mediator;
    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        IEnumerable<User> users = await _mediator.Send(new GetAllUsersQuery());
        IEnumerable<UserDto> userDtos = users.Select(e =>
        new UserDto(
            Id: Guid.Parse(e.Id),
            NickName: e.NickName,
            Age: GetUserAge(e.Birthdate),
            PhotoUrl: e.UserPhotos.Select(p => p.PhotoUrl).FirstOrDefault()!
            ));
        return userDtos;
    }
    private int GetUserAge(DateTime date)
    {
        var currentDate = DateTime.Now;
        var difference = currentDate.Subtract(date);
        var timespan = new TimeSpan(difference.Ticks);
        int age = Convert.ToInt32(timespan.TotalDays / 365);
        return age;
    }
}
