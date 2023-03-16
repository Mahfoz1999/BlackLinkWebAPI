using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommends.CommendHandler;

public class RemoveBlogCommendHandler : IRequestHandler<RemoveBlogCommend, Blog>
{
    private readonly BlackLinkDbContext Context;
    public RemoveBlogCommendHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<Blog> Handle(RemoveBlogCommend request, CancellationToken cancellationToken)
    {
        Blog? blog = await Context.Blogs.FindAsync(request.Id);
        if (blog != null)
        {
            FileManagment.DeleteFile(blog.ImageUrl!);
            Context.Blogs.Remove(blog);
            await Context.SaveChangesAsync(cancellationToken);
            return blog;
        }
        else throw new NotFoundException("Blog Not Found");
    }
}
