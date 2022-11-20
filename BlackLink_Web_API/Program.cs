using BlackLink_API.Middleware;
using BlackLink_API.Util;
using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Mail;
using BlackLink_IRepository.IRepository.Authentication;
using BlackLink_MailService.IServices;
using BlackLink_MailService.Services;
using BlackLink_Repository.IRepository;
using BlackLink_Repository.Repository;
using BlackLink_Repository.Repository.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.TryAddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.TryAddScoped<IUserRepository, UserRepository>();
builder.Services.TryAddScoped<IInterestRepository, InterestRepository>();
builder.Services.TryAddScoped<IBlogRepository, BlogRepository>();
builder.Services.TryAddScoped<IBlogCommnetRepository, BlogCommnetRepository>();
builder.Services.TryAddScoped<IStoryRepository, StoryRepository>();
builder.Services.TryAddScoped<ISocialRepository, SocialRepository>();
builder.Services.TryAddScoped<IGroubRepository, GroubRepository>();
builder.Services.TryAddScoped<ICategoryRepository, CategoryRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services
   .AddDbContext<BlackLinkDbContext>(options =>
   {
       var connectionString = builder.Configuration.GetConnectionString("DefaultSQL");
       options.UseSqlServer(connectionString);
   });

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(1));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureControllers();
var app = builder.Build();
app.UseCors(cors => cors
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
