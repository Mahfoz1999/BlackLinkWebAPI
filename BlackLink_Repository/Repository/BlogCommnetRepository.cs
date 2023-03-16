using BlackLink_Database.SQLConnection;
using BlackLink_DTO.BlogComment;
using BlackLink_Models.Models;
using BlackLink_Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Repository.Repository;

public class BlogCommnetRepository : IBlogCommnetRepository
{
    private readonly BlackLinkDbContext Context;
    private readonly IUserRepository userRepository;
    public BlogCommnetRepository(BlackLinkDbContext Context, IUserRepository userRepository)
    {
        this.Context = Context;
        this.userRepository = userRepository;
    }
    public async Task<BlogCommentFormDto> AddBlogComment(BlogCommentFormDto formDto)
    {
        var user = await userRepository.GetCurrentUser();
        var blog = await Context.Blogs.Where(blog => blog.Id == formDto.BlogId).SingleOrDefaultAsync();
        if (blog is not null)
        {
            var blogComment = new BlogComment()
            {
                Content = formDto.Content,
                Blog = blog,
                User = user
            };
            await Context.BlogComments.AddAsync(blogComment);
            await Context.SaveChangesAsync();
            formDto.Id = blogComment.Id;
            return formDto;
        }
        else throw new KeyNotFoundException("Blog not found");
    }
    public async Task<BlogCommentFormDto> UpdateBlogComment(BlogCommentFormDto formDto)
    {
        var user = await userRepository.GetCurrentUser();
        var blog = await Context.Blogs.Where(blog => blog.Id == formDto.BlogId).SingleOrDefaultAsync();
        if (blog is not null)
        {
            await Context.BlogComments.Where(com => com.Id == formDto.Id && com.User == user)
                .ExecuteUpdateAsync(bc =>
                bc.SetProperty(c => c.Content, formDto.Content).
                SetProperty(c => c.User, user).
                SetProperty(c => c.Blog, blog));
            await Context.SaveChangesAsync();
            return formDto;
        }
        else throw new KeyNotFoundException("Blog not found");
    }
    public async Task<IEnumerable<BlogCommentDto>> GetAllBlogComment(Guid blogId)
    {
        Blog? blog = await Context.Blogs.SingleOrDefaultAsync(blog => blog.Id == blogId);
        if (blog is not null)
        {
            var comments = await Context.BlogComments.Where(blog => blog.Blog.Id == blogId).Select(blog => new BlogCommentDto()
            {
                Id = blog.Id,
                Content = blog.Content,
                CreationDate = blog.CreationDate,
                UserId = Guid.Parse(blog.User.Id)
            }).ToListAsync();
            return comments;
        }
        else throw new KeyNotFoundException("Blog not found");
    }

    public async Task<BlogCommentDto> GetBlogComment(Guid Id)
    {
        var comment = await Context.BlogComments.Where(blog => blog.Id == Id).Select(blog =>
        new BlogCommentDto()
        {
            Id = blog.Id,
            Content = blog.Content,
            CreationDate = blog.CreationDate,
            UserId = Guid.Parse(blog.User.Id)
        }).SingleOrDefaultAsync();
        return comment is not null ? comment : throw new KeyNotFoundException("Comment Not Found");
    }

    public async Task<bool> RemoveBlogComment(Guid Id)
    {
        var user = await userRepository.GetCurrentUser();
        var comment = await Context.BlogComments.Where(blog => blog.Id == Id && blog.User == user).ExecuteDeleteAsync();
        if (comment is 0)
            throw new KeyNotFoundException("Comment Not Found");
        else
        {
            await Context.SaveChangesAsync();
            return true;
        }

    }


}
