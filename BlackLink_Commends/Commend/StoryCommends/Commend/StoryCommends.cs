using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlackLink_Commends.Commend.StoryCommends.Commend;

public record AddStoryCommend(string content, IFormFile? file) : IRequest<Story>;
public record UpdateStoryCommend(Guid Id, string content, IFormFile? file) : IRequest<Story>;
public record RemoveStoryCommend(Guid Id) : IRequest<Story>;
