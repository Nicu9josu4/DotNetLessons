using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UseMVCProject.Controllers;

namespace UseMVCProject.Controllers
{
    public class ExceptionFilterController : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ILogger logger = context.HttpContext.RequestServices.GetService<ILogger>();
            logger?.LogInformation(context.Result.ToString());
        }
    }
}

class ExceptionMethod
{
    [TypeFilter(typeof(ExceptionFilterController))]
    public void Method()
    {
        // Aici ce se utilizeaza in metoda tipa o eroare
    }
}
// 
