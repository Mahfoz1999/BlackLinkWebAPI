using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommends.Query;

public record GetInterestByIdQuery(Guid Id) : IRequest<Interest>;
public record GetAllInterestsQuery() : IRequest<IEnumerable<Interest>>;
