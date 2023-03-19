using BlackLink_Commends.Commend.AuthenticationCommends.Commend;
using BlackLink_DTO.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlackLink_Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMediator _mediator;
    public AuthenticationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<TokenModel> LogIn(UserLoginDto model)
    {
        TokenModel tokenModel = await _mediator.Send(new LogInCommend(model));
        return tokenModel;
    }

    public async Task<IdentityResult> SignUp(UserSignUpDto formDto)
    {
        IdentityResult identityResult = await _mediator.Send(new SignUpCommend(formDto));
        return identityResult;
    }

    public async Task<IdentityResult> UpdateUser(UserSignUpDto formDto)
    {
        IdentityResult identityResult = await _mediator.Send(new UpdateUserCommend(formDto));
        return identityResult;
    }
}
