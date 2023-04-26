using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
var options = new RewriteOptions()
    .AddRedirect("home[/]?$", "home/index")
    .AddRedirect("(.*)/$", "$1"); // "(.*)/$" - Oricare adresa "$1" - Prima grupa
    //.AddRewrite("home/index", "home/about", skipRemainingRules: false);

app.UseRewriter(options);

app.MapGet("/", async context => await context.Response.WriteAsync("HelloWorld from root page"));
app.MapGet("/home", async context => await context.Response.WriteAsync("HelloWorld from home"));
app.MapGet("/home/index", async context => await context.Response.WriteAsync("HelloWorld from homeIndex"));
app.MapGet("/home/about", async context => await context.Response.WriteAsync("HelloWorld from homeAbout"));

app.Run();

