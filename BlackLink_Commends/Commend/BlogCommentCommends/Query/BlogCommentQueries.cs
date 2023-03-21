using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommentCommends.Query;

public record GetAllBlogCommentsQuery(Guid blogId) : IRequest<IEnumerable<BlogComment>>;
public record GetBlogCommentByIdQuery(Guid Id) : IRequest<BlogComment>;
