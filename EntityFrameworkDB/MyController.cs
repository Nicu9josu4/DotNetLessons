using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDB
{
    public class MyController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public MyController(DatabaseContext context)
        {
            _dbContext = context;
        }
        IActionResult Index()
        {
            var entities = _dbContext.Vacancies.ToList();
            return View(entities);
        }
    }
}
