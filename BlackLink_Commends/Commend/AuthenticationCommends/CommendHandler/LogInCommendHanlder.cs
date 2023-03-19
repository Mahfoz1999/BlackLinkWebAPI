using BlackLink_Commends.Commend.AuthenticationCommends.Commend;
using BlackLink_Commends.Commend.AuthenticationCommends.Query;
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlackLink_Commends.Commend.AuthenticationCommends.CommendHandler;

public class LogInCommendHanlder : IRequestHandler<LogInCommend, TokenModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;
    public LogInCommendHanlder(UserManager<User> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<TokenModel> Handle(LogInCommend request, CancellationToken cancellationToken)
    {
        User? user = await _userManager.FindByNameAsync(request.model.UserName);
        /* bool emailStatus = await _userManager.IsEmailConfirmedAsync(user);
         if (emailStatus == false)
         {
             throw new AppException("Email is unconfirmed, please confirm it first");
         }*/
        if (user != null && await _userManager.CheckPasswordAsync(user, request.model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName !),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = await _mediator.Send(new GetTokenQuery(authClaims));
            return new TokenModel()
            {
                token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

        }
        throw new UnauthorizedAccessException();
    }
}
