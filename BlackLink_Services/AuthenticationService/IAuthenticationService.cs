using BlackLink_DTO.User;
using Microsoft.AspNetCore.Identity;

namespace BlackLink_Services.AuthenticationService;

public interface IAuthenticationService
{
    public Task<IdentityResult> SignUp(UserSignUpDto formDto);
    public Task<TokenModel> LogIn(UserLoginDto model);
    public Task<IdentityResult> UpdateUser(UserSignUpDto formDto);
}
