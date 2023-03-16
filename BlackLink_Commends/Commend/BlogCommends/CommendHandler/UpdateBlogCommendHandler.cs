using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Exceptions;
using BlackLink_Commends.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using BlackLink_SharedKernal.Enum.File;
using MediatR;

namespace BlackLink_Commends.Commend.BlogCommends.CommendHandler;

public class UpdateBlogCommendHandler : IRequestHandler<UpdateBlogCommend, Blog>
{
    private readonly BlackLinkDbContext Context;
    public UpdateBlogCommendHandler(BlackLinkDbContext Context)
    {
        this.Context = Context;
    }
    public async Task<Blog> Handle(UpdateBlogCommend request, CancellationToken cancellationToken)
    {
        Blog? blog = await Context.Blogs.FindAsync(request.Id);
        if (blog != null)
        {
            blog.Content = request.content;
            blog.Category = request.category;
            if (request.file is not null)
            {
                FileManagment.DeleteFile(blog.ImageUrl!);
                blog.ImageUrl = await FileManagment.SaveFile(request.file, FileType.Blogs);
            }
            Context.Blogs.Update(blog);
            await Context.SaveChangesAsync(cancellationToken);
            return blog;
        }
        else throw new NotFoundException("Blog Not Found");
    }
}
