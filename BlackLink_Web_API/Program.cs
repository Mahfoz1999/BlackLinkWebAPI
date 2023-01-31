using BlackLink_API.Middleware;
using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Mail;
using BlackLink_IRepository.IRepository.Authentication;
using BlackLink_Repository.IRepository;
using BlackLink_Repository.Repository;
using BlackLink_Repository.Repository.Authentication;
using BlackLink_Services.CategoryService;
using BlackLink_Services.InterestService;
using BlackLink_Services.MailService;
using BlackLink_Web_API.Util;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.TryAddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.TryAddScoped<IUserRepository, UserRepository>();
builder.Services.TryAddScoped<IInterestService, InterestService>();
builder.Services.TryAddScoped<IBlogRepository, BlogRepository>();
builder.Services.TryAddScoped<IBlogCommnetRepository, BlogCommnetRepository>();
builder.Services.TryAddScoped<IStoryRepository, StoryRepository>();
builder.Services.TryAddScoped<ISocialRepository, SocialRepository>();
builder.Services.TryAddScoped<IGroubRepository, GroubRepository>();
builder.Services.TryAddScoped<ICategoryService, CategoryService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<Mediator>();
builder.Services.AddCommendTransients();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services
   .AddDbContext<BlackLinkDbContext>(options =>
   {
       string? connectionString = builder.Configuration.GetConnectionString("DefaultSQL");
       _ = options.UseSqlServer(connectionString);
   });

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(1));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureControllers();
WebApplication app = builder.Build();
app.UseCors(cors => cors
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
