using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Forum.API.Infrastructure.Extensions;
using Forum.API.Infrastructure.Mappings;
using Forum.Persistence;
using Forum.API.Infrastructure.Middlewares;
using ExceptionHandlerMiddleware = Forum.API.Infrastructure.Middlewares.ExceptionHandlerMiddleware;
using Forum.API.Infrastructure.Auth.JWT;
using BackGroundServices.BackGroundWorkers;
using System.Security.Claims;
using Forum.Application.MainUsers;
using Forum.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Forum.Domain.user;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = ".",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Name = "Bearer"
                            },
                            new string[] {}
                    }
                });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Forum API",
        Version = "v1",
        Description = "Api to manage Forum",

    });
    options.ExampleFilters();
});



builder.Services.AddTokenAuth(builder.Configuration.GetSection(nameof(JWTConfiguration)).GetSection(nameof(JWTConfiguration.Secret)).Value);


builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("User", policy =>
        policy.RequireRole("User")
  
    );

    options.AddPolicy("Administrator", policy =>
    policy.RequireRole("Administrator")
    );

    options.AddPolicy("GuestOnly", policy =>
        policy.RequireAssertion(context =>
            !context.User.Identity!.IsAuthenticated));

});

builder.Services.AddServices();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));



builder.Services.AddDbContext<ForumManagementIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));

builder.Services.AddIdentity<User, IdentityRole<int>>(option =>
    {
        option.Password.RequiredLength = 6;
        option.Password.RequireUppercase = true;
    }

).AddEntityFrameworkStores<ForumManagementIdentityContext>()
.AddDefaultTokenProviders();


builder.Services.RegisterMaps();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddHostedService<TopicWorker>();

var app = builder.Build();

await Forum.Persistence.Seed.ForumManagementSeed.SeedUsersAndRolesAsync(app.Services);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Forum API");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<CultureMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
