using Microsoft.AspNetCore.Mvc.Filters;

namespace UseMVCProject.Filters
{
    public class SimpleResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("Ceva nu e bine in executed");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/MM/yyyy HH-mm-ss"));
            //context.HttpContext.Response.WriteAsync("Ceva nu e bine in executing");
        }
    }
}