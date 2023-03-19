using BlackLink_DTO.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlackLink_Commends.Commend.AuthenticationCommends.Commend;

public record SignUpCommend(UserSignUpDto formDto) : IRequest<IdentityResult>;
public record LogInCommend(UserLoginDto model) : IRequest<TokenModel>;
public record UpdateUserCommend(UserSignUpDto userDto) : IRequest<IdentityResult>;
