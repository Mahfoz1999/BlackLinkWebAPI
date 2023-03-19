using BlackLink_API.Middleware;
using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Mail;
using BlackLink_Services.AuthenticationService;
using BlackLink_Services.BlogService;
using BlackLink_Services.CategoryService;
using BlackLink_Services.InterestService;
using BlackLink_Services.MailService;
using BlackLink_Services.StoryService;
using BlackLink_Web_API.Util;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Assembly assembly = Assembly.GetExecutingAssembly();


builder.Services.AddControllers();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.TryAddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.TryAddScoped<IInterestService, InterestService>();
builder.Services.TryAddScoped<IBlogService, BlogService>();
builder.Services.TryAddScoped<IStoryService, StoryService>();
builder.Services.TryAddScoped<ICategoryService, CategoryService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
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
       options.UseSqlServer(connectionString);
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
