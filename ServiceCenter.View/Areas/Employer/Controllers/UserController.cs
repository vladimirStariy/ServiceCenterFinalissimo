using Microsoft.AspNetCore.Mvc;

namespace ServiceCenter.View.Areas.Employer.Controllers
{
    [Area("Employer")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
