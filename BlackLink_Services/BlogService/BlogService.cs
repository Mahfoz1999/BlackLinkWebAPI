using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Commend.BlogCommends.Query;
using BlackLink_DTO.Blog;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Services.BlogService;

public class BlogService : IBlogService
{
    private readonly IMediator _mediator;
    public BlogService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<AddBlogCommend> AddBlog(AddBlogCommend commend)
    {
        await _mediator.Send(commend);
        return commend;
    }

    public async Task<UpdateBlogCommend> UpdateBlog(UpdateBlogCommend commend)
    {
        await _mediator.Send(commend);
        return commend;
    }
    public async Task<IEnumerable<BlogDto>> GetAllBlogs()
    {
        IEnumerable<Blog> blogs = await _mediator.Send(new GetAllBlogsQuery());
        IEnumerable<BlogDto> blogDtos = blogs.Select(e => new BlogDto()
        {
            Id = e.Id,
            Content = e.Content,
            ImageUrl = e.ImageUrl!,
            CreationDate = e.CreationDate,
            Category = new BlackLink_DTO.Category.CategoryDto()
            {
                Id = e.Category.Id,
                Name = e.Category.Name,
            },
            User = new BlackLink_DTO.User.UserBlogDto()
            {
                Id = Guid.Parse(e.User.Id),
                NickName = e.User.NickName,
                PhotoUrl = e.User.UserPhotos.Select(u => u.PhotoUrl).FirstOrDefault()!
            }
        }).ToList();
        return blogDtos;
    }

    public async Task<BlogDto> GetBlogById(Guid Id)
    {
        Blog blog = await _mediator.Send(new GetBlogByIdQuery(Id));
        BlogDto blogDto = new()
        {
            Id = blog.Id,
            Content = blog.Content,
            ImageUrl = blog.ImageUrl!,
            CreationDate = blog.CreationDate,
            Category = new BlackLink_DTO.Category.CategoryDto()
            {
                Id = blog.Category.Id,
                Name = blog.Category.Name,
            },
            User = new BlackLink_DTO.User.UserBlogDto()
            {
                Id = Guid.Parse(blog.User.Id),
                NickName = blog.User.NickName,
                PhotoUrl = blog.User.UserPhotos.Select(u => u.PhotoUrl).FirstOrDefault()!
            }
        };
        return blogDto;
    }

    public async Task RemoveBlog(Guid Id)
    {
        await _mediator.Send(new RemoveBlogCommend(Id));
    }
}
