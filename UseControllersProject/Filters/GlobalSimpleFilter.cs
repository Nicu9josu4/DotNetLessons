using Microsoft.AspNetCore.Mvc.Filters;

namespace UseMVCProject.Filters
{
    public class GlobalSimpleFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            
        }
    }
}
