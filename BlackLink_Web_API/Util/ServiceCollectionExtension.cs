using BlackLink_Commends.Commend.CategoryCommends.Commend;
using BlackLink_Commends.Commend.CategoryCommends.CommendHandler;
using BlackLink_Commends.Commend.CategoryCommends.Query;
using BlackLink_Commends.Commend.CategoryCommends.QueryHandler;
using BlackLink_Commends.Commend.InterestCommend.Commend;
using BlackLink_Commends.Commend.InterestCommend.CommendHandler;
using BlackLink_Commends.Commend.InterestCommend.Query;
using BlackLink_Commends.Commend.InterestCommend.QueryHandler;
using BlackLink_Database.SQLConnection;
using BlackLink_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
        _ = services.AddResponseCaching();
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(assembly);
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
        _ = services.AddAuthentication(opt =>
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
        _ = services.AddTransient<IRequestHandler<GetCategoryByIdQuery, Category>, GetCategoryByIdQueryHandler>();
        _ = services.AddTransient<IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>, GetAllCategoriesQueryHandler>();
        _ = services.AddTransient<IRequestHandler<AddCategoryCommend, Category>, AddCategoryCommendHandler>();
        _ = services.AddTransient<IRequestHandler<UpdateCategoryCommend, Category>, UpdateCategoryCommendHandler>();
        _ = services.AddTransient<IRequestHandler<RemoveCategoryCommend, Category>, RemoveCategoryCommendHandler>();

        _ = services.AddTransient<IRequestHandler<GetInterestByIdQuery, Interest>, GetInterestByIdQueryHandler>();
        _ = services.AddTransient<IRequestHandler<GetAllInterestsQuery, IEnumerable<Interest>>, GetAllInterestsQueryHandler>();
        _ = services.AddTransient<IRequestHandler<AddInterestCommend, Interest>, AddInterestCommendHandler>();
        _ = services.AddTransient<IRequestHandler<UpdateInterestCommend, Interest>, UpdateInterestCommendHandler>();
        _ = services.AddTransient<IRequestHandler<RemoveInterestCommend, Interest>, RemoveInterestCommendHandler>();
    }
}
