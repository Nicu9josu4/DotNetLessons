using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDB
{
    public class MyController : Controller
    {
        private readonly ApplicationContext _dbContext;
        public MyController(ApplicationContext context)
        {
            _dbContext = context;
        }
        IActionResult Index()
        {
            List<Vacancy> vacancies = _dbContext.Vacancies.ToList();
            return View(vacancies);
        }
    }
}
