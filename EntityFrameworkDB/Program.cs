using EntityFrameworkDB;
using EntityFrameworkDB.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options
    //.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
    //.UseMySQL(builder.Configuration.GetConnectionString("SqlConnection"))
    .UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.

//app.MapGet("/", (ApplicationContext db) =>
//{
//    var users = db.user.ToList();
//    //db.user.Add(new EntityFrameworkDB.Models.User { idUser = 1, Token = "token001" });
//    var text = "";
//    foreach (var user in users)
//    {
//        text += $"{user.idUser} - {user.Token} \n";

//    }
//    //foreach (var vac in vacancies)
//    //{
//    //    text += $"{vac.Title} - {vac.Description} - {vac.StartDate} - {vac.EndDate} \n";
//    //}
//    return text;
//});

app.MapGet("/AddUser", (ApplicationContext db, HttpContext context) =>
{
    db.USERS.Add(new Users
    {  // Adaugarea unei date in baza de date
        Username = "Ion",
        Password = "pass",
        FirstName = "Ion",
        LastName = "Josu",
        Email = "Ion@mail.ru",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now,
        RoleId = 1,
    });
    //db.users.Add(new User
    //{
    //    Token = "Token"
    //});
    db.SaveChanges();
    context.Response.Redirect("/");
});
app.MapGet("/DeleteUser/{name}", (string name, ApplicationContext db, HttpContext context) =>
{
    var users = db.USERS.ToList();
    var entityToDelete = users.Find(user => user.Username == name);
    db.USERS.Remove(entityToDelete);
    db.SaveChanges();
    context.Response.Redirect("/");
});

app.MapGet("/", (ApplicationContext db) =>
{
    var users = db.USERS.ToList();
    //var users = db.users.ToList();
    var text = "";

    //foreach (var user in users)
    //{
    //    text += $"{user.idUser} - {user.Token} \n";
    //}
    foreach (var user in users)
    {
        text += $"{user.Username} - {user.Email} \n";
    }
    return Results.Text(text);
});
app.Run();