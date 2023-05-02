using Microsoft.AspNetCore.Mvc.Filters;

namespace UseMVCProject.Filters
{
    public class ActionSimpleFilter : Attribute, IActionFilter
    {
        private readonly ILogger _logger;

        public ActionSimpleFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ActionSimpleFilter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"OnResourceExecuted - {DateTime.Now}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"OnResourceExecuting - {DateTime.Now}");
        }
    }
}