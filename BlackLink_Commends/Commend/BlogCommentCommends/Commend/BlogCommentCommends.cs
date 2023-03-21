using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommentCommends.Commend;

public record AddBlogCommentCommend(string Content, Guid BlogId) : IRequest<BlogComment>;
public record UpdateBlogCommentCommend(Guid Id, string Content) : IRequest;
public record RemoveBlogCommentCommend(Guid Id) : IRequest;
