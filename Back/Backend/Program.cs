using Application;
using Application.Common.Interfaces;
using AutoMapper;
using Infrastructure.Persistance;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("ApiContextConnection");
var connectioncubeString = builder.Configuration.GetConnectionString("ApiContextConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
              options.UseSqlServer(connectionString, option => option.EnableRetryOnFailure()),
              ServiceLifetime.Transient,
              ServiceLifetime.Transient);
builder.Services.AddDbContext<ApplicationDBContext>(options =>
              options.UseSqlServer(connectioncubeString, option => option.EnableRetryOnFailure()),
              ServiceLifetime.Transient,
              ServiceLifetime.Transient);
builder.Services.AddTransient<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
builder.Services.AddScoped<IDateTime, DateTimeService>();
builder.Services.AddApplication();
builder.Services.AddOptions();



//configure the identiyserver4

builder.Services.AddTransient<IDateTime, DateTimeService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

// adds an authorization policy to make sure the token is for scope 'api1'
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "https://localhost:4200");
                      });
});
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();
app.Run();
