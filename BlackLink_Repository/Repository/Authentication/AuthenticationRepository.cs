using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Mail;
using BlackLink_DTO.User;
using BlackLink_IRepository.IRepository.Authentication;
using BlackLink_Models.Models;
using BlackLink_Models.Models.Files;
using BlackLink_Services.MailService;
using BlackLink_SharedKernal.Enum.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace BlackLink_Repository.Repository.Authentication;

public class AuthenticationRepository : IAuthenticationRepository
{
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMailService MaileService;
    private readonly BlackLinkDbContext Context;
    private readonly IConfiguration configuration;
    public AuthenticationRepository(
    UserManager<User> userManager,
    IConfiguration configuration,
    IMailService MaileService,
    BlackLinkDbContext Context,
    RoleManager<IdentityRole> roleManager,
    IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        this.MaileService = MaileService;
        this.Context = Context;
        _roleManager = roleManager;
    }
    public async Task<IdentityResult> SignUp(UserSignUpDto userDto)
    {
        User user = new()
        {
            NickName = userDto.NickName,
            UserName = userDto.UserName,
            Email = userDto.Email,
            Gender = userDto.Gender,
            Birthdate = userDto.Birthdate,
            AboutMe = userDto.AboutMe,
            City = userDto.City,
            Country = userDto.Country,
        };
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }
        if (userDto.Files is not null)
            foreach (var file in userDto.Files)
            {
                UserPhoto userPhoto = new()
                {
                    PhotoUrl = await FileManagment.SaveFile(file, FileType.Users),
                    User = user
                };
                await Context.UserPhotos.AddAsync(userPhoto);
            }
        List<Interest> interests = await Context.Interests.Where(i => userDto.InterestsIds.Contains(i.Id)).ToListAsync();
        foreach (var interest in interests)
        {
            await Context.InterestUsers.AddAsync(new InterestUser()
            {
                Interest = interest,
                User = user,
            });
        }
        await Context.SaveChangesAsync();
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string codeHtmlVersion = HttpUtility.UrlEncode(token);
        var confirmationLink = "https://" + _httpContextAccessor?.HttpContext?.Request.Host + $"/api/Authentication/ConfirmEmail?token={codeHtmlVersion}&email={user.Email}";
        if (confirmationLink is not null)
        {
            MailRequest mailRequest = new()
            {
                ToEmail = user.Email!,
                Subject = "Email Confirmation",
                Body = confirmationLink,
            };
            // await MaileService.SendEmailAsync(mailRequest);
        }
        return result;

    }
    public async Task<IdentityResult> UpdateUserAsync(UserSignUpDto userDto)
    {
        var _httpcontext = _httpContextAccessor.HttpContext;
        if (_httpcontext != null
                && _httpcontext.User != null
                && _httpcontext.User.Identity != null
                && _httpcontext.User.Identity.IsAuthenticated)
        {
            var UserName = _httpcontext.User.FindFirstValue(ClaimTypes.Name);
            User? user = await Context.Users.Where(user => user.UserName == UserName).SingleOrDefaultAsync();
            user!.Birthdate = userDto.Birthdate;
            user.NickName = userDto.NickName;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
        else
            throw new UnauthorizedAccessException("User is not Authenticated");
    }
    public async Task<TokenModel> Login(UserLoginDto model)
    {
        User? user = await _userManager.FindByNameAsync(model.UserName);
        /* bool emailStatus = await _userManager.IsEmailConfirmedAsync(user);
         if (emailStatus == false)
         {
             throw new AppException("Email is unconfirmed, please confirm it first");
         }*/
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
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

            var token = GetToken(authClaims);

            return new TokenModel()
            {
                token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

        }
        throw new UnauthorizedAccessException();
    }
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:secret"]!));

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:validIssuer"],
            audience: configuration["JwtConfig:validAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
    /* public async Task<IdentityResult> ChangePassword(string oldPassword, string newPassword)
     {
         var user = await userRepository.GetCurrentUser();
         IdentityResult changePassResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
         return changePassResult;
     }*/




}
