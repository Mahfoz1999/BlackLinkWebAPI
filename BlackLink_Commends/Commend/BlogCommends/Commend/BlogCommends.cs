using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlackLink_Commends.Commend.BlogCommends.Commend;

public record AddBlogCommend(string content, Guid categoryId, IFormFile? file) : IRequest<Blog>;
public record UpdateBlogCommend(Guid Id, string content, Guid categoryId, IFormFile? file) : IRequest<Blog>;
public record RemoveBlogCommend(Guid Id) : IRequest<Blog>;
