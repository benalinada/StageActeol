using Application;
using Application.Common.Interfaces;
using AutoMapper;
using Infrastructure.Persistance;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("ApiContextConnection");

builder.Services.AddDbContext<UsersDBContext>(options =>
              options.UseSqlServer(connectionString, option => option.EnableRetryOnFailure()),
              ServiceLifetime.Transient,
              ServiceLifetime.Transient);
builder.Services.AddTransient<IUsersDBContext>(provider => provider.GetService<UsersDBContext>());
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
builder.Services.AddApplication();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
