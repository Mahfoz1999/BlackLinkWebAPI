using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommend.Commend;

public record AddInterestCommend(string Name) : IRequest<Interest>;
public record UpdateInterestCommend(Guid Id, string Name) : IRequest<Interest>;
public record RemoveInterestCommend(Guid Id) : IRequest<Interest>;