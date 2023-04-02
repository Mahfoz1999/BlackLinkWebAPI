using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Commends.Commend.UserCommends.QueryHandler;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly BlackLinkDbContext Context;
    private readonly IMediator mediator;
    public GetAllUsersQueryHandler(BlackLinkDbContext Context, IMediator mediator)
    {
        this.Context = Context;
        this.mediator = mediator;
    }
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        User currentUser = await mediator.Send(new GetCurrentUserQuery());
        return await Context.Users.Include(e => e.UserPhotos).Where(e => e.Gender != currentUser.Gender).ToListAsync();
    }
}
