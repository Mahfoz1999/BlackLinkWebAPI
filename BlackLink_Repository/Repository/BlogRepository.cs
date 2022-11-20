using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Blog;
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using BlackLink_Repository.Exceptions;
using BlackLink_Repository.IRepository;
using BlackLink_Repository.Util;
using BlackLink_SharedKernal.Enum.File;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Repository.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlackLinkDbContext Context;
        private readonly IUserRepository userRepository;
        public BlogRepository(BlackLinkDbContext context, IUserRepository userRepository)
        {
            Context = context;
            this.userRepository = userRepository;
        }
        #region Actions
        public async Task<BlogFormDto> CreateBlog(BlogFormDto formDto)
        {
            var user = await userRepository.GetCurrentUser();
            var blog = new Blog()
            {
                Content = formDto.Content,
                User = user,
            };
            if (formDto.ImageFile is not null)
                blog.ImageUrl = await FileManagment.SaveFile(FileType.Blogs, formDto.ImageFile);
            var categories = await Context.Categories.Where(cat => formDto.CategoryIds.Contains(cat.Id)).ToListAsync();
            if (categories is not null)
            {
                foreach (var category in categories)
                {
                    await Context.CategoryEntityRealteds.AddAsync(new CategoryEntityRealted() { Blog = blog, Category = category });
                }
            }
            else throw new KeyNotFoundException("Category Not Found");
            await Context.Blogs.AddAsync(blog);
            await Context.SaveChangesAsync();
            formDto.Id = blog.Id;
            return formDto;
        }
        public async Task<BlogFormDto> UpdateBlog(BlogFormDto formDto)
        {
            var user = await userRepository.GetCurrentUser();
            var blog = await Context.Blogs.
                Include(blog => blog.User)
                .Include(blog => blog.CategoryEntityRealteds)
                .Where(blog => blog.Id == formDto.Id).SingleOrDefaultAsync();
            if (blog is not null)
            {
                if (blog.User == user)
                {
                    if (formDto.ImageFile is not null)
                    {
                        if (blog.ImageUrl is not null)
                            FileManagment.DeleteFile(blog.ImageUrl);
                        blog.ImageUrl = await FileManagment.SaveFile(FileType.Blogs, formDto.ImageFile);
                    }
                    blog.Content = formDto.Content;
                    if (formDto.CategoryIds.Count != 0)
                    {
                        blog.CategoryEntityRealteds.Clear();
                        var categories = await Context.Categories.Where(cat => formDto.CategoryIds.Contains(cat.Id)).ToListAsync();
                        if (categories is not null)
                        {
                            List<CategoryEntityRealted> categoryEntityRealteds = new();
                            foreach (Category category in categories)
                            {
                                CategoryEntityRealted categoryEntityRealted = new()
                                {
                                    Blog = blog,
                                    Category = category
                                };
                                categoryEntityRealteds.Add(categoryEntityRealted);
                            }
                            blog.CategoryEntityRealteds = categoryEntityRealteds;
                        }
                        else throw new KeyNotFoundException("Category Not Found");
                    }
                    await Context.SaveChangesAsync();
                    return formDto;
                }
                else throw new UnauthorizedAccessException();
            }
            else throw new KeyNotFoundException("Blog Not Found");
        }
        public async Task<bool> LikeBlog(Guid blogId)
        {
            var user = await userRepository.GetCurrentUser();
            var blog = await Context.Blogs.Include(blog => blog.User).Where(blog => blog.Id == blogId).SingleOrDefaultAsync();
            if (blog is null)
                throw new KeyNotFoundException("Blog Not Found");
            var likeUser = await Context.UserLikes.Where(like => like.User == user && like.Blog == blog).SingleOrDefaultAsync();
            if (likeUser is null)
            {
                var like = new UserLike()
                {
                    User = user,
                    Blog = blog
                };
                await Context.UserLikes.AddAsync(like);
                await Context.SaveChangesAsync();
                return true;
            }
            else throw new AppException("User Aleardy Like this blog");
        }
        public async Task<bool> UnLikeBlog(Guid blogId)
        {
            var user = await userRepository.GetCurrentUser();
            var blog = await Context.Blogs.Include(blog => blog.User).Where(blog => blog.Id == blogId).SingleOrDefaultAsync();
            if (blog is null)
                throw new KeyNotFoundException("Blog Not Found");
            var likeUser = await Context.UserLikes.Where(like => like.User == user && like.Blog == blog).SingleOrDefaultAsync();
            if (likeUser is not null)
            {
                Context.UserLikes.Remove(likeUser);
                await Context.SaveChangesAsync();
                return true;
            }
            else throw new AppException("User Aleardy UnLike this blog");
        }
        #endregion

        #region Get
        public async Task<IEnumerable<BlogDto>> GetAllBlogs()
        {
            var blogs = await Context.Blogs.Select(blog => new BlogDto()
            {
                Id = blog.Id,
                Content = blog.Content,
                CreationDate = blog.CreationDate,
                ImageUrl = blog.ImageUrl!,
                LikesCount = blog.UserLikes.Count,
                UsersLikes = blog.UserLikes.Select(like => like.User.NickName).ToList(),
                Categories = blog.CategoryEntityRealteds.Select(cat => cat.Category.Name).ToList(),
                User = new UserBlogDto()
                {
                    Id = Guid.Parse(blog.User.Id),
                    NickName = blog.User.NickName,
                    PhotoUrl = blog.User.PhotoUrl,
                },
                Comments = blog.BlogComments.Select(comment => new CommentDto()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    UserId = Guid.Parse(comment.User.Id)
                }).ToList()
            }).ToListAsync();
            return blogs;
        }

        public async Task<BlogDto> GetBlog(Guid blogId)
        {
            var blog = await Context.Blogs.Where(blog => blog.Id == blogId).Select(blog => new BlogDto()
            {
                Id = blog.Id,
                Content = blog.Content,
                CreationDate = blog.CreationDate,
                ImageUrl = blog.ImageUrl!,
                LikesCount = blog.UserLikes.Count,
                UsersLikes = blog.UserLikes.Select(like => like.User.NickName).ToList(),
                Categories = blog.CategoryEntityRealteds.Select(cat => cat.Category.Name).ToList(),
                User = new UserBlogDto()
                {
                    Id = Guid.Parse(blog.User.Id),
                    NickName = blog.User.NickName,
                    PhotoUrl = blog.User.PhotoUrl,
                },
                Comments = blog.BlogComments.Select(comment => new CommentDto()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    UserId = Guid.Parse(comment.User.Id)
                }).ToList()
            }).SingleOrDefaultAsync();
            return blog is not null ? blog : throw new KeyNotFoundException("Blog Not Found");
        }
        #endregion

        #region Remove
        public async Task<bool> RemoveBlog(Guid blogId)
        {
            User? user = await userRepository.GetCurrentUser();
            int blog = await Context.Blogs.Include(blog => blog.User).Where(blog => blog.Id.Equals(blogId) && blog.User == user).ExecuteDeleteAsync();
            if (blog is not 0)
            {
                await Context.SaveChangesAsync();
                return true;
            }
            else throw new KeyNotFoundException("Blog Not Found");
        }

        #endregion
    }
}
