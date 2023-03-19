using BlackLink_DTO.User;
using Microsoft.AspNetCore.Identity;

namespace BlackLink_IRepository.IRepository.Authentication
{
    public interface IAuthenticationRepository
    {
        public Task<IdentityResult> SignUp(UserSignUpDto userDto);
        //public Task<IdentityResult> ChangePassword(string oldPassword, string newPassword);
        public Task<IdentityResult> UpdateUserAsync(UserSignUpDto userDto);
        public Task<TokenModel> Login(UserLoginDto model);
    }
}
