using EntityFrameworkDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
//builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString("SqlConnection")));

//builder.Services.AddDbContext<ApplicationContext>(options =>
//);
builder.Services.AddDbContext<ApplicationContext>(options =>
    options
    //.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
    //.UseMySQL(builder.Configuration.GetConnectionString("SqlConnection"))
    .UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"))
);


//builder.Services.AddDbContext<ApplicationContext>();
//builder.Services.AddControllers();
//builder.Services.AddMvc();
// Add services to the container.

var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.

//app.MapGet("/", async context =>
//{
//    var dbContext = context.RequestServices.GetService<ApplicationContext>();
//    var vacancies = await dbContext?.Vacancies.ToListAsync();
//    await context.Response.WriteAsJsonAsync(vacancies);
//});

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
    db.USERS.Add(new EntityFrameworkDB.Models.Users
    {  // Adaugarea unei date in baza de date
        USERNAME = "Ion",
        PASSWORD = "pass",
        FIRST_NAME = "Ion",
        LAST_NAME = "Josu",
        EMAIL = "Ion@mail.ru",
        STARTDATA = DateTime.Now,
        ENDDATA = DateTime.Now,
        ROLEID = 1,
    });
    db.SaveChanges();
    context.Response.Redirect("/");
});
app.MapGet("/DeleteUser/{name}", (string name, ApplicationContext db, HttpContext context) =>
{
    var users = db.USERS.ToList();
    var entityToDelete = users.Find(user => user.USERNAME == name);
    db.USERS.Remove(entityToDelete);
    db.SaveChanges();
    context.Response.Redirect("/");
});

app.MapGet("/", (ApplicationContext db) =>
{
    var users = db.USERS.ToList();
    var text = "";
    foreach (var user in users)
    {
        text += $"{user.USERNAME} - {user.EMAIL} \n";
    }
    return Results.Text(text);
});
app.Run();
