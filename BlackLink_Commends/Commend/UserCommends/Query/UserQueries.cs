using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.UserCommends.Query;

public record GetCurrentUserQuery() : IRequest<User>;
