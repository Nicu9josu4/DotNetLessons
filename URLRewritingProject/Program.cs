using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
var options = new RewriteOptions()
    .AddRedirect("home[/]?$", "home/index")
    .AddRedirect("(.*)/$", "$1"); // "(.*)/$" - Oricare adresa "$1" - Prima grupa
app.UseRewriter(options);

app.MapGet("/", async context => await context.Response.WriteAsync("HelloWorld from home"));
app.MapGet("/home", async context => await context.Response.WriteAsync("HelloWorld from just home"));
app.MapGet("/home/index", async context => await context.Response.WriteAsync("HelloWorld from homeIndex"));

app.Run();

