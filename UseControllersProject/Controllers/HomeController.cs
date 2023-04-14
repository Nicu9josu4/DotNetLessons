using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UseControllersProject.Models;

namespace UseControllersProject.Controllers
{
    //public class HomeController
    //{
    //    public string Index() => "Hello from \"Home1 Controller\" with sufix";
    //}
    //public class Home2 : Controller
    //{
    //    public string Index() => "Hello from \"Home2 Controller\" with inheritance a Controller class";
    //}
    //[Controller]
    //public class Home3
    //{
    //    public string Index() => "Hello from \"Home3 Controller\" with using an atribute";
    //}




    //[ApiController]
    //[Route("api/[controller]")]
    //public class Home4Controller : Controller
    //{
    //    //[HttpGet]
    //    public string Action()
    //    {
    //        //ViewData["Message"] = $"id-ul omului x este: {id}";
    //        return "Hello";
    //    }

    //}

    public class Home : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        public string Create(Person person)
        {
            if (person.Age > 110 || person.Age < 0)
            {
                ModelState.AddModelError("Age", "Возраст должен находиться в диапазоне от 0 до 110");
            }
            if (person.Name?.Length < 3)
            {
                ModelState.AddModelError("Name", "Недопустимая длина строки. Имя должно иметь больше 2 символов");
            }

            if (ModelState.IsValid)
                return $"{person.Name} - {person.Age}";

            string errorMessages = "";
            // проходим по всем элементам в ModelState
            foreach (var item in ModelState)
            {
                // если для определенного элемента имеются ошибки
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                    // пробегаемся по всем ошибкам
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }
                }
            }
            return errorMessages;
        }
    }
}
