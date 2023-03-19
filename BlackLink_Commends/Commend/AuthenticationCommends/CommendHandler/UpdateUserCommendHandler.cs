using BlackLink_Commends.Commend.AuthenticationCommends.Commend;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlackLink_Commends.Commend.AuthenticationCommends.CommendHandler;

public class UpdateUserCommendHandler : IRequestHandler<UpdateUserCommend, IdentityResult>
{
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    private readonly UserManager<User> _userManager;
    private BlackLinkDbContext Context { get; set; }
    public UpdateUserCommendHandler(IHttpContextAccessor httpContextAccessor, BlackLinkDbContext Context, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        this.Context = Context;
        _userManager = userManager;
    }
    public async Task<IdentityResult> Handle(UpdateUserCommend request, CancellationToken cancellationToken)
    {
        var _httpcontext = _httpContextAccessor.HttpContext;
        if (_httpcontext != null
                && _httpcontext.User != null
                && _httpcontext.User.Identity != null
                && _httpcontext.User.Identity.IsAuthenticated)
        {
            var UserName = _httpcontext.User.FindFirstValue(ClaimTypes.Name);
            User? user = await Context.Users.Where(user => user.UserName == UserName).SingleOrDefaultAsync();
            user!.Birthdate = request.userDto.Birthdate;
            user.NickName = request.userDto.NickName;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
        else
            throw new UnauthorizedAccessException("User is not Authenticated");
    }
}
