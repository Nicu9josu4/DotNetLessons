using Microsoft.AspNetCore.Mvc;

namespace UseControllersProject.Controllers
{
    public class Home1Controller
    {
        public string Index() => "Hello from Home1Controller with Sufix";
    }
    public class Home2 : Controller
    {
        public string Index() => "Hello from Home2Controller with inheritance a Controller class";
    }
    [Controller]
    public class Home3
    {
        public string Index() => "Hello from Home2Controller with using an Atribute";
    }
}
