using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using BlackLink_SharedKernal.Enum.File;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommends.CommendHandler;

public class UpdateBlogCommendHandler : IRequestHandler<UpdateBlogCommend>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator _mediator;
    public UpdateBlogCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;

    }
    public async Task Handle(UpdateBlogCommend request, CancellationToken cancellationToken)
    {
        Blog? blog = await Context.Blogs.FindAsync(request.Id);
        if (blog != null)
        {
            blog.Content = request.content;
            if (request.file is not null)
            {
                FileManagment.DeleteFile(blog.ImageUrl!);
                blog.ImageUrl = await FileManagment.SaveFile(request.file, FileType.Blogs);
            }
            Context.Blogs.Update(blog);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else throw new NotFoundException("Blog Not Found");
    }
}
