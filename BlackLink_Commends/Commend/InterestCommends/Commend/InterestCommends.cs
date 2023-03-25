using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.InterestCommends.Commend;

public record AddInterestCommend(string Name) : IRequest<Interest>;
public record UpdateInterestCommend(Guid Id, string Name) : IRequest;
public record RemoveInterestCommend(Guid Id) : IRequest;