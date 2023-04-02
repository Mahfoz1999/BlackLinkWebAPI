using BlackLink_Commends.Commend.AuthenticationCommends.Commend;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Mail;
using BlackLink_Models.Models;
using BlackLink_Models.Models.Files;
using BlackLink_SharedKernal.Enum.File;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace BlackLink_Commends.Commend.AuthenticationCommends.CommendHandler;

public class SignUpCommendHandler : IRequestHandler<SignUpCommend, IdentityResult>
{
    private IHttpContextAccessor _httpContextAccessor { get; set; }
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly BlackLinkDbContext Context;
    public SignUpCommendHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, BlackLinkDbContext Context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        this.Context = Context;
    }
    public async Task<IdentityResult> Handle(SignUpCommend request, CancellationToken cancellationToken)
    {
        try
        {
            User user = new()
            {
                NickName = request.formDto.NickName,
                UserName = request.formDto.UserName,
                Email = request.formDto.Email,
                Gender = request.formDto.Gender,
                Birthdate = request.formDto.Birthdate,
            };
            var result = await _userManager.CreateAsync(user, request.formDto.Password);
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
            List<Interest> interests = await Context.Interests.Where(i => request.formDto.InterestsIds.Contains(i.Id)).ToListAsync();
            foreach (var interest in interests)
            {
                await Context.InterestUsers.AddAsync(new InterestUser()
                {
                    Interest = interest,
                    User = user,
                });
            }
            if (request.formDto.File is not null)
            {
                UserPhoto userPhoto = new() { PhotoUrl = await FileManagment.SaveFile(request.formDto.File, FileType.Users), User = user };
                await Context.UserPhotos.AddAsync(userPhoto);
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
        catch (Exception)
        {
            throw;
        }
    }
}
