using Blog_Project.CORE.@interface;
using Blog_Project.EF.RepositoryPattern;
using eCommerce.Core.Data;
using eCommerce.Core.entities.identity;
using eCommerce.Core.Interface;
using eCommerce.Erros;
using eCommerce.Extension;
using eCommerce.Helpers;
using eCommerce.Infrastructre.Data;
using eCommerce.Infrastructre.Identity;
using eCommerce.Middelware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);



var app = builder.Build();

app.UseMiddleware<ExceptionMiddelware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();

// making scope for temp using context to seed the data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<appDBcontext>();
var identityContext = services.GetRequiredService<AppIdentityContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await SeedappDBcontext.SeedAsync(context, loggerFactory, userManager);
    await AppUserSeeds.SeedUser(userManager);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
