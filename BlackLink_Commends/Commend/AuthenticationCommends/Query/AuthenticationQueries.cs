using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlackLink_Commends.Commend.AuthenticationCommends.Query;

public record GetTokenQuery(List<Claim> authClaims) : IRequest<JwtSecurityToken>;


