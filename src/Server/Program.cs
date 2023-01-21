using Microsoft.AspNetCore.ResponseCompression;

using Carter;
using MediatR;
using REA.Accounting.Application;
using REA.Accounting.Presentation;
using REA.Accounting.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(ApplicationAssembly.Instance);
builder.Services.AddCarter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services from namespace Server.Extensions to the container.
builder.Services.ConfigureCors();
builder.Services.AddInfrastructureServices();
builder.Services.ConfigureEfCoreDbContext(builder.Configuration);
builder.Services.ConfigureDapper(builder.Configuration);
builder.Services.AddRepositoryServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapCarter();

app.Run();

public partial class Program { }
