using BlackLink_Commends.Commend.AuthenticationCommends.Commend;
using BlackLink_Commends.Commend.AuthenticationCommends.CommendHandler;
using BlackLink_Commends.Commend.AuthenticationCommends.Query;
using BlackLink_Commends.Commend.AuthenticationCommends.QueryHandler;
using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Commends.Commend.BlogCommends.CommendHandler;
using BlackLink_Commends.Commend.BlogCommends.Query;
using BlackLink_Commends.Commend.BlogCommends.QueryHandler;
using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_Commends.Commend.BlogCommentCommends.CommendHandler;
using BlackLink_Commends.Commend.BlogCommentCommends.Query;
using BlackLink_Commends.Commend.BlogCommentCommends.QueryHandler;
using BlackLink_Commends.Commend.InterestCommends.Commend;
using BlackLink_Commends.Commend.InterestCommends.CommendHandler;
using BlackLink_Commends.Commend.InterestCommends.Query;
using BlackLink_Commends.Commend.InterestCommends.QueryHandler;
using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_Commends.Commend.StoryCommends.CommendHandler;
using BlackLink_Commends.Commend.StoryCommends.Query;
using BlackLink_Commends.Commend.StoryCommends.QueryHandler;
using BlackLink_Commends.Commend.UserCommends.Query;
using BlackLink_Commends.Commend.UserCommends.QueryHandler;
using BlackLink_Database.SQLConnection;
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace BlackLink_Web_API.Util;

public static class ServiceCollectionExtension
{
    #region Base Settings
    public static void ConfigureControllers(this IServiceCollection services)
    {
        _ = services.AddControllers(config =>
        {
            config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
            {
                Duration = 30
            });
        });
    }
    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        IdentityBuilder builder = services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.User.RequireUniqueEmail = true;
            o.SignIn.RequireConfirmedEmail = false;
        }).AddEntityFrameworkStores<BlackLinkDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection jwtConfig = configuration.GetSection("jwtConfig");
        string? secretKey = jwtConfig["secret"];
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtConfig:validAudience"],
                    ValidIssuer = configuration["JwtConfig:validIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: configuration["JwtConfig:secret"]!))
                };
            });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        _ = services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Black Link API",
                Version = "v1",
                Description = "Black Link API Services.",
                Contact = new OpenApiContact
                {
                    Name = "Mahfoz Khalil ."
                },
            });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
        });
    }
    #endregion
    public static void AddCommendTransients(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<GetCurrentUserQuery, User>, GetCurrentUserQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<User>>, GetAllUsersQueryHandler>();

        services.AddTransient<IRequestHandler<GetTokenQuery, JwtSecurityToken>, GetTokenQueryHandler>();
        services.AddTransient<IRequestHandler<SignUpCommend, IdentityResult>, SignUpCommendHandler>();
        services.AddTransient<IRequestHandler<UpdateUserCommend, IdentityResult>, UpdateUserCommendHandler>();
        services.AddTransient<IRequestHandler<LogInCommend, TokenModel>, LogInCommendHanlder>();


        services.AddTransient<IRequestHandler<GetInterestByIdQuery, Interest>, GetInterestByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllInterestsQuery, IEnumerable<Interest>>, GetAllInterestsQueryHandler>();
        services.AddTransient<IRequestHandler<AddInterestCommend, Interest>, AddInterestCommendHandler>();
        services.AddTransient<IRequestHandler<UpdateInterestCommend>, UpdateInterestCommendHandler>();
        services.AddTransient<IRequestHandler<RemoveInterestCommend>, RemoveInterestCommendHandler>();

        services.AddTransient<IRequestHandler<GetBlogByIdQuery, Blog>, GetBlogByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllBlogsQuery, IEnumerable<Blog>>, GetAllBlogsQueryHandler>();
        services.AddTransient<IRequestHandler<AddBlogCommend, Blog>, AddBlogCommendHandler>();
        services.AddTransient<IRequestHandler<UpdateBlogCommend>, UpdateBlogCommendHandler>();
        services.AddTransient<IRequestHandler<RemoveBlogCommend>, RemoveBlogCommendHandler>();

        services.AddTransient<IRequestHandler<GetBlogCommentByIdQuery, BlogComment>, GetBlogCommentByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllBlogCommentsQuery, IEnumerable<BlogComment>>, GetAllBlogCommentsQueryHandler>();
        services.AddTransient<IRequestHandler<AddBlogCommentCommend, BlogComment>, AddBlogCommentCommendHandler>();
        services.AddTransient<IRequestHandler<UpdateBlogCommentCommend>, UpdateBlogCommentCommendHandler>();
        services.AddTransient<IRequestHandler<RemoveBlogCommentCommend>, RemoveBlogCommentCommendHandler>();

        services.AddTransient<IRequestHandler<GetStoryByIdQuery, Story>, GetStoryByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllStoriesQuery, IEnumerable<Story>>, GetAllStoriesQueryHandler>();
        services.AddTransient<IRequestHandler<AddStoryCommend, Story>, AddStoryCommendHandler>();
        services.AddTransient<IRequestHandler<UpdateStoryCommend>, UpdateStoryCommendHandler>();
        services.AddTransient<IRequestHandler<RemoveStoryCommend>, RemoveStoryCommendHandler>();
    }
}
