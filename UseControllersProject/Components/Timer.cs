using Microsoft.AspNetCore.Mvc;

namespace UseControllersProject.Components
{
    public class Timer : ViewComponent
    {
        public string Invoke() => $"Time now is: {DateTime.Now:hh:mm:ss}";
    }
}
