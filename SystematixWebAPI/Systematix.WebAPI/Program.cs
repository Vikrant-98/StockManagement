using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Systematix.WebAPI.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Systematix.WebAPI.Repositories.EmployeeDetailsRepositories;
using Systematix.WebAPI.Repositories.UserRepositories;
using Systematix.WebAPI.Repositories.TokenHandlerRepositories;
using Systematix.WebAPI.Repositories.ClientDetailsRepositories;
using Systematix.WebAPI.Business;
using AutoMapper.Internal.Mappers;
using Systematix.WebAPI.Services.Mapping;
using Systematix.WebAPI.Repositories.StockDetailsRepositories;
using Systematix.WebAPI.Repositories.LedgerRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//To generate Token in SWagger
builder.Services.AddSwaggerGen(o => {
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id=JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    o.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});


//FLuent Validation
builder.Services.AddFluentValidation(O => O.
    RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddDbContext<SystematixDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("Conn1"));
});

builder.Services.AddScoped<IEmployeeDetailRepository, EmployeeDetailRepository>();

// Add User Repository for Static User
//builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();
// Add User Repository for dynamic User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientDetailsRepository, ClientDetailsRepository>();
builder.Services.AddScoped<IClientDetailsBusiness, ClientDetailsBusiness>();
builder.Services.AddScoped<IStockDetailsRepository, StockDetailsRepository>();
builder.Services.AddScoped<IStockDetailsBusiness, StockDetailsBusiness>();
builder.Services.AddScoped<ILedgerRepository, LedgerRepository>();
builder.Services.AddScoped<ILedgerBusiness, LedgerBusiness>();
builder.Services.AddScoped<ObjectMapper>();
builder.Services.AddScoped<ITokenHandler, Systematix.WebAPI.Repositories.TokenHandlerRepositories.TokenHandler>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

//For JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseCors(builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
