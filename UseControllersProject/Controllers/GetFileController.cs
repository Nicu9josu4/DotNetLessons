using Microsoft.AspNetCore.Mvc;

namespace UseControllersProject.Controllers
{
    public class GetFileController : Controller
    {
        public IActionResult Index()
        {
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.txt");
            string file_type = "text/plain";
            string file_name = "file.txt"; // nu-i obligatoriu
            return PhysicalFile(file_path, file_type, file_name);
            // return File();
        }
    }
}