using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UseHealthCheck.Controllers
{
    public class StopHealthUI : Controller
    {
        //private readonly IServiceCollection _services;

        //public StopHealthUI(IServiceCollection services)
        //{
        //    _services = services;
        //}
        [HttpGet("api/StopMonitoring")]
        public IActionResult StopMonitoring()
        {
            //var healthCheckBuilder = _services.AddHealthChecks();
            //foreach (var registration in healthCheckBuilder.Services)
            //{
            //    _services.Remove(registration);
            //}
            return Ok();
        }
        [HttpGet("api/StartMonitoring")]
        public IActionResult StartMonitoring()
        {
            return Ok();
        }

    }
}
