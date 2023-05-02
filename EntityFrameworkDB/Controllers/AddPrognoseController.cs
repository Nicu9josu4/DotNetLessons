using EntityFrameworkDB.Model;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDB.Controllers
{
    public class AddPrognoseController:Controller
    {
        [HttpGet]
        public void Index(Prognose prognose)
        {
            AdventureContext db = new();
            db.Add(new Prognose
            {
                VoterId = prognose.VoterId,
                MatchId = prognose.MatchId,
                ScoreTeam1 = prognose.ScoreTeam1,
                ScoreTeam2 = prognose.ScoreTeam2,
                PrognosedDate = DateTime.Now,
                PrognosedPlayer = prognose.PrognosedPlayer,
                PrognosedType = prognose.PrognosedType,
            });
            db.SaveChanges();
            HttpContext.Response.Redirect("/GetPrognoses");
        }
    }
}
