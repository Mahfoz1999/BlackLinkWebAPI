using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.GroubCommends.Commend;

public record AddGroubCommend(string Name, string Description, User User) : IRequest<Groub>;
public record UpdateGroubCommend(Guid Id, string Name, string Description, User User) : IRequest<Groub>;
public record RemoveGroubCommend(Guid Id, User User) : IRequest<Groub>;
