using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Blog;
using BlackLink_DTO.Groub;
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using BlackLink_Repository.Exceptions;
using BlackLink_Repository.IRepository;
using BlackLink_Repository.Util;
using BlackLink_SharedKernal.Enum.File;
using Microsoft.EntityFrameworkCore;



namespace BlackLink_Repository.Repository
{
    public class GroubRepository : IGroubRepository
    {
        private readonly BlackLinkDbContext Context;
        private readonly IUserRepository userRepository;
        public GroubRepository(BlackLinkDbContext context, IUserRepository userRepository)
        {
            Context = context;
            this.userRepository = userRepository;
        }
        #region Actions
        public async Task<GroubFormDto> CreateGroub(GroubFormDto formDto)
        {
            User user = await userRepository.GetCurrentUser();
            var groub = new Groub()
            {
                Name = formDto.Name,
                Description = formDto.Description,
                Admin = user
            };
            if (formDto.CoverFile is not null)
                groub.CoverUrl = await FileManagment.SaveFile(FileType.Blogs, formDto.CoverFile);
            groub.Users.Add(new GroubUser()
            {
                Groub = groub,
                User = user
            });
            var categories = await Context.Categories.Where(cat => formDto.CategoryIds.Contains(cat.Id)).ToListAsync();
            if (categories is not null)
            {
                foreach (var category in categories)
                {
                    await Context.CategoryEntityRealteds.AddAsync(new CategoryEntityRealted() { Groub = groub, Category = category });
                }
            }
            else throw new KeyNotFoundException("Category Not Found");
            await Context.Groubs.AddAsync(groub);
            await Context.SaveChangesAsync();
            formDto.Id = groub.Id;
            return formDto;
        }
        public async Task<GroubFormDto> UpdateGroub(GroubFormDto formDto)
        {
            User user = await userRepository.GetCurrentUser();
            Groub? groub = await Context.Groubs.Where(groub => groub.Id == formDto.Id && groub.Admin == user).SingleOrDefaultAsync();
            if (groub is not null)
            {
                if (groub.CoverUrl is not null)
                {
                    if (groub.CoverUrl is not null)
                        FileManagment.DeleteFile(groub.CoverUrl);
                    groub.CoverUrl = await FileManagment.SaveFile(FileType.Blogs, formDto.CoverFile);
                }
                groub.Name = formDto.Name;
                groub.Description = formDto.Description;
                groub.CategoryEntityRealteds.Clear();
                Context.Groubs.Update(groub);
                await Context.SaveChangesAsync();
                var categories = await Context.Categories.Where(cat => formDto.CategoryIds.Contains(cat.Id)).ToListAsync();
                if (categories is not null)
                {
                    foreach (var category in categories)
                    {
                        await Context.CategoryEntityRealteds.AddAsync(new CategoryEntityRealted() { Groub = groub, Category = category });
                    }
                }
                else throw new KeyNotFoundException("Category Not Found");
                await Context.SaveChangesAsync();
                return formDto;
            }
            else
            {
                throw new KeyNotFoundException("Groub Not Found");
            }
        }
        public async Task JoinToGroub(Guid groubId)
        {
            User user = await userRepository.GetCurrentUser();
            var groub = await Context.Groubs.Where(groub => groub.Id == groubId).SingleOrDefaultAsync();
            if (groub is not null)
            {
                var groubUser = await Context.GroubUsers.Where(gu => gu.User == user && gu.Groub == groub).SingleOrDefaultAsync();
                if (groubUser is null)
                {
                    await Context.GroubUsers.AddAsync(new GroubUser() { Groub = groub, User = user });
                    await Context.SaveChangesAsync();
                }
                else throw new AppException("user is aleardy join to this groub");
            }
            else throw new KeyNotFoundException("Groub Not Found");
        }
        public async Task LeaveGroub(Guid groubId)
        {
            User user = await userRepository.GetCurrentUser();
            var groub = await Context.Groubs.Where(groub => groub.Id == groubId).SingleOrDefaultAsync();
            if (groub is not null)
            {
                var groubUser = await Context.GroubUsers.Where(gu => gu.User == user && gu.Groub == groub).SingleOrDefaultAsync();
                if (groubUser is not null)
                {
                    Context.GroubUsers.Remove(groubUser);
                    await Context.SaveChangesAsync();
                }
                else throw new AppException("user is aleardy has left this groub");
            }
            else throw new KeyNotFoundException("Groub Not Found");
        }
        #endregion

        #region Get
        public async Task<IEnumerable<GroubDto>> GetAllGroubs()
        {
            List<GroubDto> groubs = await Context.Groubs.Select(groub => new GroubDto()
            {
                Id = groub.Id,
                Name = groub.Name,
                Description = groub.Description,
                CoverUrl = groub.CoverUrl,
            }).ToListAsync();
            return groubs;
        }
        public async Task<GroubInfoDto> GetGroub(Guid Id)
        {
            GroubInfoDto? groub = await Context.Groubs.Select(groub => new GroubInfoDto()
            {
                Id = groub.Id,
                Name = groub.Name,
                Description = groub.Description,
                CoverUrl = groub.CoverUrl,
                Users = groub.Users.Select(user => new UserDto()
                {
                    Id = Guid.Parse(user.User.Id),
                    NickName = user.User.NickName,
                    PhotoUrl = user.User.PhotoUrl
                }).ToList(),
                Blogs = groub.Blogs.Select(blog => new BlogDto()
                {
                    Id = blog.Blog.Id,
                    Content = blog.Blog.Content,
                    ImageUrl = blog.Blog.ImageUrl!,
                    CreationDate = blog.Blog.CreationDate,
                    LikesCount = blog.Blog.UserLikes.Count,
                    User = new UserBlogDto()
                    {
                        Id = Guid.Parse(blog.Blog.User.Id),
                        NickName = blog.Blog.User.NickName,
                        PhotoUrl = blog.Blog.User.PhotoUrl,
                    },
                    Comments = blog.Blog.BlogComments.Select(comment => new CommentDto()
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        UserId = Guid.Parse(comment.User.Id)
                    }).ToList(),
                }).ToList()
            }).SingleOrDefaultAsync();
            return groub is not null ? groub : throw new KeyNotFoundException("Groub Not Found");
        }
        public async Task<IEnumerable<GroubUserDto>> GetUserGroubs(Guid Id)
        {
            var groubs = await Context.GroubUsers.Where(user => user.User.Id.Equals(Id)).Select(groub => new GroubUserDto()
            {
                Id = groub.Groub.Id,
                Name = groub.Groub.Name,
                Description = groub.Groub.Description,
                CoverUrl = groub.Groub.CoverUrl,
            }).ToListAsync();
            return groubs;
        }
        #endregion

        #region Remove
        public async Task<bool> RemoveGroub(Guid Id)
        {
            int groub = await Context.Groubs.Where(groub => groub.Id == Id).ExecuteDeleteAsync();
            if (groub is not 0)
            {
                await Context.SaveChangesAsync();
                return true;
            }
            else throw new KeyNotFoundException("Groub Not Found");
        }
        #endregion

    }
}
