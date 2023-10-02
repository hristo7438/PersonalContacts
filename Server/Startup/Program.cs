using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseCors(options => options
	.AllowAnyOrigin()
	.AllowAnyHeader()
	.AllowAnyMethod());

app.UseStaticFiles();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces