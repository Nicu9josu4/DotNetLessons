using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using UseMVCProject.Services;

namespace UseControllersProject.Components
{
    public class Timer : ViewComponent
    {
        private IGetTimerService _getTimer;

        public Timer(IGetTimerService timerService)
        {
            _getTimer = timerService;
        }

        //public string Invoke() => _getTimer.GetTime();

        public IViewComponentResult Invoke()
        {
            string time = _getTimer.GetTime(); // Send Content to View
            return Content(time);

            return new ContentViewComponentResult(time); // Send a content by new object

            return new HtmlContentViewComponentResult( // send a content by html page
                    new HtmlString($"<p>Текущее время:<b>{time}</b></p>")
                );
        }
    }
}