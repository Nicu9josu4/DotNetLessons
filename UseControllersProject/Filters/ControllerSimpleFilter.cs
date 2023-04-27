using Microsoft.AspNetCore.Mvc.Filters;

namespace UseMVCProject.Filters
{
    public class ControllerSimpleFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
        }
    }
}