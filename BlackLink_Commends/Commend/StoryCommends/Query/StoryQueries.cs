using BlackLink_Models.Models;
using MediatR;

namespace BlackLink_Commends.Commend.StoryCommends.Query;

public record GetAllStoriesQuery() : IRequest<IEnumerable<Story>>;
public record GetStoryByIdQuery(Guid Id) : IRequest<Story>;

