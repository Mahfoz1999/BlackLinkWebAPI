using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlackLink_Commends.Commend.BlogCommends.Commend;

public record AddBlogCommend(string content, Category category, User User, IFormFile? file) : IRequest<Blog>;
public record UpdateBlogCommend(Guid Id, string content, Category category, IFormFile? file) : IRequest<Blog>;
public record RemoveBlogCommend(Guid Id) : IRequest<Blog>;
