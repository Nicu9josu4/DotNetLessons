using EntityFrameworkDB;
using EntityFrameworkDB.Model;
using EntityFrameworkDB.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContextFactory<ApplicationContext>(options =>
    options
    //.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
    //.UseMySQL(builder.Configuration.GetConnectionString("SqlConnection"))
    .UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")) // , x => x.MigrationsAssembly(nameof(EntityFrameworkDB.ApplicationContext))
);
builder.Services.AddDbContext<AdventureContext>();
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

app.MapDefaultControllerRoute();
app.MapGet("/GetMatches", (AdventureContext db) =>
{
    var match = (
    from M in db.Matches
    join T in db.Teams on M.FirstTeamId equals T.Id
    join TT in db.Teams on M.SecondTeamId equals TT.Id
    select new
    {
        FirstTeamName =  T.TeamName,
        SecondTeamName = TT.TeamName,
        M.StartTime
    }

               /*
                SELECT m.id          AS ID,
                    t.team_name   AS First_Team,
                    tt.team_name  AS Second_Team,
                    to_char(m.start_time, 'dd-MON HH24:MI')  AS StartTime
                FROM IJOSU.match m
                LEFT JOIN IJOSU.Teams t ON t.id = m.first_Team_id
                LEFT JOIN IJOSU.Teams tt ON tt.id = m.Second_Team_Id
                WHERE m.first_team_id IS NOT NULL AND m.second_team_id IS NOT NULL 

                */
               ).ToList();
    var text = "";
    match.ForEach(item => text += item + "\n");
    //foreach (var item in top)
    //{
    //    text += item.ToString();
    //}

    return text;

});

app.MapDelete("/GetPrognoses/DeletePrognose/{prognoseId}", (int prognoseId, AdventureContext db, HttpContext context) =>
{
    var prognoses = db.Prognoses.ToList();
    var prognoseToDelete = prognoses.First(prognose => prognose.Id == prognoseId);
    db.Remove(prognoseToDelete);
    db.SaveChanges();
    context.Response.Redirect("/GetPrognoses");
});

app.MapGet("/GetPrognoses/AddPrognose/{VoterId}/{MatchId}/{ScoreTeam1}/{ScoreTeam2}/{PrognosedType}", (int VoterId, int MatchId, int ScoreTeam1, int ScoreTeam2, int PrognosedType, AdventureContext db, HttpContext context) =>
{
    db.Add(
        new Prognose { 
    VoterId = VoterId,
    MatchId = MatchId,
    ScoreTeam1 = ScoreTeam1,
    ScoreTeam2 = ScoreTeam2,
    PrognosedType = PrognosedType
    });
    db.SaveChanges();
    context.Response.Redirect("/GetPrognoses");
});

app.MapGet("/GetPrognoses", (AdventureContext db) =>
{
//var top = (
//from M in db.Matches
//join Team1 in db.Teams on M.FirstTeamId equals Team1.Id
//join Team2 in db.Teams on M.SecondTeamId equals Team2.Id
//select new
//{
//    M.Id,
//    M.FirstTeamScore,
//    FirstTeamName = Team1.TeamName, // Adaugarea unui nume diferit la conoana data
//    M.SecondTeamScore,
//    SecondTeamName = Team2.TeamName,
//    M.StartTime
//}

var prognoses = (from P in db.Prognoses
                 join M in db.Matches on P.MatchId equals M.Id
                 join V in db.Voters on P.VoterId equals V.Id
                 join PP in db.Players on P.PrognosedPlayer equals PP.Id
                 join T in db.Teams on M.FirstTeamId equals T.Id
                 join TT in db.Teams on M.SecondTeamId equals TT.Id
                 join TTT in db.Teams on PP.TeamId equals TTT.Id
                 where P.VoterId == 4
                 orderby P.PrognosedDate descending
                 select new
                 {
                     P.VoterId,
                     P.ScoreTeam1,
                     FirstTeamName = T.TeamName,
                     P.ScoreTeam2,
                     SecondTeamName = TT.TeamName,
                     P.PrognosedPlayer,
                     PP.PlayerName,
                     PlayerTeam = TTT.TeamName,
                     M.FirstTeamScore,
                     M.SecondTeamScore,
                     PlayerID = PP.Id,
                     M.StartTime,
                     P.PrognosedDate

                     /*
                      
                      p.voter_id AS voterid,
                                p.score_team1 AS sT1,
                                t.team_name AS First_Team,
                                p.score_team2 AS sT2,
                                tt.team_name AS Second_Team,
                                p.prognosed_player AS prognosedP,
                                pp.player_name AS PlayerName,
                                ttt.team_name AS PlayerTeam,
                                m.first_team_score AS FirstTeamScore,
                                m.second_team_score AS SecondTeamScore,
                                pp.id               AS PlayerID,
                                                    m.START_TIME        AS StartMatch,
                                to_char(p.prognosed_date, 'dd-MM-yyyy HH24:MI:SS') AS prognosedD,
                                                    p.PROGNOSED_DATE AS prognozedDate
                      */
                 }
/*
 FROM IJOSU.prognose p
                      LEFT JOIN IJOSU.match m ON m.id = p.match_id
                      LEFT JOIN IJOSU.voter v ON v.id = p.Voter_id
                      LEFT JOIN IJOSU.players pp ON pp.id = p.prognosed_player
                      LEFT JOIN IJOSU.Teams t ON t.id = m.first_Team_id
                      LEFT JOIN IJOSU.Teams tt ON tt.id = m.Second_Team_Id
                      LEFT JOIN IJOSU.Teams ttt ON ttt.id = pp.team_id
                      WHERE v.chat_id = P_UserID AND p.match_id = P_MatchID
                      ORDER BY p.prognosed_date DESC
 */
).ToList();
    /*
                
    SELECT m.id AS ID,
    m.first_team_score AS sT1,
    t.team_name AS First_Team,
    m.second_team_score AS sT2,
    tt.team_name AS Second_Team,
    to_char(m.start_time, 'MM/dd/yyyy HH24:MI:SS') AS StartTime
                FROM IJOSU.match m
                LEFT JOIN IJOSU.Teams t ON t.id = m.first_Team_id
                LEFT JOIN IJOSU.Teams tt ON tt.id = m.Second_Team_Id
                WHERE m.id = P_MatchID
    */


               /*
                SELECT p.id AS ID,
                                p.voter_id AS voterid,
                                p.score_team1 AS sT1,
                                t.team_name AS First_Team,
                                p.score_team2 AS sT2,
                                tt.team_name AS Second_Team,
                                p.prognosed_player AS prognosedP,
                                pp.player_name AS PlayerName,
                                ttt.team_name AS PlayerTeam,
                                m.first_team_score AS FirstTeamScore,
                                m.second_team_score AS SecondTeamScore,
                                pp.id               AS PlayerID,
                                                    m.START_TIME        AS StartMatch,
                                to_char(p.prognosed_date, 'dd-MM-yyyy HH24:MI:SS') AS prognosedD,
                                                    p.PROGNOSED_DATE AS prognozedDate

                           FROM IJOSU.prognose p
                                     LEFT JOIN IJOSU.match m ON m.id = p.match_id
                                     LEFT JOIN IJOSU.voter v ON v.id = p.Voter_id
                                     LEFT JOIN IJOSU.players pp ON pp.id = p.prognosed_player
                                     LEFT JOIN IJOSU.Teams t ON t.id = m.first_Team_id
                                     LEFT JOIN IJOSU.Teams tt ON tt.id = m.Second_Team_Id
                                     LEFT JOIN IJOSU.Teams ttt ON ttt.id = pp.team_id
                                     WHERE v.chat_id = P_UserID AND p.match_id = P_MatchID
                                     ORDER BY p.prognosed_date DESC

                */
    var text = "";
    //top.ForEach(item => text += item + "\n");
    prognoses.ForEach(item => text += item + "\n");
    return text;
});

app.MapGet("/GetTopVoters", (AdventureContext db) =>
{
    var top = (from P in db.Prognoses
               join V in db.Voters on P.VoterId equals V.Id
               join M in db.Matches on P.MatchId equals M.Id
               join PG in db.PlayerGoals on P.PrognosedPlayer equals PG.PlayerId
               group new { P, V, M, PG } by new { V.FirstName, V.LastName } into g
               let TotalVoters = g.Count()
               let points = g.Sum(x => ((x.P.ScoreTeam1 == 99 && x.P.ScoreTeam2 == 0 && x.M.FirstTeamScore > x.M.SecondTeamScore && x.P.PrognosedDate < x.M.StartTime) ||
                                       (x.P.ScoreTeam1 == 0 && x.P.ScoreTeam2 == 99 && x.M.FirstTeamScore < x.M.SecondTeamScore && x.P.PrognosedDate < x.M.StartTime) ||
                                       (x.P.ScoreTeam1 == 50 && x.P.ScoreTeam2 == 50 && x.M.FirstTeamScore == x.M.SecondTeamScore && x.P.PrognosedDate < x.M.StartTime) ? 1 : 0) +
                                    (x.P.ScoreTeam1 == x.M.FirstTeamScore && x.P.ScoreTeam2 == x.M.SecondTeamScore && x.P.PrognosedDate < x.M.StartTime ? 3 : 0))
               select new 
               {
                   g.Key.FirstName,
                   g.Key.LastName,
                   Points = points
               }
               ).ToList();
    var text = "";
    top.ForEach(item => text += item);
    //foreach (var item in top)
    //{
    //    text += item.ToString();
    //}

    return text;

    /*
     WHERE P.voter_id = v.id OR P.match_id = m.id
     
     (

      SELECT DISTINCT
            v.FIRST_NAME,
            v.LAST_NAME,
			MAX(p.PROGNOSED_DATE) AS LastPrognosedDate,
            SUM(
            CASE WHEN (P.score_team1 = 99 AND P.Score_Team2 = 0 AND m.first_team_score > m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME) OR
                      (P.score_team1 = 0 AND P.Score_Team2 = 99 AND m.first_team_score < m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME) OR
                      (P.score_team1 = 50 AND P.Score_Team2 = 50 AND m.first_team_score = m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME)
                 THEN 1 ELSE 0 END + --
            CASE WHEN P.score_team1 = m.first_team_score AND P.Score_Team2 = m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME THEN 3 ELSE 0 END +
            CASE WHEN G.match_id IS NOT NULL AND P.match_id = G.match_id AND p.PROGNOSED_DATE < m.START_TIME THEN 1 ELSE 0 END) Points,
						SUM(
            CASE WHEN (P.score_team1 = 99 AND P.Score_Team2 = 0 AND m.first_team_score > m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME) OR
                      (P.score_team1 = 0 AND P.Score_Team2 = 99 AND m.first_team_score < m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME) OR
                      (P.score_team1 = 50 AND P.Score_Team2 = 50 AND m.first_team_score = m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME)
                 THEN 1 ELSE 0 END + --
            CASE WHEN P.score_team1 = m.first_team_score AND P.Score_Team2 = m.second_team_score AND p.PROGNOSED_DATE < m.START_TIME THEN 3 ELSE 0 END +
            CASE WHEN G.match_id IS NOT NULL AND P.match_id = G.match_id AND p.PROGNOSED_DATE < m.START_TIME THEN 1 ELSE 0 END 
            ) Points_After	 
      FROM      prognose     P
           JOIN voter        V ON P.voter_id = v.id
      LEFT JOIN match        M ON m.ID = P.match_id
      LEFT JOIN Player_Goals G ON P.Prognosed_Player = G.player_id
      GROUP BY v.FIRST_NAME, v.LAST_NAME
      ORDER BY Points_After DESC, LastPrognosedDate  
      
     
     */
});

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
    db.USERS.Add(new Users
    {  // Adaugarea unei date in baza de date
        Username = "nnn",
        Password = "pass",
        FirstName = "Iadsaon",
        LastName = "sdada",
        Email = "dasdas",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now,
        RoleId = 2,
    });
    //db.users.Add(new User
    //{
    //    Token = "Token"
    //});
    db.SaveChanges();
    context.Response.Redirect("/");
});

app.MapDelete("/DeleteUser/{name}", (string name, ApplicationContext db, HttpContext context) =>
{
    var users = db.USERS.ToList();
    var entityToDelete = users.Find(user => user.Username == name);
    db.USERS.Remove(entityToDelete);
    db.SaveChanges();
    context.Response.Redirect("/");
});

app.MapGet("/", (ApplicationContext db) =>
{
    //db.Database.EnsureCreated();
    var users = db.USERS.ToList();
    var users1 = db.USERS.FromSql($"Select * from Users").ToList();
    //var users = db.users.ToList();
    var text = "";

    //foreach (var user in users)
    //{
    //    text += $"{user.idUser} - {user.Token} \n";
    //}
    foreach (var user in users)
    {
        text += $"{user.Username} - {user.StartDate} \n";
    }
    return Results.Text(text);
});

app.Run();