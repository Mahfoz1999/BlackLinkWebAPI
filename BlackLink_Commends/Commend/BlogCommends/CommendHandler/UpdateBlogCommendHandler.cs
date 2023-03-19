using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Commend.CategoryCommends.Query;
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
    private readonly IMediator _mediator;
    public UpdateBlogCommendHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        _mediator = mediator;

    }
    public async Task<Blog> Handle(UpdateBlogCommend request, CancellationToken cancellationToken)
    {
        Blog? blog = await Context.Blogs.FindAsync(request.Id);
        GetCategoryByIdQuery cateogrycommend = new(Id: request.categoryId);
        Category category = await _mediator.Send(cateogrycommend);
        if (blog != null)
        {
            blog.Content = request.content;
            blog.Category = category;
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
