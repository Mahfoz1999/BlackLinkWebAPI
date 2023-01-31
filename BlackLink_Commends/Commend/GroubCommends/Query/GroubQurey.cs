using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.GroubCommends.Query;

public record GetGroubQuery(Guid Id) : IRequest<Groub>;
public record GetAllGroubsQuery() : IRequest<IEnumerable<Groub>>;
