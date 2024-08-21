using _MVC_Identity_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _MVC_Identity_.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
