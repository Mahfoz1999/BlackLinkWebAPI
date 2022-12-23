using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Interest;
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using BlackLink_Repository.IRepository;
using BlackLink_SharedKernal.Enum.Personality;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace BlackLink_Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly BlackLinkDbContext Context;
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public UserRepository(UserManager<User> userManager, BlackLinkDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            Context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetCurrentUser()
        {

            var _httpcontext = _httpContextAccessor.HttpContext;
            if (_httpcontext != null
                    && _httpcontext.User != null
                    && _httpcontext.User.Identity != null
                    && _httpcontext.User.Identity.IsAuthenticated)
            {
                User? user = await _userManager.GetUserAsync(_httpcontext.User)!;
                return user!;
            }
            else
                throw new UnauthorizedAccessException("User is not Authenticated");

        }
        private async Task<GenderPrefere> GetGenderPrefereForCurrentUser()
        {
            var _httpcontext = _httpContextAccessor.HttpContext;
            if (_httpcontext != null
                    && _httpcontext.User != null
                    && _httpcontext.User.Identity != null
                    && _httpcontext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(_httpcontext.User);
                return user!.GenderPrefere;
            }
            else
                throw new UnauthorizedAccessException("User is not Authenticated");
        }
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            GenderPrefere genderPrefere = await GetGenderPrefereForCurrentUser();
            var users = await Context.Users.Where(user => user.GenderPrefere == genderPrefere)
               .Select(user => new UserDto()
               {
                   Id = Guid.Parse(user.Id),
                   NickName = user.NickName,
                   Age = Math.Abs(user.Birthdate.Year - DateTimeOffset.Now.Year),
                   Country = user.Country!,
                   City = user.City!,
                   PhotoUrl = user.UserPhotos.Select(p => p.PhotoUrl).FirstOrDefault()!,
               }).ToListAsync();
            return users;
        }
        public async Task<UserInfoDto> GetUser(Guid Id)
        {
            var user = await Context.Users
                .Where(user => user.Id == Id.ToString()).Select(user => new UserInfoDto()
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName!,
                    NickName = user.NickName,
                    Birthdate = user.Birthdate,
                    CreationDate = user.CreationDate,
                    Country = user.Country,
                    City = user.City,
                    Age = Math.Abs(user.Birthdate.Year - DateTimeOffset.Now.Year),
                    GenderPrefere = user.GenderPrefere,
                    AboutMe = user.AboutMe,
                    FacebookLink = user.FacebookLink,
                    InstagramLink = user.InstagramLink,
                    Gender = user.Gender,
                    Latitude = user.Latitude,
                    Longitude = user.Longitude,
                    Friends = user.Friends.Count,
                    Followers = user.Followers.Count,
                    Interests = user.InterestUsers.Select(inte => new InterestUserDto()
                    {
                        Id = inte.Interest.Id,
                        InterestName = inte.Interest.InterestName
                    }).ToList(),
                    UserPhotos = user.UserPhotos.Select(p => p.PhotoUrl).ToList(),
                }).SingleOrDefaultAsync();
            return user is not null ? user : throw new KeyNotFoundException("User Not Found");
        }
        public async Task UpdateCurrentUserPhoto(IFormFile file)
        {
            User? user = await GetCurrentUser();
            if (user is not null)
            {
                if (file is not null)
                {
                    /* if (user.PhotoUrl is not "")
                         FileManagment.DeleteFile(user.PhotoUrl);
                     user.PhotoUrl = await FileManagment.SaveFile(FileType.Users, file);
                     await Context.SaveChangesAsync();*/
                }
            }
        }
    }
}
