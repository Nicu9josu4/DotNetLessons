using Microsoft.AspNetCore.Mvc.Filters;

namespace UseMVCProject.Filters
{
    public class ActionSimpleFilter : Attribute, IResourceFilter
    {
        readonly ILogger _logger;

        public ActionSimpleFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ActionSimpleFilter");
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation($"OnResourceExecuted - {DateTime.Now}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation($"OnResourceExecuting - {DateTime.Now}");
        }
    }
}
