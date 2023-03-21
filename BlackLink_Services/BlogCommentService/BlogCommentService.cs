using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_Commends.Commend.BlogCommentCommends.Query;
using BlackLink_DTO.BlogComment;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Services.BlogCommentService;

public class BlogCommentService : IBlogCommentService
{
    private readonly IMediator _mediator;
    public BlogCommentService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<AddBlogCommentCommend> AddBlogComment(AddBlogCommentCommend commend)
    {
        await _mediator.Send(commend);
        return commend;
    }

    public async Task<IEnumerable<BlogCommentDto>> GetAllBlogComments(Guid blogId)
    {
        IEnumerable<BlogComment> blogComments = await _mediator.Send(new GetAllBlogCommentsQuery(blogId));
        IEnumerable<BlogCommentDto> blogCommentDtos = blogComments.Select(e => new BlogCommentDto()
        {
            Id = e.Id,
            Content = e.Content,
            CreationDate = e.CreationDate,
            User = new()
            {
                Id = Guid.Parse(e.User.Id),
                NickName = e.User.NickName,
                PhotoUrl = e.User.UserPhotos.Select(c => c.PhotoUrl).FirstOrDefault()!
            }
        }).ToList();
        return blogCommentDtos;
    }

    public async Task<BlogCommentDto> GetBlogComment(Guid Id)
    {
        BlogComment blogComment = await _mediator.Send(new GetBlogCommentByIdQuery(Id));
        return new BlogCommentDto()
        {
            Id = blogComment.Id,
            Content = blogComment.Content,
            CreationDate = blogComment.CreationDate,
            User = new()
            {
                Id = Guid.Parse(blogComment.User.Id),
                NickName = blogComment.User.NickName,
                PhotoUrl = blogComment.User.UserPhotos.Select(c => c.PhotoUrl).FirstOrDefault()!
            }
        };
    }

    public async Task RemoveBlogComment(Guid Id)
    {
        await _mediator.Send(new RemoveBlogCommentCommend(Id));
    }

    public async Task UpdateBlogComment(UpdateBlogCommentCommend commend)
    {
        await _mediator.Send(commend);
    }
}
