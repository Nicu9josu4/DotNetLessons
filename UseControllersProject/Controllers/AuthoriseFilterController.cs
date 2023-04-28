using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UseMVCProject.Controllers
{
    public class AuthoriseFilterController : Controller
    {
        [Authorize(Roles = "admin")] // Acest filtru va bloca accesul la acțiunea "AdminAction" pentru toți utilizatorii care nu au rolul de "admin".
        public IActionResult AdminAction()
        {
            // acțiune doar pentru utilizatorii cu rol de admin
            return Content("Welcome Admin");
        }
        [AllowAnonymous]
        public IActionResult PublicAction() // Acest filtru permite accesul la acțiunea "PublicAction" pentru toți utilizatorii, indiferent de drepturile lor.
        {
            // acțiune publică
            return Content("Welcome User");
        }
    }
}
