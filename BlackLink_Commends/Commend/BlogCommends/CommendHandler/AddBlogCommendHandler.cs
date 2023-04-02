using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using BlackLink_SharedKernal.Enum.File;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommends.CommendHandler;

public class AddBlogCommendHandler : IRequestHandler<AddBlogCommend, Blog>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public AddBlogCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;
    }
    public async Task<Blog> Handle(AddBlogCommend request, CancellationToken cancellationToken)
    {
        try
        {
            User user = await _mediator.Send(new GetCurrentUserQuery());
            Blog blog = new()
            {
                Content = request.content,
                User = user,
            };
            if (request.file is not null)
                blog.ImageUrl = await FileManagment.SaveFile(request.file, FileType.Blogs);
            await Context.Blogs.AddAsync(blog, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return blog;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
